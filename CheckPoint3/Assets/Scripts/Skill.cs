using UnityEngine;

[CreateAssetMenu(fileName = "NewSkill", menuName = "Skills/Skill")]
public class Skill : ScriptableObject
{
    public enum SkillType { BasicAttack, Fireball, Heal, Rage, Shield }
    [Header("General Info")]
    public string skillName;
    public SkillType type;
    public Sprite icon;
    [Header("Stats")]
    public int healthCost;
    public bool requiresHealth;
    public int manaCost;
    public bool requiresMana;
    public float cooldown;
    public float damage;

    // No serializamos esto para evitar que el valor persista en el asset entre sesiones
    [System.NonSerialized]
    public float lastUseTime = -Mathf.Infinity;

    private void OnEnable()
    {
        // Asegura que al empezar el juego la skill esté "lista"
        lastUseTime = -Mathf.Infinity;
    }

    // ¿Listo por cooldown solamente?
    public bool IsReady()
    {
        return Time.time >= lastUseTime + cooldown;
    }
    public bool CanUse(PlayableCarrier user)
    {
        if (!IsReady()) return false; // cooldown
        if (requiresMana && user.Mana.CurrentValue < manaCost) return false; // mana
        if (requiresHealth && user.Health.CurrentValue < healthCost) return false; // health
        return true;
    }
    public float GetRemainingCooldown()
    {
        float remaining = (lastUseTime + cooldown) - Time.time;
        return remaining > 0f ? remaining : 0f;
    }
    public void Activate(PlayableCarrier user)
    {
        lastUseTime = Time.time;

        // Aquí puedes programar efectos generales o dejarlo en blanco
        Debug.Log($"{user.name} activó la habilidad {skillName}");
    }



    public void UseSkill(PlayableCarrier user)
    {
        if (!IsReady())
        {
            Debug.Log($"{user.name} no puede usar {skillName}, cooldown restante: {GetRemainingCooldown()}s");
            return;
        }

        if (requiresMana && user.Mana.CurrentValue < manaCost)
        {
            Debug.Log($"{user.name} no tiene suficiente mana para usar {skillName}");
            return;
        }

        // Consume recursos según corresponda
        if (requiresMana)
            user.Mana.AffectValue(-manaCost);

        if (requiresHealth && user.Health.CurrentValue < healthCost)
        {
            Debug.Log($"{user.name} no tiene suficiente vida para usar {skillName}");
            return;
        }

        if (requiresHealth)
            user.Health.AffectValue(-healthCost);

        Activate(user);

    }
}
