using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// D�finit une classe publique PlayerAnimator qui h�rite de MonoBehaviour,
// permettant � ce script d'�tre attach� � un GameObject dans Unity.
public class PlayerAnimator : MonoBehaviour
{
    // D�clare une constante string IS_WALKING qui stocke le nom du param�tre d'animation.
    // Cela �vite les erreurs de saisie manuelle dans les cha�nes et facilite les modifications.
    private const string IS_WALKING = "IsWalking";

    // R�f�rence au composant Player du joueur, d�fini comme [SerializeField] pour �tre assign� depuis l'�diteur d'Unity.
    // Cela permet d'acc�der aux m�thodes et propri�t�s du joueur, comme v�rifier s'il marche.
    [SerializeField] private Player player;

    // Variable priv�e pour stocker une r�f�rence � l'Animator attach� au m�me GameObject que ce script.
    private Animator animator;

    // La m�thode Awake est appel�e lorsque le script est charg�.
    // Elle est utilis�e ici pour initialiser la r�f�rence � l'Animator.
    private void Awake()
    {
        animator = GetComponent<Animator>(); // R�cup�re le composant Animator sur le GameObject.
    }

    // La m�thode Update est appel�e � chaque frame.
    // Elle est utilis�e pour mettre � jour les param�tres de l'Animator bas�s sur l'�tat du joueur.
    private void Update()
    {
        // Utilise la m�thode IsWalking() du composant Player pour v�rifier si le joueur est en train de marcher,
        // et met � jour le param�tre IsWalking de l'Animator en cons�quence.
        // Cela permet de contr�ler l'animation de marche du joueur.
        animator.SetBool(IS_WALKING, player.IsWalking());
    }
}