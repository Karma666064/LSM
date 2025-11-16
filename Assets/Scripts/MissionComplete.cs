using UnityEngine;
using UnityEngine.SceneManagement;

public class MissionComplete : MonoBehaviour
{
    private const string StartSceneName = "MTestScene";

    public void EndScenarioAndAdvance()
    {
        if (ProfileManager.Instance == null) return;

        // 1. Demande au Manager de calculer le profil suivant et de l'enregistrer en mémoire.
        PlayerProfile nextProfile = ProfileManager.Instance.GetNextProfileInSequence();

        if (nextProfile == PlayerProfile.Complete)
        {
            // 2. Si c'est la fin de la séquence de jeu
            Debug.Log("Tous les scénarios sont terminés. Charger l'écran de fin.");
            // SceneManager.LoadScene("FinalScreen"); // Chargez l'écran de fin
        }
        else
        {
            // 3. Recharger la scène pour démarrer le nouveau scénario (ex: Burnout)
            Debug.Log("Démarrage du scénario suivant : " + nextProfile.ToString());
            SceneManager.LoadScene(StartSceneName);
        }
    }
}