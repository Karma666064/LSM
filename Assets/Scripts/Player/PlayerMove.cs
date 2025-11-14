using UnityEngine;
using Terresquall;

public class PlayerMove : MonoBehaviour
{
    Rigidbody2D rb;

    [SerializeField] float speed;
    
    public bool canMove = true;
    bool isMoving;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (canMove)
        {
            Vector2 direction = new Vector2(
                VirtualJoystick.GetAxis("Horizontal"),
                VirtualJoystick.GetAxis("Vertical")
            ).normalized;

            rb.linearVelocity = direction * speed;

            if (direction.magnitude != 0) isMoving = true;
            else isMoving = false;
        }
    }
}
