using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public AudioSource sfxSource;

    public void PlaySFX(AudioClip clipToPlay)
    {
        if (sfxSource != null && clipToPlay != null)
        {
            sfxSource.PlayOneShot(clipToPlay);
        }
    }
}
