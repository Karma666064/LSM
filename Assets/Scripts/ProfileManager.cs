using UnityEngine;
public enum PlayerProfile { Dyslexia, Burnout, Wheelchair, Default, Complete, None } // Complete = tous les scénarios sont faits

public class ProfileManager : MonoBehaviour
{
    // Réf. Singleton
    public static ProfileManager Instance;

    [Header("Profil de Joueur Actuel")]
    public PlayerProfile currentProfileToLoad = PlayerProfile.Dyslexia; // Valeur de départ (à changer si on veut mettre un autre handicap' en premier)

    [Header("Séquence de jeu")]
    public PlayerProfile[] gameSequence = { PlayerProfile.Dyslexia, PlayerProfile.Burnout, PlayerProfile.Wheelchair };

    private int currentSequenceIndex = 0; // pour savoir ou nous en sommes dans la séquence

    public PlayerProfile GetNextProfileInSequence()
    {
        currentSequenceIndex++;

        if(currentSequenceIndex >= gameSequence.Length)
        {
            return PlayerProfile.Complete; // Fin de tous les scenarios
        }

        currentProfileToLoad = gameSequence[currentSequenceIndex];
        return currentProfileToLoad;
    }


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        if (gameSequence.Length > 0)
        {
            currentProfileToLoad = gameSequence[0];
        }
    }

    //// Fonction pour définir le prochain scénario (appelée à la fin d'une mission)
    //public void SetNextProfile(PlayerProfile profile)
    //{
    //    nextProfileToLoad = profile;
    //    Debug.Log("Prochain profil défini : " + profile.ToString());
    //}
}
