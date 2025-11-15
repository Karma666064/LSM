using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;

public class SettingsManager : MonoBehaviour
{
    // Réf. pour le Contraste 
    public Image[] backgroundImagesToControl;
    public Color HighContrastColor = Color.black;
    public Color DefaultColor = Color.grey;
    
    // Réf. pour la Police
    public TMP_FontAsset mainFontAsset;
    public TMP_FontAsset highContrastFontAsset;
    
    // Réf. pour la Taille de Texte 
    public TextMeshProUGUI[] allUITextComponents;
    private float[] initialFontSizes;
    public static float TextSizeMultiplier = 0.0f;

    // Réf. pour l'Audio
    public AudioMixer masterMixer;
    
    // Réf. pour singleton
    public static SettingsManager Instance;

    // Réf. pour sauvegarde
    [Header("Ref. pour sauvegarde")]
    public Toggle highContrastToggle;
    public Toggle simpleFontToggle;
    public Slider textSizeSlider;
    public Toggle hapticsToggle;
    public Slider masterVolumeSlider;
    public Slider sfxVolumeSlider;

    private void Awake()
    {
        // Logique de persistance
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

        // Trouve tous les composants de texte d'UI (UGUI)
        allUITextComponents = FindObjectsByType<TextMeshProUGUI>(FindObjectsInactive.Include, FindObjectsSortMode.None);

        // Initialise le tableau pour stocker les tailles de police d'origine
        initialFontSizes = new float[allUITextComponents.Length];

        // Stocke la taille initiale (DOIT être fait AVANT LoadSettings)
        for (int i = 0; i < allUITextComponents.Length; i++)
        {
            if (allUITextComponents[i] != null)
            {
                initialFontSizes[i] = allUITextComponents[i].fontSize;
                // Désactiver AutoSize ici, si ce n'est pas fait manuellement dans l'Inspector,
                // pour garantir que le script a le contrôle total de la taille.
                allUITextComponents[i].enableAutoSizing = false;
            }
        }

        Debug.Log("Composants de texte TMP trouvés : " + allUITextComponents.Length);

        // CHARGEMENT DES RÉGLAGES
        LoadSettings(); // Chargement des réglages sauvegardés au démarrage
    }

    // ---------- SECTION VISUELLE ----------

    // 1. Contrôle de la Police (Toggle)
    public void SetSimpleFont(bool isSimple)
    {
        TMP_FontAsset targetFont = isSimple ? highContrastFontAsset : mainFontAsset;

        if (targetFont == null) return; // Sécurité

        // Parcourir TOUS les textes et appliquer la police
        for (int i = 0; i < allUITextComponents.Length; i++)
        {
            if (allUITextComponents[i] != null)
            {
                allUITextComponents[i].font = targetFont;
                // IMPORTANT : Réappliquer la taille après avoir changé la police
                allUITextComponents[i].fontSize = initialFontSizes[i] + TextSizeMultiplier;
            }
        }
        Debug.Log("Police changée : " + (isSimple ? "Simplifiée" : "Standard"));

        // Sauvegarde
        PlayerPrefs.SetInt("Setting_Font", isSimple ? 1 : 0);
        SaveSettings();
    }

    // 2. Contrôle du Contraste (Toggle)
    public void SetHighContrast(bool isHighContrast)
    {
        Color targetColor = isHighContrast ? HighContrastColor : DefaultColor;
        targetColor.a = 1.0f;
        foreach (Image bg in backgroundImagesToControl)
        {
            if (bg != null)
            {
                bg.color = targetColor;
            }
        }
        Debug.Log("Contraste : " + isHighContrast);

        // Sauvegarde
        PlayerPrefs.SetInt("Setting_Contrast", isHighContrast ? 1 : 0);
        SaveSettings();
    }

    // 3. Contrôle de la Taille du Texte (Slider)
    public void ApplyTextSizeToAll()
    {
        if (allUITextComponents == null || initialFontSizes == null || allUITextComponents.Length == 0)
        {
            // Peut survenir si appelé trop tôt ou si aucun texte n'est trouvé.
            Debug.LogError("Les composants de texte n'ont pas été initialisés correctement !");
            return;
        }

        for (int i = 0; i < allUITextComponents.Length; i++)
        {
            if (allUITextComponents[i] != null)
            {
                // CORRECTION : Addition de la valeur du Slider à la taille initiale
                allUITextComponents[i].fontSize = initialFontSizes[i] + TextSizeMultiplier;
            }
        }
    }

    // Fonction appelée par le Slider (Value sera entre 0 et N - Le delta à ajouter)
    public void SetTextSizeMultiplier(float sliderValue)
    {
        // TextSizeMultiplier est directement le delta (l'augmentation en points)
        // Si votre slider va de 0 à 10, alors TextSizeMultiplier va de 0 à 10 points.
        TextSizeMultiplier = sliderValue;

        // Applique immédiatement la nouvelle taille aux textes visibles
        ApplyTextSizeToAll();

        Debug.Log("Taille du texte réglée à : +" + TextSizeMultiplier.ToString("F1") + " pts");

        // Sauvegarde
        PlayerPrefs.SetFloat("Setting_TextSizeMultiplier", sliderValue);
        SaveSettings();
    }

    // ---------- SECTION AUDIO & CONTRÔLE ----------

    // 4. Contrôle du Volume Général (Slider)
    public void SetMasterVolume(float volume)
    {
        if (volume <= 0.0001f)
        {
            masterMixer.SetFloat("MasterVolume", -80f);
        }
        else
        {
            masterMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
        }

        // Sauvegarde
        PlayerPrefs.SetFloat("Volume_Master", volume);
        SaveSettings();
    }

    public void SetSFXVolume(float volume)
    {
        if (volume <= 0.0001f)
        {
            masterMixer.SetFloat("SFXVolume", -80f);
        }
        else
        {
            masterMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20);
        }

        // Sauvegarde
        PlayerPrefs.SetFloat("Volume_SFX", volume);
        SaveSettings();
    }

    // 5. Feedback Haptique (Toggle)
    public void ToggleHaptics(bool enableHaptics)
    {
        if (enableHaptics)
        {
            Handheld.Vibrate();
        }
        PlayerPrefs.SetInt("HapticsEnabled", enableHaptics ? 1 : 0);
        // Sauvegarde
        SaveSettings();
    }

    // ---------- SECTION SAUVEGARDE ----------

    public void SaveSettings()
    {
        // Sauvegarder les volumes (lecture depuis le Mixer)
        // Les Toggles (Contrast, Font) et TextSizeMultiplier sont déjà mis à jour via PlayerPrefs.SetFloat/SetInt dans leurs fonctions respectives.
        // On n'a pas besoin de les redéfinir ici avec des GetInt.

        PlayerPrefs.Save();
        Debug.Log("Réglages sauvegardés.");
    }

    public void LoadSettings()
    {
        // VOLUME (Défaut : 0.75)
        float masterVol = PlayerPrefs.GetFloat("Volume_Master", 0.75f);
        SetMasterVolume(masterVol);
        if (masterVolumeSlider != null) masterVolumeSlider.value = masterVol;

        // SFX (Défaut : 0.75)
        float sfxVol = PlayerPrefs.GetFloat("Volume_SFX", 0.75f);
        SetSFXVolume(sfxVol);
        if (sfxVolumeSlider != null) sfxVolumeSlider.value = sfxVol;

        // CONTRASTE (Défaut : 0 / Faux)
        bool isHighContrast = PlayerPrefs.GetInt("Setting_Contrast", 0) == 1;
        SetHighContrast(isHighContrast);
        if (highContrastToggle != null) highContrastToggle.isOn = isHighContrast;

        // POLICE (Défaut : 0 / Faux)
        bool isSimpleFont = PlayerPrefs.GetInt("Setting_Font", 0) == 1;
        SetSimpleFont(isSimpleFont);
        if (simpleFontToggle != null) simpleFontToggle.isOn = isSimpleFont;

        // TAILLE DU TEXTE (Défaut : 0.0 / Pas d'augmentation)
        // La valeur chargée est le delta à ajouter (ex: 0 à 10)
        float sliderValue = PlayerPrefs.GetFloat("Setting_TextSizeMultiplier", 0.0f);
        SetTextSizeMultiplier(sliderValue);
        if (textSizeSlider != null) textSizeSlider.value = sliderValue;

        // HAPTIQUE (Défaut : 0 / Faux)
        // Non lié à un Slider, mais important à charger pour l'initialisation du jeu.
        bool hapticsEnables = PlayerPrefs.GetInt("HapticsEnabled", 0) == 1; // On charge juste la valeur, ToggleHaptics n'a pas besoin d'être appelé ici si vous n'avez pas de Toggle pour le représenter au démarrage.
        ToggleHaptics(hapticsEnables);
        if (hapticsToggle != null) hapticsToggle.isOn = hapticsEnables;

        Debug.Log("Réglages chargés.");
    }

    // ---------- SECTION RESET ----------
    public void ResetSetting()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        Debug.Log("Tous les réglages d'accessibilité et audio ont été réinitialisés aux valeurs par défaut.");
        LoadSettings();
    }
}