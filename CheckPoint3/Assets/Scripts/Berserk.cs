using System.Collections.Generic;
using UnityEngine;

public class Berserk : PlayableCarrier
{
    
    
        protected override void Awake()
        {
            base.Awake();
            // Comenzar con la vida máxima
            Health.AffectValue(0);
        }
    public void AddSkill(Skill skill)
    {
        if (skill != null)
            Skill.Add(skill);
    }

    public override void UseSkill()
        {
            // Busca la skill en la lista de la base
            Skill skillToUse = Skill.Find(s => s != null );

            if (skillToUse == null)
            {
                Debug.LogWarning($"{gameObject.name} no tiene la skill" );
                return;
            }

            // Verifica cooldown, mana y health
            if (!skillToUse.CanUse(this))
            {
                Debug.Log($"{gameObject.name} no puede usar {skillToUse.skillName}");
                return;
            }

            // Aplica la skill
            skillToUse.UseSkill(this);
        }
    }