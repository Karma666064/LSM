using UnityEngine;
using UnityEngine.UI;
using TMPro;
//using UnityEngine.Audio;

public class SettingsManager : MonoBehaviour
{
    // Réf. pour le Contraste 
    public Image[] backgroundImagesToControl;
    public Color HighContrastColor = Color.black; 
    public Color DefaultColor = Color.grey;       

    // Réf. pour la Police
    public TMP_FontAsset mainFontAsset;     // Police standard
    public TMP_FontAsset highContrastFontAsset; // Police DYS 

    // Réf. pour la Taille de Texte 
    // Liste tous les Textes à changer la police dynamiquement
    public TextMeshProUGUI[] allUITextComponents;
    private float[] initialFontSizes;
    public static float TextSizeMultiplier = 1.0f;
    private const float MAX_SIZE_INCREASE = 0.5f;

    private void Awake()
    {
        // Trouve tous les composants de texte d'UI (UGUI)
        allUITextComponents = FindObjectsByType<TextMeshProUGUI>(FindObjectsInactive.Include, FindObjectsSortMode.None);
       
        // Initialise le tableau pour stocker les tailles de police d'origine
        initialFontSizes = new float[allUITextComponents.Length];
        
        // Stocke la taille initiale pour éviter une augmentation exponentielle
        for (int i = 0; i < allUITextComponents.Length; i++)
        {
            if (allUITextComponents[i] != null)
            {
                initialFontSizes[i] = allUITextComponents[i].fontSize;
            }
        }

        Debug.Log("Composants de texte TMP trouvés : " + allUITextComponents.Length);
    }

    // --- SECTION VISUELLE ---

    // 1. Contrôle de la Police (Toggle)
    public void SetSimpleFont(bool isSimple)
    {
        TMP_FontAsset targetFont = isSimple ? highContrastFontAsset : mainFontAsset;

        // Parcourir TOUS les textes et appliquer la police
        foreach (var textComp in allUITextComponents)
        {
            textComp.font = targetFont;
        }
        Debug.Log("Police changée : " + (isSimple ? "Simplifiée" : "Standard"));
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
        // Logique future : changer les couleurs de tous les fonds de l'UI.
        Debug.Log("Contraste : " + isHighContrast);
    }

    // 3. Contrôle de la Taille du Texte (Slider)

    public void ApplyTextSizeToAll()
    {
        // Vérification d'initialisation au cas où
        if (allUITextComponents == null || initialFontSizes == null || allUITextComponents.Length == 0)
        {
            Debug.LogError("Les composants de texte n'ont pas été initialisés correctement !");
            return;
        }

        for (int i = 0; i < allUITextComponents.Length; i++)
        {
            if (allUITextComponents[i] != null)
            {
                // CORRECTION MAJEURE : On utilise la taille INITIALE pour le calcul
                allUITextComponents[i].fontSize = initialFontSizes[i] + TextSizeMultiplier;
            }
        }
    }

    // Fonction appelée par le Slider (Value sera entre 0 et 1)
    public void SetTextSizeMultiplier(float sliderValue)
    {
        // Calcule le multiplicateur réel (Ex: 0 -> 1.0; 1 -> 1.5)
        TextSizeMultiplier = sliderValue;

        // Applique immédiatement la nouvelle taille aux textes visibles
        ApplyTextSizeToAll();

        Debug.Log("Taille du texte réglée à : x" + TextSizeMultiplier.ToString("F2"));
    }

    // --- SECTION AUDIO & COGNITIF ---

    // 4. Contrôle du Volume Général (Slider)
    public void SetMasterVolume(float volume)
    {
        // audioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);

        Debug.Log("Volume Général : " + volume.ToString("F2"));
    }

    // --- SECTION CONTRÔLE ---

    // 5. Feedback Haptique (Toggle)
    public void ToggleHaptics(bool enableHaptics)
    {
        if (enableHaptics)
        {
            // Fait vibrer l'appareil une fois pour confirmer l'activation
            Handheld.Vibrate();
        }
        PlayerPrefs.SetInt("HapticsEnabled", enableHaptics ? 1 : 0);
        Debug.Log("Vibration activée : " + enableHaptics);
    }
}