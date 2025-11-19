using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameManager : MonoBehaviour
{
    private const string MainMenuScene = "ManagerScene";
    public void ReturnToMainMenu()
    {
        // Réinitialise le jeu au premier profil pour la prochaine partie.
        if (ProfileManager.Instance != null)
        {
            ProfileManager.Instance.ResetSequence();
        }

        // Cache le bouton de transition (qui était actif sur le FinalScreen)
        GameObject transitionButton = GameObject.Find("TransitionButton");
        if (transitionButton != null)
        {
            transitionButton.SetActive(false);
            Debug.Log("TransitionButton désactivé.");
        }

        // Réactive le MainMenuPanel
        GameObject Canvas = GameObject.Find("Canvas");
        if (Canvas != null)
        {
            Transform mainMenuPanelTransform = Canvas.transform.Find("MainMenuPanel");

            if (mainMenuPanelTransform != null)
            {
                mainMenuPanelTransform.gameObject.SetActive(true);
                Debug.Log("MainMenuPanel réactivé par EndGameManager!");
            }
            else
            {
                // Ceci est une bonne vérification pour le débogage
                Debug.LogError("Impossible de trouver le MainMenuPanel par son nom !");
            }
        }
        else
        {
            Debug.LogError(" Canvas persistant introuvable. Le ManagerUI a-t-il été détruit ?");
        }
        // Charge la scène de menu principal
        SceneManager.LoadScene(MainMenuScene);
    }
}

