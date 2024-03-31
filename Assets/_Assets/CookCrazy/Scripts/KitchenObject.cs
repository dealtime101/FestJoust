using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// D�clare une classe publique KitchenObject qui h�rite de MonoBehaviour,
// ce qui permet de l'attacher � un GameObject dans Unity.
public class KitchenObject : MonoBehaviour
{
    // D�clare une variable priv�e de type KitchenObjectSO et l'expose dans l'�diteur Unity gr�ce � [SerializeField].
    // Cela permet d'assigner un ScriptableObject sp�cifique de type KitchenObjectSO � cet objet de cuisine depuis l'�diteur Unity.
    // Ce ScriptableObject contient les donn�es sp�cifiques � cet objet de cuisine, comme son prefab, son sprite, et son nom.
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    // D�clare une m�thode publique GetKitchenObjectSO qui renvoie la r�f�rence au KitchenObjectSO associ�.
    // Cette m�thode peut �tre appel�e par d'autres scripts qui ont besoin d'acc�der aux donn�es de l'objet de cuisine,
    // comme pour obtenir son sprite pour l'afficher dans l'interface utilisateur ou lire son nom pour des raisons de logique de jeu.
    public KitchenObjectSO GetKitchenObjectSO()
    {
        return kitchenObjectSO;
    }
}