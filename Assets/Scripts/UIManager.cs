using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject MainMenuPanel;
    public GameObject SettingsPanel;
    
    public void Start()
    {
        Debug.Log("Lancement du jeu SignalMatters !");
    }

   public void StartOnClick()
    {
        SceneManager.LoadScene("MTestScene"); // à modifier
    }

    public void OpenSettings()
    {
        MainMenuPanel.SetActive(false);
        SettingsPanel.SetActive(true);
    }
    public void CloseSettings()
    {
        MainMenuPanel.SetActive(true );
        SettingsPanel.SetActive(false);
    }

    public void OpenAgefiphLink()
    {
        Application.OpenURL("https://www.agefiph.fr/");
    }
}
