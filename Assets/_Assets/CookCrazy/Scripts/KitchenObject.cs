using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Déclare une classe publique KitchenObject qui hérite de MonoBehaviour,
// ce qui permet de l'attacher à un GameObject dans Unity.
public class KitchenObject : MonoBehaviour
{
    // Déclare une variable privée de type KitchenObjectSO et l'expose dans l'éditeur Unity grâce à [SerializeField].
    // Cela permet d'assigner un ScriptableObject spécifique de type KitchenObjectSO à cet objet de cuisine depuis l'éditeur Unity.
    // Ce ScriptableObject contient les données spécifiques à cet objet de cuisine, comme son prefab, son sprite, et son nom.
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    // Déclare une méthode publique GetKitchenObjectSO qui renvoie la référence au KitchenObjectSO associé.
    // Cette méthode peut être appelée par d'autres scripts qui ont besoin d'accéder aux données de l'objet de cuisine,
    // comme pour obtenir son sprite pour l'afficher dans l'interface utilisateur ou lire son nom pour des raisons de logique de jeu.
    public KitchenObjectSO GetKitchenObjectSO()
    {
        return kitchenObjectSO;
    }
}