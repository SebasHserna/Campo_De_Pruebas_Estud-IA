using System.Collections.Generic;
using UnityEngine;

public class TestCarriers : MonoBehaviour
{
    public Mage magePrefab;
    public Berserk berserkPrefab;

    private Mage mage;
    private Berserk berserk;

    public List<Skill> allSkills; // Arrastra aquí los ScriptableObjects de las skills

    void Start()
    {
        // Instanciamos personajes
        mage = Instantiate(magePrefab);
        berserk = Instantiate(berserkPrefab);

        mage.name = "Mage";
        berserk.name = "Berserk";

        // Asignamos skills
        foreach (var skill in allSkills)
        {
            mage.AddSkill(skill);
            berserk.AddSkill(skill);
        }

        // Mostrar estado inicial
        Debug.Log($"{mage.name} - Health: {mage.Health.CurrentValue}, Mana: {mage.Mana.CurrentValue}");
        Debug.Log($"{berserk.name} - Health: {berserk.Health.CurrentValue}, Mana: {berserk.Mana.CurrentValue}");
    }

    void Update()
    {
        // Tecla 1: Mage usa Fireball
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            mage.UseSkill(Skill.SkillType.Fireball);
        }

        // Tecla 2: Berserk usa Rage
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            berserk.UseSkill(Skill.SkillType.Rage);
        }

        // Mostrar estado en consola
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log($"{mage.name} - Health: {mage.Health.CurrentValue}, Mana: {mage.Mana.CurrentValue}");
            Debug.Log($"{berserk.name} - Health: {berserk.Health.CurrentValue}, Mana: {berserk.Mana.CurrentValue}");
        }
    }
}