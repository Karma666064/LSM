using UnityEngine;

public class TestDialogueTrigger : MonoBehaviour
{
    [Header("Séquences par Profil")]
    public DialogueSequenceSO dyslexiaSequence;
    public DialogueSequenceSO burnoutSequence;
    public DialogueSequenceSO wheelchairSequence;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // 1. Vérification des Managers
        if (DialogueManager.Instance == null || ProfileManager.Instance == null)
        {
            Debug.LogError("Les Managers ne sont pas prêts !");
            return;
        }

        // 2. Récupérer le profil ACTUEL depuis le manager
        PlayerProfile currentProfile = ProfileManager.Instance.currentProfileToLoad;

        DialogueSequenceSO sequenceToLoad = null;

        // 3. Sélectionner l'Asset de dialogue en fonction du profil
        switch (currentProfile)
        {
            case PlayerProfile.Dyslexia:
                sequenceToLoad = dyslexiaSequence;
                break;
            case PlayerProfile.Burnout:
                sequenceToLoad = burnoutSequence;
                break;
            case PlayerProfile.Wheelchair:
                sequenceToLoad = wheelchairSequence;
                break;
            default:
                Debug.LogWarning("Profil non géré ou Non, le dialogue ne se lancera pas.");
                return;
        }

        // 4. Lancement du dialogue
        if (sequenceToLoad != null)
        {
            // Convertit la liste des lignes du Scriptable Object en une séquence utilisable par le Manager.
            DialogueSequence tempSequence = new DialogueSequence { lines = sequenceToLoad.lines };
            DialogueManager.Instance.StartDialogueSequence(tempSequence);
        }
    }
}

    