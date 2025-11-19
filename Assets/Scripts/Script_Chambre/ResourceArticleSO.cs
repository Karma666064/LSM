// RessourceArticleSO.cs
using UnityEngine;

[CreateAssetMenu(fileName = "NewArticle", menuName = "Game Ressource/Article")]
public class ResourceArticleSO : ScriptableObject
{
    public string title;
    [TextArea(10, 20)]
    public string content;
}
