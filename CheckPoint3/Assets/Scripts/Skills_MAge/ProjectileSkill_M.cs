using UnityEngine;

[CreateAssetMenu(menuName = "Skills/Mage/ProjectileSkill")]
public class ProjectileSkill_M : Skill
{
    [Header("Projectile Settings")]
    public Rigidbody projectilePrefab;
    public float speed = 20f;

    [Tooltip("Efecto opcional de explosión al impactar")]
    public GameObject fireExplosion;

    public override void Activate(PlayableCarrier user)
    {
        if (projectilePrefab == null)
        {
            Debug.LogError("No se asignó prefab de proyectil en " + skillName);
            return;
        }

        // Buscar ActionEmiter en el usuario
        ActionEmiter emiter = user.GetComponent<ActionEmiter>();
        if (emiter == null)
        {
            Debug.LogError($"{user.name} no tiene ActionEmiter asignado para lanzar {skillName}");
            return;
        }

        // Delega toda la lógica de disparo al ActionEmiter
        emiter.Launch(projectilePrefab, speed, this);

        Debug.Log($"{user.name} lanzó proyectil {skillName}");
    }
}
