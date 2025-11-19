using UnityEngine;

public class InteractionPaper : MonoBehaviour, IInteractable
{
    public void OnInteractStart(PlayerInteraction player)
    {
        if (gameObject.CompareTag("Paper"))
        {
            player.GetComponent<PlayerInventory>().AddObject(InventoryObject.Name.Paper, 1);

            Destroy(gameObject);
        }
    }
}
