using UnityEngine;

[CreateAssetMenu(menuName = "Skills/Berserk/LeapAttack")]
public class LeapAttackSkill : Skill
{
    public float leapForce = 10f;
    public float damageRadius = 2f;

    public override void Activate(PlayableCarrier user)
    {
        Rigidbody rb = user.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(user.transform.forward * leapForce + Vector3.up * 5f, ForceMode.Impulse);
        }

        // El da�o por aterrizaje idealmente deber�a ejecutarse cuando detectas el suelo
        // pero para simplificar, puedes aplicar da�o inmediatamente a los cercanos:
        Collider[] hits = Physics.OverlapSphere(user.transform.position, damageRadius);
        foreach (var hit in hits)
        {
            if (hit.CompareTag("Enemy"))
            {
                Carrier enemyCarrier = hit.GetComponent<Carrier>();
                if (enemyCarrier != null)
                {
                    // Aseg�rate de usar el m�todo correcto para restar vida
                    if (enemyCarrier is NonPlayableCarrier npc)
                        npc.TakeDamage(Mathf.RoundToInt(damage));
                    else
                        enemyCarrier.Health.AffectValue(-Mathf.RoundToInt(damage));
                }
            }
        }

        Debug.Log($"{user.name} realiz� LeapAttack");
    }
}
