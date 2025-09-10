using UnityEngine;

[CreateAssetMenu(menuName = "Skills/Berserk/LeapAttack")]
public class LeapAttackSkill : Skill
{
    public float leapForce = 10f;
    public float upwardForce = 5f;
    public float dashDuration = 1f; // Tiempo que dura la embestida


    [Header("Visual Effect")]
    public GameObject leapEffectPrefab; // Partículas al saltar
    public float effectDuration = 2f;   // Tiempo antes de destruirlas
    public override void Activate(PlayableCarrier user)
    {
        Rigidbody rb = user.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(user.transform.forward * leapForce + Vector3.up * upwardForce, ForceMode.Impulse);
        }
        if (leapEffectPrefab != null)
        {
            GameObject effect = Instantiate(leapEffectPrefab, user.transform.position, Quaternion.LookRotation(user.transform.forward));
            Destroy(effect, effectDuration);
        }

        // Activar detector de colisión temporal
        LeapAttackDetector detector = user.gameObject.AddComponent<LeapAttackDetector>();
        detector.Initialize(this, user, dashDuration);

        Debug.Log($"{user.name} realizó LeapAttack");
    }
}

/// <summary>
/// Script temporal que detecta colisiones durante el dash
/// </summary>
public class LeapAttackDetector : MonoBehaviour
{
    private LeapAttackSkill skill;
    private PlayableCarrier user;
    private float lifetime;

    public void Initialize(LeapAttackSkill skill, PlayableCarrier user, float duration)
    {
        this.skill = skill;
        this.user = user;
        lifetime = duration;

        // Se destruye automáticamente al terminar el dash
        Destroy(this, duration);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Carrier enemyCarrier = collision.gameObject.GetComponent<Carrier>();
            if (enemyCarrier != null)
            {
                if (enemyCarrier is NonPlayableCarrier npc)
                    npc.TakeDamage(Mathf.RoundToInt(skill.damage));
                else
                    enemyCarrier.Health.AffectValue(-Mathf.RoundToInt(skill.damage));

                Debug.Log($"{user.name} impactó a {collision.gameObject.name} con LeapAttack");

                //  Opcional: destruir el detector inmediatamente tras el primer golpe
                Destroy(this);
            }
        }
    }
}