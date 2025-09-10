using UnityEngine;

[CreateAssetMenu(menuName = "Skills/Mage/AreaSkill")]
public class AreaSkill_M : Skill
{
    [Header("Area Settings")]
    public GameObject areaPrefab;   // Prefab visual del área
    public float range = 5f;        // Radio del área
    public float distance = 3f;     // Distancia frente al jugador donde aparece
    public float duration = 2f;     // Duración en segundos

    public override void Activate(PlayableCarrier user)
    {
        if (areaPrefab == null)
        {
            Debug.LogError("No se asignó prefab de área en " + skillName);
            return;
        }

        // Tomamos la cámara del jugador
        Camera cam = Camera.main;
        Vector3 forward = cam != null ? cam.transform.forward : user.transform.forward;

        // Solo dirección en el plano XZ (ignoramos Y)
        forward.y = 0f;
        forward.Normalize();

        // punto frente al mago según dirección de cámara
        Vector3 spawnPos = user.transform.position + forward * distance;

        // rotacion hacia adelante según cámara (solo horizontal)
        Quaternion spawnRot = Quaternion.LookRotation(forward);

        // instanciamos el prefab
        GameObject areaInstance = Instantiate(areaPrefab, spawnPos, spawnRot);
        Collider[] hits = Physics.OverlapSphere(spawnPos, range);
        foreach (Collider hit in hits)
        {
            NonPlayableCarrier npc = hit.GetComponent<NonPlayableCarrier>();
            if (npc != null)
            {
                npc.TakeDamage(Mathf.RoundToInt(damage));

                Debug.Log($"→ {npc.name} recibió {damage} de daño por {skillName}");
            }
        }

        Debug.Log($"{user.name} activó {skillName} causando {damage} de daño en área.");

        // Destruir área después de duración
        Object.Destroy(areaInstance, duration);
    }
}
