using UnityEngine;

public class InteractionDesk : MonoBehaviour, IInteractable
{
    public void OnInteractStart(PlayerInteraction player)
    {
        Debug.Log("Le player \"" + player.name + "\" a intéragit avec \"" + gameObject.name + "\" !");
    }
}
