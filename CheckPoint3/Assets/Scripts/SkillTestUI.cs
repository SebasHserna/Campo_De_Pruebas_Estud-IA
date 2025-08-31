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
        if (Input.GetKeyDown(KeyCode.Alpha1)) UseSkill(mage, Skill.SkillType.Fireball);
        if (Input.GetKeyDown(KeyCode.Alpha2)) UseSkill(mage, Skill.SkillType.Heal);

        // Teclas para Berserk
        if (Input.GetKeyDown(KeyCode.Q)) UseSkill(berserk, Skill.SkillType.Rage);
        if (Input.GetKeyDown(KeyCode.W)) UseSkill(berserk, Skill.SkillType.BasicAttack);

        // Actualiza sliders cada frame
        UpdateUI();
    }

    void UseSkill(PlayableCarrier player, Skill.SkillType type)
    {
        player.UseSkill(type);
    }
}
