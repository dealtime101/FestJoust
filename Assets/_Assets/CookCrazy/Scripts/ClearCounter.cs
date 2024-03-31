using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Déclare une classe publique ClearCounter qui hérite de MonoBehaviour,
// ce qui permet à ce script d'être attaché à un objet Unity.
public class ClearCounter : MonoBehaviour
{
    // Utilise [SerializeField] pour exposer une variable privée dans l'inspecteur Unity sans rendre la variable publique.
    // Cela permet de conserver l'encapsulation tout en permettant le réglage via l'éditeur Unity.
    [SerializeField] private KitchenObjectSO kitchenObjectSO; // Référence à un ScriptableObject représentant un objet de cuisine.
    [SerializeField] private Transform counterTopPoint; // Point de transformation sur le comptoir où les objets de cuisine seront placés.

    // Variable privée pour suivre l'instance de KitchenObject actuellement sur le comptoir.
    private KitchenObject kitchenObject;

    // Méthode publique Interact appelée pour interagir avec le comptoir.
    public void Interact()
    {
        // Vérifie si kitchenObject est nul, ce qui indique qu'aucun objet n'est actuellement placé sur le comptoir.
        if (kitchenObject == null)
        {
            // Si aucun objet n'est présent, un nouvel objet est instancié à la position counterTopPoint,
            // en utilisant le prefab défini dans kitchenObjectSO.
            Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab, counterTopPoint);

            // Le positionnement local de l'objet instancié est réinitialisé à Vector3.zero,
            // ce qui le place exactement à l'emplacement du point counterTopPoint.
            kitchenObjectTransform.localPosition = Vector3.zero;

            // Récupère le composant KitchenObject de l'objet instancié et le stocke dans la variable kitchenObject,
            // permettant ainsi de garder une trace de l'objet actuellement placé sur le comptoir.
            kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();
        }

    }
}
