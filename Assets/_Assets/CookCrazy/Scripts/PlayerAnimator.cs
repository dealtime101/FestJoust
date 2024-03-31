using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Définit une classe publique PlayerAnimator qui hérite de MonoBehaviour,
// permettant à ce script d'être attaché à un GameObject dans Unity.
public class PlayerAnimator : MonoBehaviour
{
    // Déclare une constante string IS_WALKING qui stocke le nom du paramètre d'animation.
    // Cela évite les erreurs de saisie manuelle dans les chaînes et facilite les modifications.
    private const string IS_WALKING = "IsWalking";

    // Référence au composant Player du joueur, défini comme [SerializeField] pour être assigné depuis l'éditeur d'Unity.
    // Cela permet d'accéder aux méthodes et propriétés du joueur, comme vérifier s'il marche.
    [SerializeField] private Player player;

    // Variable privée pour stocker une référence à l'Animator attaché au même GameObject que ce script.
    private Animator animator;

    // La méthode Awake est appelée lorsque le script est chargé.
    // Elle est utilisée ici pour initialiser la référence à l'Animator.
    private void Awake()
    {
        animator = GetComponent<Animator>(); // Récupère le composant Animator sur le GameObject.
    }

    // La méthode Update est appelée à chaque frame.
    // Elle est utilisée pour mettre à jour les paramètres de l'Animator basés sur l'état du joueur.
    private void Update()
    {
        // Utilise la méthode IsWalking() du composant Player pour vérifier si le joueur est en train de marcher,
        // et met à jour le paramètre IsWalking de l'Animator en conséquence.
        // Cela permet de contrôler l'animation de marche du joueur.
        animator.SetBool(IS_WALKING, player.IsWalking());
    }
}