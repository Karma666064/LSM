using UnityEngine;

public class TPToNextZone : MonoBehaviour
{
    [SerializeField] GameObject tpPoint;
    [SerializeField] AudioSource audioManager;
    [SerializeField] AudioClip nextZoneMusic;

    bool canTeleport = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && canTeleport)
        {
            collision.transform.position = tpPoint.transform.position;
            canTeleport = false;
            audioManager.clip = nextZoneMusic;
            audioManager.Play();
            //Debug.Log("TP to \"" + tpPoint.name + "\"");
            //    if (nextZoneMusic != null)
            //    {
            //        nextZoneMusic.Play();
            //    }

            //    if (currentMusic !=null)
            //    {
            //        currentMusic.Stop();
            //    }
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
