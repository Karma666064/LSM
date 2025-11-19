using UnityEngine;

public class InteractionLibrary : MonoBehaviour, IInteractable
{
    [SerializeField] PlayerMove player;
    [SerializeField] GameObject callToActionText;
    [SerializeField] GameObject ArmoireUI_Panel;

    public void OnInteractStart(PlayerInteraction player)
    {
        if (ArmoireUI_Panel != null)
        {
            // Active le panneau UI et le met sur l'écran
            ArmoireUI_Panel.SetActive(true);

            // Pause le jeu
            Time.timeScale = 0f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            callToActionText.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            callToActionText.SetActive(false);
        }
    }
}
