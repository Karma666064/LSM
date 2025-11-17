using UnityEngine;
using System.Collections.Generic;

// L'Asset de contenu

[CreateAssetMenu(fileName = "NewDialogue", menuName = "Dialogue/Dialogue Sequence")]
public class DialogueSequenceSO : ScriptableObject
{
    public List<DialogueLine> lines;
}