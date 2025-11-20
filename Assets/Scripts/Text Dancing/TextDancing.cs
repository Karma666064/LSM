using TMPro;
using UnityEngine;

public class TextDancing : MonoBehaviour
{
    [SerializeField] GameObject paper;
    [SerializeField] string targetText;

    void ShowPaper()
    {
        paper.SetActive(true);
    }

    void MakeTextDancing()
    {
        TextMeshProUGUI text = gameObject.GetComponent<TextMeshProUGUI>();

        text.text = targetText;
    }
}
