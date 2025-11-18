using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject MainMenuPanel;
    public GameObject SettingsPanel;

    [Header("Bouton de Retour des Réglages")]
    public GameObject returnToPauseButton;
    public GameObject returnToMainMenuButton;

    // Réf. pour singleton
    public static UIManager Instance;


    public void Awake()
    {
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
    public void Start()
    {
        Debug.Log("Lancement du jeu SignalMatters !");
    }

   public void StartOnClick()
    {
        SceneManager.LoadScene("MTestScene"); // à modifier
        MainMenuPanel.SetActive(false);
       
        if (PauseManager.Instance != null)
        {
            PauseManager.Instance.SetInGame(true); // Active la touche Escape en GAME
        }
    }

    public void OpenSettingsFromMainMenu()
    {
        MainMenuPanel.SetActive(false);
        SettingsPanel.SetActive(true);

        // --- LOGIQUE D'ACTIVATION DU BOUTON ---

        // 1. On s'assure que le bouton de l'autre contexte est caché
        if (returnToPauseButton != null)
        {
            returnToPauseButton.SetActive(false);
        }

        // 2. On affiche NOTRE bouton de retour
        if (returnToMainMenuButton != null)
        {
            returnToMainMenuButton.SetActive(true);
        }
    }
    public void CloseSettingsToMainMenu()
    {
        MainMenuPanel.SetActive(true );
        SettingsPanel.SetActive(false);
    }

    public void OpenAgefiphLink()
    {
        Application.OpenURL("https://www.agefiph.fr/");
    }

    // Permet de réactiver le MenuPanel si on quitte le jeu et informer le PauseManager que l'on n'est PLUS en jeu
    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("ManagerScene");
        MainMenuPanel.SetActive(true);
        if (PauseManager.Instance != null)
        {
            PauseManager.Instance.SetInGame(false);
        }
    }
}
