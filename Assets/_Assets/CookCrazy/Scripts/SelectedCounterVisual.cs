using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Déclare une classe publique SelectedCounterVisual qui hérite de MonoBehaviour,
// permettant de l'attacher à un GameObject dans Unity pour gérer les éléments visuels de sélection.
public class SelectedCounterVisual : MonoBehaviour
{
    // Référence à un script ClearCounter, qui gère probablement l'interaction avec un comptoir spécifique.
    // La référence doit être assignée depuis l'éditeur Unity grâce à [SerializeField].
    [SerializeField] private ClearCounter clearCounter;

    // Référence à l'objet visuel qui indiquera la sélection (par exemple, un halo ou un cadre).
    // Assigné depuis l'éditeur Unity.
    [SerializeField] private GameObject visualGameObject;

    // La méthode Start est appelée juste avant la première frame de mise à jour.
    // Ici, elle s'abonne à l'événement OnSelectedCounterChanged du joueur, pour réagir aux changements de comptoir sélectionné.
    private void Start()
    {
        Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
    }

    // Méthode appelée lorsque l'événement OnSelectedCounterChanged est déclenché par le joueur.
    // Elle prend en paramètres le sender (l'émetteur de l'événement, ici le joueur) et les arguments de l'événement e,
    // qui contiennent le comptoir actuellement sélectionné par le joueur.
    private void Player_OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e)
    {
        // Vérifie si le comptoir sélectionné correspond au ClearCounter associé à cet objet visuel.
        if (e.selectedCounter == clearCounter)
        {
            Show(); // Si oui, affiche l'élément visuel.
        }
        else
        {
            Hide(); // Sinon, cache l'élément visuel.
        }
    }

    // Active l'objet visuel, le rendant visible dans la scène.
    private void Show()
    {
        visualGameObject.SetActive(true);
    }

    // Désactive l'objet visuel, le rendant invisible dans la scène.
    private void Hide()
    {
        visualGameObject.SetActive(false);
    }
}