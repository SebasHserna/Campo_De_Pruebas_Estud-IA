using System.Collections.Generic;
using UnityEngine;

public abstract class PlayableCarrier : Carrier
{
    [Header("PLAYABLE")]
    [Header("Parameters")]
    [SerializeField, Min(1)] private int maxMana = 1;
    
    [SerializeField] private FillType manaFillType = FillType.Instant;

    [Header("Skills")]
    [SerializeField] public List<Skill> Skill = new List<Skill>();

    public Health health => _health; // del Carrier
    private Mana _mana;
    public Mana Mana => _mana;


    protected override void Awake()
    {
        base.Awake();

        // Health del Carrier base
        _health = new Health(0, maxHealth);

        // Mana propio de PlayableCarrier
        _mana = new Mana(0, maxMana, manaFillType, true);

        // Resetear cooldowns de skills
        if (Skill != null)
        {
            foreach (var s in Skill)
            {
                if (s != null) s.lastUseTime = -Mathf.Infinity;
            }
        }
    }

    // Propiedad pública de solo lectura para que otros scripts puedan consultarla



    

    private void Start()
    {
       
    }

    // Lógica de uso de skill por tipo
    public virtual void UseSkill()
    {
        if (Skill == null || Skill.Count == 0)
        {
            Debug.Log($"{gameObject.name} no tiene skills asignadas.");
            return;
        }

        Skill skillToUse = Skill.Find(s => s != null);
        if (skillToUse == null)
        {
            Debug.LogWarning($"{gameObject.name} no tiene la skill");
            return;
        }

        if (!skillToUse.CanUse(this))
        {
            Debug.Log($"{gameObject.name} no puede usar {skillToUse.skillName}");
            return;
        }

        skillToUse.UseSkill(this); // Consume Mana/Health según corresponda
    }
}


