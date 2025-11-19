using TMPro;
using UnityEngine;

public class InteractionPaper : MonoBehaviour, IInteractable
{
    [SerializeField] TextMeshProUGUI textTask; 

    public void OnInteractStart(PlayerInteraction player)
    {
        if (gameObject.CompareTag("Paper"))
        {
            player.GetComponent<PlayerInventory>().AddObject(InventoryObject.Name.Paper, 1);

            int papersCount = player.GetComponent<PlayerInventory>().GetCountOfObjectByName(InventoryObject.Name.Paper);

            textTask.text = "Récupère tous les documents\r\n" + papersCount +" / 5";

            Destroy(gameObject);
        }
    }
}
