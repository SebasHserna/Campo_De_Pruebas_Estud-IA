using System;
using System.Collections.Generic;
using UnityEngine;

public class Mage : PlayableCarrier

{
    private Mana _mana;
    private List<Skill> _skills;

    public Mage(int minHealth, int maxHealth, int minMana, int maxMana)
         : base(minHealth, maxHealth, maxMana, minMana)
    {
        _mana = new Mana(minMana, maxMana);
        _skills = new List<Skill>(); // Inicializamos lista de habilidades
    }

    public new void AddSkill(Skill skill)
    {
        _skills.Add(skill);
    }

    public void UseSkill(int index)
    {
        if (index < 0 || index >= _skills.Count)
        {
            Debug.Log("Index fuera de rango");
            return;
        }

        Skill skill = _skills[index];

        // Verificamos si tiene suficiente mana
        if (_mana.CurrentValue >= skill.manaCost)
        {
            _mana.AffectValue(-skill.manaCost); // restamos el mana
            skill.Activate(this); // activamos la skill sobre el Mage
        }
        else
        {
            Debug.Log("No hay suficiente mana para usar la habilidad: " + skill.skillName);
        }
    }
}
