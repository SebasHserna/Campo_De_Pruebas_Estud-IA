using UnityEngine;


public abstract class Skill : ScriptableObject
{
    [Header("General Info")]
    public string skillName;
    public Sprite icon;

    [Header("Costs")]
    public int healthCost;
    public bool requiresHealth;
    public int manaCost;
    public bool requiresMana;
    public float cooldown;

    [Header("Stats")]
    public float damage;

    [System.NonSerialized] public float lastUseTime = -Mathf.Infinity;

    private void OnEnable() => lastUseTime = -Mathf.Infinity;

    public float GetRemainingCooldown()
    {
        float remaining = (lastUseTime + cooldown) - Time.time;
        return remaining > 0f ? remaining : 0f;
    }

    public bool IsReady() => Time.time >= lastUseTime + cooldown;

    public virtual bool CanUse(PlayableCarrier user)
    {
        if (!IsReady()) return false;
        if (requiresMana && user.Mana.CurrentValue < manaCost) return false;
        if (requiresHealth && user.Health.CurrentValue < healthCost) return false;
        return true;
    }

    public void ConsumeResources(PlayableCarrier user)
    {
        if (requiresMana) user.Mana.AffectValue(-manaCost);
        if (requiresHealth) user.Health.AffectValue(-healthCost);
    }

    // Método público que el código del jugador / UI debe invocar
    public void UseSkill(PlayableCarrier user)
    {
        if (!CanUse(user))
        {
            Debug.Log($"{user.name} no puede usar {skillName} (Cooldown/Recursos).");
            return;
        }

        // Consumo de recursos y registro de uso
        ConsumeResources(user);
        lastUseTime = Time.time;

        // Llama al hook de la subclase
        Activate(user);
    }

    // Hook que define el comportamiento concreto de la skill
    public abstract void Activate(PlayableCarrier user);
}
