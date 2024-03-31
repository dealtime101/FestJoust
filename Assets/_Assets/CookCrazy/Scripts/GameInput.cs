using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Définit une classe publique GameInput qui hérite de MonoBehaviour,
// permettant à ce script d'être attaché à un GameObject dans Unity.
public class GameInput : MonoBehaviour
{
    // Déclare un événement public OnInteractAction utilisant EventHandler.
    // Cet événement peut être abonné par d'autres scripts qui souhaitent réagir à l'action d'interaction.
    public event EventHandler OnInteractAction;

    // Variable pour stocker l'instance de PlayerInputActions, qui est une classe générée automatiquement
    // par le système d'input Unity à partir de la configuration des actions d'input dans l'éditeur.
    private PlayerInputActions playerInputActions;

    // La méthode Awake est appelée lorsque le script est chargé.
    // Elle initialise les actions d'input et active le mappage d'input pour le joueur.
    private void Awake()
    {
        playerInputActions = new PlayerInputActions(); // Crée une nouvelle instance de PlayerInputActions.
        playerInputActions.Player.Enable(); // Active le mappage d'input pour le joueur.

        // S'abonne à l'événement 'performed' de l'action Interact dans PlayerInputActions.
        // Cela signifie que la méthode Interact_performed sera appelée chaque fois que l'action Interact est déclenchée par l'utilisateur.
        playerInputActions.Player.Interact.performed += Interact_performed;
    }

    // Méthode appelée lorsque l'action d'interaction est déclenchée.
    // Elle déclenche à son tour l'événement OnInteractAction, permettant à d'autres scripts de répondre à cette interaction.
    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        // ?. est l'opérateur d'appel conditionnel. OnInteractAction est invoqué seulement si elle a des abonnés (non nul).
        // 'this' fait référence à l'instance actuelle de GameInput, et EventArgs.Empty indique qu'aucune donnée supplémentaire n'est envoyée avec l'événement.
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    // Méthode publique pour obtenir le vecteur de mouvement basé sur les entrées de l'utilisateur.
    // Utile pour déplacer le personnage dans le jeu.
    public Vector2 GetMovementVectorNormalized()
    {
        // Lit la valeur de l'action de mouvement, qui est attendue comme un Vector2 (par exemple, x pour gauche/droite, y pour haut/bas).
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();

        // Normalise le vecteur pour s'assurer que la vitesse de déplacement reste constante, indépendamment de la direction.
        inputVector = inputVector.normalized;

        // Retourne le vecteur de mouvement normalisé.
        return inputVector;
    }
}