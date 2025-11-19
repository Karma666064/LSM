using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{
    [Header("Palyer Setting")]
    private Rigidbody2D rb;
    public Vector2 moveInput;
    public float moveSpeed = 5f;
    public PlayerInput playerInput;

    [Header("Jump Setting")]
    public float jumpForce = 7f;
    public float groundCheckDistance = 0.1f;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public bool isGrounded;
    public bool isJumping;

    [Header("Dialogue")]
    public DialogueManager manager;
    public bool canPass = false;
    [Header("Dialogue")]
    public bool isInDialogue = false;
    public bool canInteract = false;

    private ArmoireResourceTrigger currentInteractable = null;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        manager = FindFirstObjectByType<DialogueManager>();

    }

    // Update is called once per frame
    void Update()
    {
        // Mouvement appliqué que si le joueur n'est pas  en dialogue
        if (!isInDialogue)
        {
            // Move
            rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocity.y);
            // Jump
            if (isJumping)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                isJumping = false;
            }
        }
        else
        {
            // On s'assure que le joueur est immobile pendant le dialogue
            rb.linearVelocity = Vector2.zero;
        }

        if (groundCheck != null)
        {
            isGrounded = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, groundLayer);
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (!isInDialogue)
        {
            moveInput = context.ReadValue<Vector2>();

            if (context.canceled)
            {
                moveInput = Vector2.zero;
            }
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (!isInDialogue && context.started && isGrounded)
        {
            isJumping = true;
        }
    }



    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.started)
        {
          if(currentInteractable != null)
            {
                currentInteractable.PerformInteraction();
            }
        }
    }

    public void SetCurrentInteractable(ArmoireResourceTrigger trigger)
    {
        currentInteractable = trigger;
    }

    public void ClearCurrentInteractable()
    {
        currentInteractable = null;
    }
}
