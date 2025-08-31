using System.Collections.Generic;
using UnityEngine;
using static Skill;

public class Berserk : PlayableCarrier
{

    protected override void Awake()
    {
        base.Awake();
        Health.AffectValue(0);
    }
    [SerializeField] private List<Skill> skills = new List<Skill>();

    public Berserk(int minHealth, int maxHealth, int minMana, int maxMana) : base(minHealth, maxHealth, minMana, maxMana)
    {
    }


    // UseSkill usando el campo 'skills'




    public override void UseSkill(Skill.SkillType type)
    { if (skills == null || skills.Count == 0)
        {
            Debug.Log($"{gameObject.name} no tiene skills asignadas.");
            return;
        }

        Skill skill = skills.Find(s => s != null && s.type == type); // <-- aquí usamos 'skills'

        if (skill == null)
        {
            Debug.LogWarning($"{gameObject.name} no tiene la skill de tipo {type}.");
            return;
        
        }
      
    }
}
