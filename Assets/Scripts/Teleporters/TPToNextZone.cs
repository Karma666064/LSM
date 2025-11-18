using UnityEngine;

public class TPToNextZone : MonoBehaviour
{
    [SerializeField] GameObject tpPoint;

    bool canTeleport = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && canTeleport)
        {
            collision.transform.position = tpPoint.transform.position;
            canTeleport = false;
            Debug.Log("TP to \"" + tpPoint.name + "\"");
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
