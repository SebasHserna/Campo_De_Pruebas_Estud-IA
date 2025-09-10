using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarrierUI : MonoBehaviour
{
    [Header("Sliders")]
    public Slider healthSlider;
    public Slider manaSlider;
    public Text healthText; // nuevo
    public Text manaText;   // nuevo



    [Header("Skills")]
    public List<Image> skillIcons; // asigna en el inspector
    public List<Text> cooldownTexts;
    public List<Image> skillOverlays; // Imágenes tipo Filled para cada skill

    [Header("Target Carrier")]
    public PlayableCarrier targetCarrier;

    void Start()
    {
        if (targetCarrier == null)
            Debug.LogError("No se asignó targetCarrier en CarrierUI");
    }

    void Update()
    {
        if (targetCarrier == null) return;

        {
            healthSlider.maxValue = targetCarrier.Health.MaxValue;
            healthSlider.value = targetCarrier.Health.CurrentValue;

            manaSlider.maxValue = targetCarrier.Mana.MaxValue;
            manaSlider.value = targetCarrier.Mana.CurrentValue;

            if (healthText != null)
                healthText.text = $"{targetCarrier.Health.CurrentValue} / {targetCarrier.Health.MaxValue}";
            if (manaText != null)
                manaText.text = $"{targetCarrier.Mana.CurrentValue} / {targetCarrier.Mana.MaxValue}";

        }
        // Actualiza Skills
        for (int i = 0; i < targetCarrier.Skill.Count; i++)
        {
            var skill = targetCarrier.Skill[i];
            if (skillIcons.Count > i && skillIcons[i] != null)
                skillIcons[i].sprite = skill.icon;

            if (cooldownTexts.Count > i && cooldownTexts[i] != null)
            {
                float remaining = skill.GetRemainingCooldown();
                cooldownTexts[i].text = remaining > 0 ? remaining.ToString("F1") + "s" : "";
            }
            // Overlay de cooldown
            if (skillOverlays.Count > i && skillOverlays[i] != null)
            {
                float remaining = skill.GetRemainingCooldown();
                float cd = skill.cooldown > 0 ? skill.cooldown : 1f; // evita dividir por 0
                skillOverlays[i].fillAmount = remaining > 0 ? remaining / cd : 0f;
            }
        }
    }
}
