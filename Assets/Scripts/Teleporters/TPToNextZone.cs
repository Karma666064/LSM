using UnityEngine;

public class TPToNextZone : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    [SerializeField] GameObject tpPoint;

    bool canTeleport = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && canTeleport)
        {
            collision.transform.position = tpPoint.transform.position;

            if (gameObject.name == "Trigger To Street") mainCamera.orthographicSize = 9f;
            if (gameObject.name == "Trigger To Workplace") mainCamera.orthographicSize = 7f;

            canTeleport = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canTeleport = true;
        }
    }
}
