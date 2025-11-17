using UnityEngine;
using System.Collections.Generic;

// Structure pour stocker les infos d'une seule ligne de dialogue
[System.Serializable]
public class DialogueLine
{
    public string characterName;
    [TextArea(3, 10)] // Rend la zone de texte plus grande dans l'Inspector
    public string dialogueText;
}

// Structure pour une conversation complète (séquence de lignes)
[System.Serializable]
public class DialogueSequence
{
    public List<DialogueLine> lines;
}

