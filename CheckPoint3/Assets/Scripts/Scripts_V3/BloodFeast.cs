using UnityEngine;

[CreateAssetMenu(menuName = "Skills/Berserk/BloodFeast")]
public class BloodFeast : Skill
{
    [Header("Blood Feast Settings")]
    [Range(0f, 1f)]
    public float healPercent = 0.25f; // 25% de la vida m�xima


    [Header("VFX")]
    public GameObject bloodEffectPrefab; // Part�culas de sangre
    public Vector3 effectOffset = Vector3.up; // Offset para que aparezca en el torso
    public override void Activate(PlayableCarrier user)
    {
        int healAmount = Mathf.RoundToInt(user.Health.MaxValue * healPercent);

        int finalHeal = Mathf.Min(healAmount, user.Health.MaxValue - user.Health.CurrentValue);
        user.Health.AffectValue(finalHeal);

        Debug.Log($"{user.name} recuper� {finalHeal} de vida con Blood Feast ({healPercent * 100}% de la vida m�xima).");


        // Spawn de part�culas si hay prefab
        if (bloodEffectPrefab != null)
        {
            GameObject effect = GameObject.Instantiate(
                bloodEffectPrefab,
                user.transform.position + effectOffset,
                Quaternion.identity, user.transform
            );

            // Destruir el prefab despu�s de un tiempo (seg�n duraci�n del sistema de part�culas)
            ParticleSystem ps = effect.GetComponent<ParticleSystem>();
            if (ps != null)
            {
                GameObject.Destroy(effect, ps.main.duration + ps.main.startLifetime.constantMax);
            }
            else
            {
                GameObject.Destroy(effect, 2f); // fallback por si no es un ParticleSystem
            }
        }
    }
}
