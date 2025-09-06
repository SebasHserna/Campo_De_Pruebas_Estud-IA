using System;
using System.Collections.Generic;
using UnityEngine;

public class Mage : PlayableCarrier

{
    protected override void Awake()
    {
        base.Awake();
        // Comenzar con la vida máxima
        Health.AffectValue(0);
    }

    // Agregar habilidad usando la lista de PlayableCarrier
    public void AddSkill(Skill skill)
    {
        if (skill != null)
            Skill.Add(skill);
    }

    // Lógica de uso de skills
    public override void UseSkill()
    {
        // Busca la skill en la lista de la base
        Skill skillToUse = Skill.Find(s => s != null);

        if (skillToUse == null)
        {
            Debug.LogWarning($"{gameObject.name} no tiene la skill ");
            return;
        }

        // Verifica si puede usarla (cooldown y recursos)
        if (!skillToUse.CanUse(this))
        {
            Debug.Log($"{gameObject.name} no puede usar {skillToUse.skillName}");
            return;
        }

        // Aplica la skill, consumiendo mana y/o health según corresponda
        skillToUse.UseSkill(this);
    }
}