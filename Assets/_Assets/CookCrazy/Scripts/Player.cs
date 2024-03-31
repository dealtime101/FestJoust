using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Nécessaire pour utiliser le nouveau système d'input Unity.
using UnityEngine.InputSystem.Controls;

public class Player : MonoBehaviour
{
    // Singleton pattern pour assurer un accès global unique à l'instance du joueur.
    public static Player Instance { get; private set; }

    // Événement déclenché lorsque le comptoir sélectionné par le joueur change.
    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;

    // Classe pour les arguments de l'événement OnSelectedCounterChanged.
    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public ClearCounter selectedCounter;
    }

    // Variables de configuration modifiables dans l'éditeur Unity.
    [SerializeField] private float moveSpeed = 7f; // Vitesse de déplacement du joueur.
    [SerializeField] private GameInput gameInput; // Référence au script de gestion des inputs.
    [SerializeField] private LayerMask counterLayerMask; // Masque de layer pour identifier les comptoirs.

    // Variables d'état interne.
    private bool isWalking; // Si le joueur est en train de marcher.
    private Vector3 lastInteractDir; // Dernière direction d'interaction.
    private ClearCounter selectedCounter; // Comptoir actuellement sélectionné.

    private void Awake()
    {
        // Initialise le singleton Instance et assure qu'il n'y a qu'une seule instance du joueur.
        if (Instance != null)
        {
            Debug.LogError("There is more than one Player instance");
        }
        Instance = this;
    }

    private void Start()
    {
        // S'abonne à l'événement OnInteractAction du script GameInput pour gérer les interactions du joueur.
        gameInput.OnInteractAction += GameInput_OnInteractAction;
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {
        // Gère l'action d'interaction en appelant la méthode Interact sur le comptoir sélectionné.
        if (selectedCounter != null)
        {
            selectedCounter.Interact();
        }
    }

    private void Update()
    {
        // Appelle les méthodes de gestion du mouvement et de l'interaction à chaque frame.
        HandleMovement();
        HandleInteraction();
    }

    // Retourne si le joueur est actuellement en train de marcher.
    public bool IsWalking()
    {
        return isWalking;
    }

    // Gère la logique d'interaction, notamment la sélection de comptoirs à interagir.
    private void HandleInteraction()
    {
        // Utilise le GameInput pour obtenir le vecteur de mouvement normalisé.
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        // Convertit le vecteur d'input 2D en vecteur 3D pour le mouvement.
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        // Met à jour la dernière direction d'interaction si le joueur se déplace.
        if (moveDir != Vector3.zero)
        {
            lastInteractDir = moveDir;
        }

        // Raycast pour détecter les comptoirs devant le joueur.
        float interactDistance = 2f;
        if (Physics.Raycast(transform.position, lastInteractDir, out RaycastHit raycastHit, interactDistance, counterLayerMask))
        {
            // Vérifie si l'objet touché par le raycast est un comptoir.
            if (raycastHit.transform.TryGetComponent(out ClearCounter clearCounter))
            {
                // Si c'est un nouveau comptoir, met à jour le comptoir sélectionné.
                if (clearCounter != selectedCounter)
                {
                    SetSelectedCounter(clearCounter);
                }
            }
            else
            {
                SetSelectedCounter(null);
            }
        }
        else
        {
            SetSelectedCounter(null);
        }
    }

    // Gère la logique de mouvement du joueur.
    private void HandleMovement()
    {
        // Similaire à HandleInteraction, obtient le vecteur de mouvement.
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        // Calcule la distance de mouvement basée sur la vitesse et le temps écoulé.
        float moveDistance = moveSpeed * Time.deltaTime;

        // Vérifie les collisions pour s'assurer que le joueur peut se déplacer.
        // Utilise un CapsuleCast pour simuler le collider du joueur.
        float playerRadius = .7f;
        float playerHeight = 2f;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);

        // Si le mouvement est bloqué, tente de se déplacer uniquement sur l'axe X ou Z.
        if (!canMove)
        {
            // Tente un mouvement sur l'axe X.
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);

            if (canMove)
            {
                moveDir = moveDirX;
            }
            else
            {
                // Si le mouvement sur X est bloqué, tente sur l'axe Z.
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);

                if (canMove)
                {
                    moveDir = moveDirZ;
                }
                // Sinon, le joueur ne peut pas se déplacer.
            }
        }

        // Si le joueur peut se déplacer, met à jour sa position.
        if (canMove)
        {
            transform.position += moveDir * moveDistance;
        }

        // Met à jour l'état de marche en fonction du mouvement.
        isWalking = moveDir != Vector3.zero;

        // Oriente le joueur dans la direction du mouvement.
        float rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
    }

    // Met à jour le comptoir sélectionné et déclenche l'événement OnSelectedCounterChanged.
    private void SetSelectedCounter(ClearCounter selectedCounter)
    {
        this.selectedCounter = selectedCounter;

        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
        {
            selectedCounter = selectedCounter
        });
    }
}
