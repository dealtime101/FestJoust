using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// L'attribut CreateAssetMenu permet de cr�er des instances de KitchenObjectSO via l'interface de l'�diteur Unity,
// en faisant un clic droit dans le panneau Project et en naviguant � Create > KitchenObjectSO.
[CreateAssetMenu()]

// D�clare une classe publique KitchenObjectSO qui h�rite de ScriptableObject,
// ce qui permet de stocker des donn�es sans n�cessiter un GameObject.
public class KitchenObjectSO : ScriptableObject
{
    // D�clare une variable publique de type Transform pour stocker un prefab.
    // Un prefab est une instance pr�fabriqu�e d'un GameObject que vous pouvez cr�er et configurer dans l'�diteur Unity,
    // puis r�utiliser � plusieurs endroits dans votre jeu.
    public Transform prefab;

    // D�clare une variable publique de type Sprite pour stocker un sprite.
    // Un sprite est une image 2D utilis�e g�n�ralement pour des objets de jeu en 2D, des ic�nes ou des �l�ments d'interface utilisateur.
    public Sprite sprite;

    // D�clare une variable publique de type string pour stocker le nom de l'objet.
    // Cette cha�ne peut �tre utilis�e pour identifier l'objet de cuisine dans l'interface utilisateur, les logs, etc.
    public string objectName;
}