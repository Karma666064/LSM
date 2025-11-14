using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] InteractionButton[] buttons;

    private void Update()
    {
        if (buttons[0].isPressed) OnInteraction();
        if (buttons[1].isPressed) OnCancel();
    }

    public void OnInteraction()
    {
        Debug.Log("Interaction is fucking good!!!");
        // Lancer un raycast devant le Player pour détecter l'objet, récupérer son script
        // Et executer sa fonction interaction 
    }

    public void OnCancel()
    {
        Debug.Log("Cancel is fucking good!!!");
        // Sortir d'une interaction ou annuler une action
    }
}
