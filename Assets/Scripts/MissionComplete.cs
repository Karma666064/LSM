using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MissionComplete : MonoBehaviour
{
    private const string StartSceneName = "MTestScene";
    private const string FinalSceneName = "FinalScreen";
    

    public void EndScenarioAndAdvance()
    {
        if (ProfileManager.Instance == null) return;

        // 1. Demande au Manager de calculer le profil suivant et de l'enregistrer en mémoire.
        PlayerProfile nextProfile = ProfileManager.Instance.GetNextProfileInSequence();

        // 1.1 Test Cacher la transition button
        GameObject transitionButton = GameObject.Find("TransitionButton");
        if (transitionButton != null)
        {
            transitionButton.SetActive(false);

            TextMeshProUGUI buttonText = transitionButton.GetComponentInChildren<TextMeshProUGUI>(true); // Le 'true' cherche aussi les inactifs
            if (buttonText != null)
            {
                buttonText.gameObject.SetActive(false);
            }
        }

        if (nextProfile == PlayerProfile.Complete)
        {
            // 2. Si c'est la fin de la séquence de jeu
            Debug.Log("Tous les scénarios sont terminés. Charger l'écran de fin.");
            SceneManager.LoadScene(FinalSceneName); // Chargez l'écran de fin
        }
        else
        {
            // 3. Recharger la scène pour démarrer le nouveau scénario (ex: Burnout)
            Debug.Log("Démarrage du scénario suivant : " + nextProfile.ToString());
            SceneManager.LoadScene(StartSceneName);
        }
    }
}