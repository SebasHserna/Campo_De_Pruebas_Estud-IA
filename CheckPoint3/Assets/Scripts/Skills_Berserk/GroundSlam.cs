using UnityEngine;

[CreateAssetMenu(menuName = "Skills/Berserk/GroundSlam")]
public class GroundSlam : Skill
{
    [Header("Ground Slam Settings")]
    public GameObject slamPrefab;
    public float radius = 3f;
    public float duration = 2f;

    public override void Activate(PlayableCarrier user)
    {
        // Crear el prefab en la posición del usuario
        GameObject slamInstance = Instantiate(slamPrefab, user.transform.position, Quaternion.identity);

        // Buscar enemigos dentro del radio
        Collider[] hits = Physics.OverlapSphere(user.transform.position, radius);

        foreach (Collider hit in hits)
        {
            if (hit.CompareTag("Enemy"))
            {
                NonPlayableCarrier npc = hit.GetComponent<NonPlayableCarrier>();
                if (npc != null)
                {
                    npc.TakeDamage((int)damage); // <-- Usa TakeDamage()
                    Debug.Log($"{user.name} hizo {damage} de daño a {npc.name} con Ground Slam");
                }
            }
        }

        // Destruir el prefab tras la duración
        Destroy(slamInstance, duration);
    }
}