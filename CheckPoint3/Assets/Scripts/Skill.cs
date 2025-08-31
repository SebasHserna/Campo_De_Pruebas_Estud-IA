using UnityEngine;

[CreateAssetMenu(fileName = "NewSkill", menuName = "Skills/Skill")]
public class Skill : ScriptableObject
{
    public enum SkillType { BasicAttack, Fireball, Heal, Rage, Shield }

    public string skillName;
    public SkillType type;
    public int manaCost;
    public bool requiresMana;
    public float cooldown;
    public float damage;
    public Sprite icon;

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
    public void Activate(PlayableCarrier user)
    {
        lastUseTime = Time.time;

        // Aquí puedes programar efectos generales o dejarlo en blanco
        Debug.Log($"{user.name} activó la habilidad {skillName}");
    }

    // Tiempo restante de cooldown (0 si está listo)
    public float GetRemainingCooldown()
    {
        float remaining = (lastUseTime + cooldown) - Time.time;
        return remaining > 0f ? remaining : 0f;
    }

    // ¿Listo por cooldown y mana?
    public bool CanUse(float currentMana)
    {
        return IsReady() && (currentMana >= manaCost);
    }

    public void UseSkill()
    {
        lastUseTime = Time.time;
        Debug.Log($"{skillName} usada a las {lastUseTime:F2}s");
    }
}
