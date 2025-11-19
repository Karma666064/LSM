// ResourceDisplayManager.cs

using UnityEngine;
using TMPro;

public class ResourceDisplayManager : MonoBehaviour
{
    // --- Références UI à glisser-déposer ---
    [Header("Panneaux")]
    public GameObject resourceMenuPanel;
    public GameObject contentPanel;
    public GameObject UI_panel;

    [Header("Composants de Texte")]
    public TextMeshProUGUI articleTitle;
    public TextMeshProUGUI articleContent;

    [Header("Assets des Articles (SO)")]
    public ResourceArticleSO dyslexiaScientific;
    public ResourceArticleSO dyslexiaPublic;
    public ResourceArticleSO burnoutScientific;
    public ResourceArticleSO burnoutPublic;


    // --- Logique d'Affichage ---

    // 1. Affiche l'article et cache le menu
    public void DisplayArticle(ResourceArticleSO article)
    {
        if (article == null) return;

        articleTitle.text = article.title;
        articleContent.text = article.content;

        // Assurez-vous d'afficher le panneau de contenu en premier pour éviter le clignotement
        contentPanel.SetActive(true);
        resourceMenuPanel.SetActive(false);
    }

    // 2. Bouton "Retour au Menu"
    public void ReturnToMenuSelection()
    {
        contentPanel.SetActive(false);      // Cache l'article
        resourceMenuPanel.SetActive(true);  // Affiche le menu de sélection
    }

    //3. Bouton "Fermer l'Armoire" (sort de l'UI et reprend le jeu)
    //public void CloseResourceUI()
    //{
    //    // Désactive le panneau parent, ce qui cache tout
    //    this.gameObject.SetActive(false);

    //    // Reprend le temps du jeu
    //    Time.timeScale = 1f;

    //    Debug.Log("Jeu repris.");
    //}

    public void CloseMainMenu()
    {
        UI_panel.SetActive(false);
        Time.timeScale = 1f;
    }
}