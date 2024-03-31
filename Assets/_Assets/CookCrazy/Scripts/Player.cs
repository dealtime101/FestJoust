using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// N�cessaire pour utiliser le nouveau syst�me d'input Unity.
using UnityEngine.InputSystem.Controls;

public class Player : MonoBehaviour
{
    // Singleton pattern pour assurer un acc�s global unique � l'instance du joueur.
    public static Player Instance { get; private set; }

    // �v�nement d�clench� lorsque le comptoir s�lectionn� par le joueur change.
    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;

    // Classe pour les arguments de l'�v�nement OnSelectedCounterChanged.
    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public ClearCounter selectedCounter;
    }

    // Variables de configuration modifiables dans l'�diteur Unity.
    [SerializeField] private float moveSpeed = 7f; // Vitesse de d�placement du joueur.
    [SerializeField] private GameInput gameInput; // R�f�rence au script de gestion des inputs.
    [SerializeField] private LayerMask counterLayerMask; // Masque de layer pour identifier les comptoirs.

    // Variables d'�tat interne.
    private bool isWalking; // Si le joueur est en train de marcher.
    private Vector3 lastInteractDir; // Derni�re direction d'interaction.
    private ClearCounter selectedCounter; // Comptoir actuellement s�lectionn�.

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
        // S'abonne � l'�v�nement OnInteractAction du script GameInput pour g�rer les interactions du joueur.
        gameInput.OnInteractAction += GameInput_OnInteractAction;
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {
        // G�re l'action d'interaction en appelant la m�thode Interact sur le comptoir s�lectionn�.
        if (selectedCounter != null)
        {
            selectedCounter.Interact();
        }
    }

    private void Update()
    {
        // Appelle les m�thodes de gestion du mouvement et de l'interaction � chaque frame.
        HandleMovement();
        HandleInteraction();
    }

    // Retourne si le joueur est actuellement en train de marcher.
    public bool IsWalking()
    {
        return isWalking;
    }

    // G�re la logique d'interaction, notamment la s�lection de comptoirs � interagir.
    private void HandleInteraction()
    {
        // Utilise le GameInput pour obtenir le vecteur de mouvement normalis�.
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        // Convertit le vecteur d'input 2D en vecteur 3D pour le mouvement.
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        // Met � jour la derni�re direction d'interaction si le joueur se d�place.
        if (moveDir != Vector3.zero)
        {
            lastInteractDir = moveDir;
        }

        // Raycast pour d�tecter les comptoirs devant le joueur.
        float interactDistance = 2f;
        if (Physics.Raycast(transform.position, lastInteractDir, out RaycastHit raycastHit, interactDistance, counterLayerMask))
        {
            // V�rifie si l'objet touch� par le raycast est un comptoir.
            if (raycastHit.transform.TryGetComponent(out ClearCounter clearCounter))
            {
                // Si c'est un nouveau comptoir, met � jour le comptoir s�lectionn�.
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

    // G�re la logique de mouvement du joueur.
    private void HandleMovement()
    {
        // Similaire � HandleInteraction, obtient le vecteur de mouvement.
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        // Calcule la distance de mouvement bas�e sur la vitesse et le temps �coul�.
        float moveDistance = moveSpeed * Time.deltaTime;

        // V�rifie les collisions pour s'assurer que le joueur peut se d�placer.
        // Utilise un CapsuleCast pour simuler le collider du joueur.
        float playerRadius = .7f;
        float playerHeight = 2f;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);

        // Si le mouvement est bloqu�, tente de se d�placer uniquement sur l'axe X ou Z.
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
                // Si le mouvement sur X est bloqu�, tente sur l'axe Z.
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);

                if (canMove)
                {
                    moveDir = moveDirZ;
                }
                // Sinon, le joueur ne peut pas se d�placer.
            }
        }

        // Si le joueur peut se d�placer, met � jour sa position.
        if (canMove)
        {
            transform.position += moveDir * moveDistance;
        }

        // Met � jour l'�tat de marche en fonction du mouvement.
        isWalking = moveDir != Vector3.zero;

        // Oriente le joueur dans la direction du mouvement.
        float rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
    }

    // Met � jour le comptoir s�lectionn� et d�clenche l'�v�nement OnSelectedCounterChanged.
    private void SetSelectedCounter(ClearCounter selectedCounter)
    {
        this.selectedCounter = selectedCounter;

        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
        {
            selectedCounter = selectedCounter
        });
    }
}
