using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// D�clare une classe publique ClearCounter qui h�rite de MonoBehaviour,
// ce qui permet � ce script d'�tre attach� � un objet Unity.
public class ClearCounter : MonoBehaviour
{
    // Utilise [SerializeField] pour exposer une variable priv�e dans l'inspecteur Unity sans rendre la variable publique.
    // Cela permet de conserver l'encapsulation tout en permettant le r�glage via l'�diteur Unity.
    [SerializeField] private KitchenObjectSO kitchenObjectSO; // R�f�rence � un ScriptableObject repr�sentant un objet de cuisine.
    [SerializeField] private Transform counterTopPoint; // Point de transformation sur le comptoir o� les objets de cuisine seront plac�s.

    // Variable priv�e pour suivre l'instance de KitchenObject actuellement sur le comptoir.
    private KitchenObject kitchenObject;

    // M�thode publique Interact appel�e pour interagir avec le comptoir.
    public void Interact()
    {
        // V�rifie si kitchenObject est nul, ce qui indique qu'aucun objet n'est actuellement plac� sur le comptoir.
        if (kitchenObject == null)
        {
            // Si aucun objet n'est pr�sent, un nouvel objet est instanci� � la position counterTopPoint,
            // en utilisant le prefab d�fini dans kitchenObjectSO.
            Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab, counterTopPoint);

            // Le positionnement local de l'objet instanci� est r�initialis� � Vector3.zero,
            // ce qui le place exactement � l'emplacement du point counterTopPoint.
            kitchenObjectTransform.localPosition = Vector3.zero;

            // R�cup�re le composant KitchenObject de l'objet instanci� et le stocke dans la variable kitchenObject,
            // permettant ainsi de garder une trace de l'objet actuellement plac� sur le comptoir.
            kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();
        }

    }
}
