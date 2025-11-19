using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PauseManager : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject settingsPanel;
    public GameObject returnToPauseButton;

    private bool isPaused = false;
    private bool isSettingsOpen = false;

    // Réf. pour singleton
    public static PauseManager Instance;

    // Réf. pour savoir si on est dans le MainMenu ou non
    private bool isInGame = false;

    private void Awake()
    {
        // Logique de persistance
        if (Instance == null)
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
    // Update is called once per frame
    void Update()
    {
        if(isInGame && Input.GetKeyDown(KeyCode.Escape))
        {
            if(isSettingsOpen)
            {
                ReturnToPauseMenu();
            }
            else if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f; // Arrête le temps
        isPaused = true;
    }

    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f; // Reprend le temps normal
        isPaused = false;
    }

    public void OpenSettings()
    {
        pausePanel.SetActive(false); // Cache le menu pause
        settingsPanel.SetActive(true); // Ouvre le panneau de réglage
        isSettingsOpen = true;

        // --- LOGIQUE D'ACTIVATION DU BOUTON ---

        // 1. On s'assure que le bouton de l'autre contexte est caché
        // Si le bouton de retour au menu principal existe et est une référence publique :
        // if (UIManager.Instance.returnToMainMenuButton != null) UIManager.Instance.returnToMainMenuButton.SetActive(false);

        // 2. On affiche NOTRE bouton de retour
        if (returnToPauseButton != null)
        {
            returnToPauseButton.SetActive(true);
        }
    }

    public void ReturnToPauseMenu()
    {
        settingsPanel.SetActive(false); // Ferme le panneau réglage
        pausePanel.SetActive(true); // Affiche à nouveau le menu Pause
        isSettingsOpen = false;
    }

    public void SetInGame(bool state)
    {
        isInGame = state;
    }


    public void QuiGame()
    {
        //Pour le build finaux
        // Applicatin.Quit()

        Debug.Log("Quitter le jeu...");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
