using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InteractionPNJ : MonoBehaviour, IInteractable
{
    [SerializeField] DialogueSequenceSO dialogText;

    public void OnInteractStart(PlayerInteraction player)
    {
        DialogueSequence dialogList = new DialogueSequence { lines = dialogText.lines };

        DialogueManager.Instance.StartDialogueSequence(dialogList);
    }
}
