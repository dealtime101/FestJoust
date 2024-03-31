using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// D�finit une classe publique GameInput qui h�rite de MonoBehaviour,
// permettant � ce script d'�tre attach� � un GameObject dans Unity.
public class GameInput : MonoBehaviour
{
    // D�clare un �v�nement public OnInteractAction utilisant EventHandler.
    // Cet �v�nement peut �tre abonn� par d'autres scripts qui souhaitent r�agir � l'action d'interaction.
    public event EventHandler OnInteractAction;

    // Variable pour stocker l'instance de PlayerInputActions, qui est une classe g�n�r�e automatiquement
    // par le syst�me d'input Unity � partir de la configuration des actions d'input dans l'�diteur.
    private PlayerInputActions playerInputActions;

    // La m�thode Awake est appel�e lorsque le script est charg�.
    // Elle initialise les actions d'input et active le mappage d'input pour le joueur.
    private void Awake()
    {
        playerInputActions = new PlayerInputActions(); // Cr�e une nouvelle instance de PlayerInputActions.
        playerInputActions.Player.Enable(); // Active le mappage d'input pour le joueur.

        // S'abonne � l'�v�nement 'performed' de l'action Interact dans PlayerInputActions.
        // Cela signifie que la m�thode Interact_performed sera appel�e chaque fois que l'action Interact est d�clench�e par l'utilisateur.
        playerInputActions.Player.Interact.performed += Interact_performed;
    }

    // M�thode appel�e lorsque l'action d'interaction est d�clench�e.
    // Elle d�clenche � son tour l'�v�nement OnInteractAction, permettant � d'autres scripts de r�pondre � cette interaction.
    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        // ?. est l'op�rateur d'appel conditionnel. OnInteractAction est invoqu� seulement si elle a des abonn�s (non nul).
        // 'this' fait r�f�rence � l'instance actuelle de GameInput, et EventArgs.Empty indique qu'aucune donn�e suppl�mentaire n'est envoy�e avec l'�v�nement.
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    // M�thode publique pour obtenir le vecteur de mouvement bas� sur les entr�es de l'utilisateur.
    // Utile pour d�placer le personnage dans le jeu.
    public Vector2 GetMovementVectorNormalized()
    {
        // Lit la valeur de l'action de mouvement, qui est attendue comme un Vector2 (par exemple, x pour gauche/droite, y pour haut/bas).
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();

        // Normalise le vecteur pour s'assurer que la vitesse de d�placement reste constante, ind�pendamment de la direction.
        inputVector = inputVector.normalized;

        // Retourne le vecteur de mouvement normalis�.
        return inputVector;
    }
}