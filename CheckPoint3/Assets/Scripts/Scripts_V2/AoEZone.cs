using UnityEngine;

public class AoEZone : MonoBehaviour
{
    private GameObject owner;          // Quien lanzó la habilidad
    private float radius;              // Radio del AoE
    private float duration;            // Duración total
    private SkillEffect[] effects;     // Efectos que aplica (ej: RawDamage)

    private float timer = 0f;          // Tiempo transcurrido
    private SphereCollider sphere;     // Collider de área

    /// <summary>
    /// Inicializa la zona de efecto
    /// </summary>
    public void Init(GameObject owner, float radius, float duration, SkillEffect[] effects, bool applyImmediately = true)
    {
        this.owner = owner;
        this.radius = radius;
        this.duration = duration;
        this.effects = effects;

        // Configurar collider
        sphere = GetComponent<SphereCollider>();
        if (sphere == null)
            sphere = gameObject.AddComponent<SphereCollider>();

        sphere.isTrigger = true;
        sphere.radius = radius;

        
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= duration)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == owner) return;        // no se aplica al caster
        if (!other.CompareTag("Enemy")) return;       // filtra por tag (ajustable)

        ApplyEffectsTo(other.gameObject);
    }

    // helper que aplica todos los efectos al target
    private void ApplyEffectsTo(GameObject target)
    {
        if (effects == null || effects.Length == 0) return;

        foreach (var eff in effects)
        {
            if (eff == null) continue;
            // usa la firma Apply(GameObject player, GameObject target)
            eff.Apply(owner, target);
        }
    }
}
