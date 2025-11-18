using UnityEngine;
using TMPro; 
using System.Collections;
using System.Collections.Generic; 

// Systeme d'affichage

public class DialogueManager : MonoBehaviour
{
    //PlayerState ps;
    // Réf. Singleton
    public static DialogueManager Instance;

    // Réf. UI à glisser-déposer
    [Header("Références UI")]
    public GameObject dialoguePanel;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;


    // Paramètres d'écriture
    [Header("Paramètre d'écriture")]
    [Range(0.01f, 0.1f)]
    public float typeSpeed = 0.05f;

    private DialogueSequence currentSequence;
    private int currentLineIndex = 0;
    private Coroutine currentTypingCoroutine;
    private bool isTyping = false;

    // Test Transition
    //[Header("Fin de Dialogue")]
    //public GameObject transitionButton;
    //public TextMeshProUGUI transitionButtonTextComponent;

    private void Awake()
    {
        //ps = GetComponent<PlayerState>();

        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
    void Start()
    {
        // On s'assure que le panneau est fermé au démarrage
        if (dialoguePanel != null)
        {
            dialoguePanel.SetActive(false);
        }
       // test transition cache le bouton au début
        //if (transitionButton != null) // Assurez-vous qu'il est désactivé
        //{
        //    transitionButton.SetActive(false);
        //}
    }

    public void StartDialogueSequence(DialogueSequence sequence)
    {
        if (sequence == null || sequence.lines.Count == 0) return;

        currentSequence = sequence;
        currentLineIndex = 0;

        // 1. Répare la référence du texte du bouton si elle est perdue (Problème #1)
        //if (transitionButton != null && transitionButtonTextComponent == null)
        //{
        //    // Cherche le composant texte sur les enfants du bouton et l'assigne de manière persistante.
        //    transitionButtonTextComponent = transitionButton.GetComponentInChildren<TextMeshProUGUI>();
        //    Debug.Log("Référence texte du bouton rétablie.");
        //}

        // 2. Cache le bouton de transition (Essentiel pour chaque nouveau dialogue)
        //if (transitionButton != null)
        //{
        //    transitionButton.SetActive(false);
        //}

        // 3. Active le panneau de dialogue
        dialoguePanel.SetActive(true);

        // 4. Démarre l'affichage de la 1ère ligne
        DisplayNextLine();
    }

    private void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        currentSequence = null;
        currentLineIndex = 0;

        Debug.Log("Sequence de dialogue terminée.");

        // Optionnel : refaire bouger le personnage ? 
        // C'est la ligne magique qui active le bouton tadadadadadaaaaaa
        //if (transitionButton != null)
        //{
        //    transitionButton.SetActive(true);
        //}
        //// UTILISE LA RÉFÉRENCE PUBLIQUE
        //if (transitionButtonTextComponent != null)
        //{
        //    transitionButtonTextComponent.gameObject.SetActive(true);
        //    transitionButtonTextComponent.text = "Continuer l'Aventure";

        //}

    }

    public void DisplayNextLine()
    {
        if (currentLineIndex >= currentSequence.lines.Count)
        {
            EndDialogue();
            return;
        }

        DialogueLine line = currentSequence.lines[currentLineIndex];
        nameText.text = line.characterName;
        dialogueText.text = "";

        if (currentTypingCoroutine != null)
        {
            StopCoroutine(currentTypingCoroutine);
        }
        currentTypingCoroutine = StartCoroutine(TypeLine(line.dialogueText));
    }

    private IEnumerator TypeLine(string fullText)
    {
        //if (!ps.isPausing)
        //{
        //}
        isTyping = true;
        foreach (char letter in fullText.ToCharArray())
        {
            dialogueText.text += letter;
            // Utilisez WaitForSecondsRealtime pour garantir que l'écriture avance
            // même si Time.timeScale est à 0 (menu pause).
            yield return new WaitForSecondsRealtime(typeSpeed);
        }
        isTyping = false;
        currentTypingCoroutine = null;
    }

    public void HandleUserInput()
    {
        if (isTyping)
        {
            // Rôle 1 : Avance Rapide

            // On arrête la coroutine d'écriture
            if (currentTypingCoroutine != null)
            {
                StopCoroutine(currentTypingCoroutine);
            }

            // On affiche le texte COMPLET immédiatement
            dialogueText.text = currentSequence.lines[currentLineIndex].dialogueText;
            isTyping = false;
            currentTypingCoroutine = null;
        }
        else
        {
            // Rôle 2 : Passer à la Ligne Suivante
            currentLineIndex++;
            DisplayNextLine(); // Tente d'afficher la ligne suivante, ou appelle EndDialogue() si c'est la fin
        }
    }
}