using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// L'attribut CreateAssetMenu permet de créer des instances de KitchenObjectSO via l'interface de l'éditeur Unity,
// en faisant un clic droit dans le panneau Project et en naviguant à Create > KitchenObjectSO.
[CreateAssetMenu()]

// Déclare une classe publique KitchenObjectSO qui hérite de ScriptableObject,
// ce qui permet de stocker des données sans nécessiter un GameObject.
public class KitchenObjectSO : ScriptableObject
{
    // Déclare une variable publique de type Transform pour stocker un prefab.
    // Un prefab est une instance préfabriquée d'un GameObject que vous pouvez créer et configurer dans l'éditeur Unity,
    // puis réutiliser à plusieurs endroits dans votre jeu.
    public Transform prefab;

    // Déclare une variable publique de type Sprite pour stocker un sprite.
    // Un sprite est une image 2D utilisée généralement pour des objets de jeu en 2D, des icônes ou des éléments d'interface utilisateur.
    public Sprite sprite;

    // Déclare une variable publique de type string pour stocker le nom de l'objet.
    // Cette chaîne peut être utilisée pour identifier l'objet de cuisine dans l'interface utilisateur, les logs, etc.
    public string objectName;
}