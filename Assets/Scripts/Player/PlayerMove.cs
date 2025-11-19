using UnityEngine;
using Terresquall;

public class PlayerMove : MonoBehaviour
{
    PlayerState ps;
    Rigidbody2D rb;

    [SerializeField] float speed;
    
    public bool canMove = true;
    public enum Facing { Left, Right, Up, Down }
    public Facing facing = Facing.Right;

    private void Start()
    {
        ps = GetComponent<PlayerState>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (canMove && !ps.isDialoging)
        {
            Vector2 direction = new Vector2(
                VirtualJoystick.GetAxis("Horizontal"),
                VirtualJoystick.GetAxis("Vertical")
            ).normalized;

            // Application du mouvement
            rb.linearVelocity = direction * speed;

            // Set facing variable selon si on se déplace ou non
            if (direction.magnitude != 0) ps.isMoving = true;
            else ps.isMoving = false;

            // Set facing variable selon la direction
            if (ps.isMoving)
            {
                if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
                {
                    if (direction.x > 0) facing = Facing.Right;
                    else facing = Facing.Left;
                }
                else
                {
                    if (direction.y > 0) facing = Facing.Up;
                    else facing = Facing.Down;
                }
            }
        }
    }

    public void OnDrawGizmos()
    {
        Vector3 start = transform.position;
        Vector3 dir = Vector3.right;
        float length = gameObject.GetComponent<PlayerInteraction>().distance;

        switch (facing)
        {
            case Facing.Up:
                dir = Vector3.up;
                break;
            case Facing.Down:
                dir = Vector3.down;
                break;
            case Facing.Left:
                dir = Vector3.left;
                break;
            case Facing.Right:
                dir = Vector3.right;
                break;
        }

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(start, start + dir * length);
    }
}
