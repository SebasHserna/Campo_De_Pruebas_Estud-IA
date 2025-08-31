using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCarriers : MonoBehaviour
{
    public Berserk berserkPlayer;
    public Mage magePlayer;

    void Start()
    {
        ListPlayerSkills(berserkPlayer);
        ListPlayerSkills(magePlayer);

        StartCoroutine(TestBerserkSkills());
        StartCoroutine(TestMageSkills());
    }

    // --- Mostrar todas las skills de un jugador ---
    private void ListPlayerSkills(PlayableCarrier player)
    {
        Debug.Log($"=== Skills de {player.gameObject.name} ===");
        if (player.Skills.Count == 0)
        {
            Debug.Log("No tiene skills asignadas.");
            return;
        }

        foreach (var skill in player.Skills)
        {
            Debug.Log($"{skill.skillName} (Tipo: {skill.type}, ManaCost: {skill.manaCost}, Cooldown: {skill.cooldown}s, Damage: {skill.damage})");
        }
    }

    // --- Test Berserk ---
    private IEnumerator TestBerserkSkills()
    {
        Debug.Log("=== Test Berserk ===");
        for (int i = 0; i < 3; i++)
        {
            berserkPlayer.UseSkill(Skill.SkillType.BasicAttack);
            yield return new WaitForSeconds(1f);
        }
    }

    // --- Test Mage ---
    private IEnumerator TestMageSkills()
    {
        Debug.Log("=== Test Mage ===");
        for (int i = 0; i < 5; i++)
        {
            magePlayer.UseSkill(Skill.SkillType.Fireball);
            yield return new WaitForSeconds(1f); // prueba de cooldown
        }
    }
}