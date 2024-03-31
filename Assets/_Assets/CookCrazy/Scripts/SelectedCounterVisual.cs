using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// D�clare une classe publique SelectedCounterVisual qui h�rite de MonoBehaviour,
// permettant de l'attacher � un GameObject dans Unity pour g�rer les �l�ments visuels de s�lection.
public class SelectedCounterVisual : MonoBehaviour
{
    // R�f�rence � un script ClearCounter, qui g�re probablement l'interaction avec un comptoir sp�cifique.
    // La r�f�rence doit �tre assign�e depuis l'�diteur Unity gr�ce � [SerializeField].
    [SerializeField] private ClearCounter clearCounter;

    // R�f�rence � l'objet visuel qui indiquera la s�lection (par exemple, un halo ou un cadre).
    // Assign� depuis l'�diteur Unity.
    [SerializeField] private GameObject visualGameObject;

    // La m�thode Start est appel�e juste avant la premi�re frame de mise � jour.
    // Ici, elle s'abonne � l'�v�nement OnSelectedCounterChanged du joueur, pour r�agir aux changements de comptoir s�lectionn�.
    private void Start()
    {
        Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
    }

    // M�thode appel�e lorsque l'�v�nement OnSelectedCounterChanged est d�clench� par le joueur.
    // Elle prend en param�tres le sender (l'�metteur de l'�v�nement, ici le joueur) et les arguments de l'�v�nement e,
    // qui contiennent le comptoir actuellement s�lectionn� par le joueur.
    private void Player_OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e)
    {
        // V�rifie si le comptoir s�lectionn� correspond au ClearCounter associ� � cet objet visuel.
        if (e.selectedCounter == clearCounter)
        {
            Show(); // Si oui, affiche l'�l�ment visuel.
        }
        else
        {
            Hide(); // Sinon, cache l'�l�ment visuel.
        }
    }

    // Active l'objet visuel, le rendant visible dans la sc�ne.
    private void Show()
    {
        visualGameObject.SetActive(true);
    }

    // D�sactive l'objet visuel, le rendant invisible dans la sc�ne.
    private void Hide()
    {
        visualGameObject.SetActive(false);
    }
}