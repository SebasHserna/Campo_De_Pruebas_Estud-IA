using UnityEngine;
using static UnityEngine.ParticleSystem;
using System.Collections;

public class NonPlayableCarrier : Carrier
{


    [SerializeField] private GameObject deathParticles; // Prefab de partículas de muerte
    protected override void Awake()
    {

        base.Awake();
        // Inicializa la vida con el maxHealth definido en Carrier
        _health = new Health(0, maxHealth);
    }

    public void TakeDamage(int damage)
    {
        _health.AffectValue(-damage); // usamos negativo para restar vida
        Debug.Log($"{gameObject.name} recibió {damage} de daño. Vida restante: {_health.CurrentValue}");

        if (_health.CurrentValue <= 0)
        {
            Die();
        }
    }


    public void Heal(int amount)
    {
        _health.AffectValue(amount);
        Debug.Log($"{gameObject.name} fue curado en {amount}. Vida actual: {_health.CurrentValue}");
    }


    public void TakeDamageOverTime(int totalDamage, float duration, int ticks)
    {
        StopCoroutine("DamageOverTime");
        StartCoroutine(DamageOverTime(totalDamage, duration, ticks));
    }

    private IEnumerator DamageOverTime(int totalDamage, float duration, int ticks)
    {
        int damagePerTick = Mathf.Max(1, totalDamage / ticks);
        float interval = duration / ticks;
        int totalApplied = 0;

        for (int i = 0; i < ticks; i++)
        {
            TakeDamage(damagePerTick);
            totalApplied += damagePerTick;
            yield return new WaitForSeconds(interval);
        }

       
        

        // seguridad: si quedó algo sin aplicar, se aplica aquí
        int missing = totalDamage - totalApplied;
        if (missing > 0)
            TakeDamage(missing);
    }


    private void Die()
    {
        Debug.Log($"{gameObject.name} ha muerto.");

        if (deathParticles != null)
        {
            GameObject instance = Instantiate(deathParticles, transform.position, Quaternion.identity);
            float maxLifetime = 0f;
            ParticleSystem[] systems = instance.GetComponentsInChildren<ParticleSystem>();
            foreach (var ps in systems)
            {
                var main = ps.main;
                // duration + startLifetime (usamos el valor máximo por si es MinMax)
                float startLifeMax = Mathf.Max(main.startLifetime.constant, main.startLifetime.constantMax);
                float sysLife = main.duration + startLifeMax;
                if (sysLife > maxLifetime) maxLifetime = sysLife;
            }

            // Si no encontramos particle systems, usamos un fallback
            float destroyAfter = (maxLifetime > 0f) ? maxLifetime + 0.1f : 2f;
            Destroy(instance, destroyAfter);
        }


        Destroy(gameObject); // Destruye el NPC
    }
}
