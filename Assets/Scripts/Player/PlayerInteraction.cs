using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    PlayerState ps;
    PlayerMove pm;

    [SerializeField] InteractionButton[] buttons;
    public float distance;
    [SerializeField] LayerMask layerTarget;

    bool canInteraction = true;
    bool canCancel = true;

    private void Start()
    {
        ps = GetComponent<PlayerState>();
        pm = GetComponent<PlayerMove>();
    }

    private void Update()
    {
        if (buttons[0].isPressed) OnInteraction();
        else if (!buttons[0].isPressed && !canInteraction) canInteraction = true;

        if (buttons[1].isPressed) OnCancel();
        else if (!buttons[1].isPressed && !canCancel) canCancel = true;
    }

    public void OnInteraction()
    {
        if (canInteraction && !ps.isDialoging)
        {
            Vector2 direction = Vector2.right;

            if (pm.facing == PlayerMove.Facing.Left) direction = Vector2.left;
            if (pm.facing == PlayerMove.Facing.Right) direction = Vector2.right;
            if (pm.facing == PlayerMove.Facing.Up) direction = Vector2.up;
            if (pm.facing == PlayerMove.Facing.Down) direction = Vector2.down;

            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, distance, layerTarget);

            if (hit.collider != null && hit.collider.TryGetComponent<IInteractable>(out var interactable))
            {
                interactable.OnInteractStart(this);
                canInteraction = false;
            }
        }
    }

    public void OnCancel()
    {
        if (canCancel && !ps.isDialoging)
        {
            Debug.Log("Cancel is fucking good!!!");
            // Sortir d'une interaction ou annuler une action
            
            canCancel = false;
        }
    }
}
