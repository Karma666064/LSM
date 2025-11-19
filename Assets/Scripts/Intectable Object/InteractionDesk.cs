using UnityEngine;

public class InteractionDesk : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject paper;
    [SerializeField] GameObject taskPanel;

    bool paperActive;

    public void OnInteractStart(PlayerInteraction player)
    {
        paperActive = !paperActive;
        paper.SetActive(paperActive);
        taskPanel.SetActive(true);
        player.GetComponent<PlayerMove>().canMove = !paperActive;
    }
}
