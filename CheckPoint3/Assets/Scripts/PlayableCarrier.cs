using System.Collections.Generic;
using UnityEngine;

public abstract class PlayableCarrier : Carrier
{
    [Header("PLAYABLE")]
    [Header("Parameters")]
    [SerializeField, Min(1)] private int maxMana = 1;
    [SerializeField] private FillType manaFillType;

    // Backing field (serializado para que aparezca en el Inspector)
    [SerializeField] private List<Skill> Skill = new List<Skill>();


    protected Health health;
    protected Mana mana;

    public PlayableCarrier(int minHealth, int maxHealth, int minMana, int maxMana)
        : base() // si Carrier tiene constructor vacío
    {
        health = new Health(minHealth, maxHealth);
        mana = new Mana(minMana, maxMana);
    }
    // Propiedad pública de solo lectura para que otros scripts puedan consultarla
    public IReadOnlyList<Skill> Skills => Skill;

    private Mana _mana;
    public Mana Mana => _mana;

    protected override void Awake()
    {
        base.Awake();
        _mana = new Mana(0, maxMana, manaFillType, true);

        // Resetear cooldowns runtime (si usas ScriptableObjects)
        if (Skill != null)
        {
            foreach (var s in Skill)
            {
                if (s != null) s.lastUseTime = -Mathf.Infinity;
            }
        }
    }

    private void Start()
    {
        Debug.Log($"{gameObject.name} tiene {Skill.Count} skills asignadas.");
        foreach (var s in Skill)
        {
            if (s == null)
                Debug.LogWarning($"{gameObject.name} tiene un slot de skill vacío.");
            else
                Debug.Log($"{gameObject.name} skill: {s.skillName}, cooldown {s.cooldown}s, mana cost {s.manaCost}");
        }
    }

    // Lógica de uso de skill por tipo
    public virtual void UseSkill(Skill.SkillType type)
    {
        if (Skill == null || Skill.Count == 0)
        {
            Debug.Log($"{gameObject.name} no tiene skills asignadas.");
            return;
        }

        // Busca la skill que coincida con el tipo solicitado
        Skill skill = Skill.Find(x => x != null && x.type == type);

        if (skill == null)
        {
            Debug.LogWarning($"{gameObject.name} no tiene la skill de tipo {type}.");
            return;
        }

        // Verifica cooldown (usa IsReady() si la tienes)
        if (!skill.IsReady())
        {
            Debug.Log($"{skill.skillName} aún está en cooldown.");
            return;
        }

        // convertir float -> int y comprobar mana
        int cost = Mathf.CeilToInt(skill.manaCost);

        if (_mana.CurrentValue < cost)
        {
            Debug.Log($"{gameObject.name} no tiene suficiente mana para usar {skill.skillName} (coste {cost}).");
            return;
        }

        // aplicar consumo y activar la skill
        _mana.AffectValue(-cost);
        skill.UseSkill(); // o skill.Activate(this) según tu implementación de Skill
        Debug.Log($"{gameObject.name} usó la skill {skill.skillName}. Mana restante: {_mana.CurrentValue}");
    }

    // Helpers públicos para modificar la lista en runtime si lo necesitas
    public void AddSkill(Skill s)
    {
        if (s != null) Skill.Add(s);
    }

    public void RemoveSkill(Skill s)
    {
        if (s != null) Skill.Remove(s);
    }
}
