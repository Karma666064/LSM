// ArmoireResourceTrigger.cs (Attaché à l'objet 3D de l'Armoire)

using UnityEngine;
using TMPro;

public class ArmoireResourceTrigger : MonoBehaviour
{
    [Header("Panneau UI à Activer")]
    // Glissez-déposez l'ArmoireUI_Panel ici
    public GameObject ArmoireUI_Panel;

    [Header("Prompt d'Interaction")]
    public GameObject InteractionPrompt;

    // Remplacé par la logique du PlayerController
    // private bool playerIsNearby = false; 

    // --- NOUVELLE FONCTION PUBLIQUE ---
    // Cette méthode est appelée par le PlayerController lorsque le joueur appuie sur la touche d'action.
    public void PerformInteraction()
    {
        Debug.Log("Armoire : Interaction reçue du joueur. Ouverture de l'UI.");

        if (ArmoireUI_Panel != null)
        {
            // Active le panneau UI et le met sur l'écran
            ArmoireUI_Panel.SetActive(true);

            // Pause le jeu
            Time.timeScale = 0f;
        }
    }

    // --- Logique de Détection (Trigger) ---

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("le joueur est dans la zone");
            if (InteractionPrompt != null)
            {
                InteractionPrompt.SetActive(true);
            }
            // Récupère le PlayerController
            PlayerController pc = other.GetComponent<PlayerController>();

            if (pc != null)
            {
                // Informe le PlayerController que cette armoire est maintenant l'objet interactif
                pc.SetCurrentInteractable(this);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (InteractionPrompt != null)
            {
                InteractionPrompt.SetActive(false); 
            }
            // Récupère le PlayerController
            PlayerController pc = other.GetComponent<PlayerController>();

            if (pc != null)
            {
                // Demande au PlayerController d'oublier cette armoire
                pc.ClearCurrentInteractable();
            }
        }
    }

}