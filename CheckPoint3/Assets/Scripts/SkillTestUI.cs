using System;
using UnityEngine;
using UnityEngine.UI;

public class SkillTestUI : MonoBehaviour
{
    [Header("Referencias de Jugadores")]
    public Mage mage;
    public Berserk berserk;

    [Header("Sliders")]
    public Slider mageHealthSlider;
    public Slider mageManaSlider;
    public Slider berserkHealthSlider;
    public Slider berserkManaSlider;

    void Start()
    {
        UpdateUI();
    }

    void UpdateUI()
    {
        mageHealthSlider.value = mage.Health.CurrentValue;
        mageManaSlider.value = mage.Mana.CurrentValue;

        berserkHealthSlider.value = berserk.Health.CurrentValue;
        berserkManaSlider.value = berserk.Mana.CurrentValue;
    }

    void Update()
    {
        // Teclas para Mage
        if (Input.GetKeyDown(KeyCode.Alpha1)) UseSkill();
        if (Input.GetKeyDown(KeyCode.Alpha2)) UseSkill();

        // Teclas para Berserk
        if (Input.GetKeyDown(KeyCode.Q)) UseSkill();
        if (Input.GetKeyDown(KeyCode.R)) UseSkill();

        // Actualiza sliders cada frame
        UpdateUI();
    }

    private void UseSkill()
    {
       
    }

    void UseSkill(PlayableCarrier player)
    {
        player.UseSkill();
    }
}
