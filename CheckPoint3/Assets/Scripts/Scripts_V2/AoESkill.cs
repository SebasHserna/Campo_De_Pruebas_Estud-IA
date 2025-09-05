using UnityEngine;

[CreateAssetMenu(menuName = "Skill/AoESkill")]
public class AoESkill : SkillV2
{
    public GameObject aoePrefab;   // Prefab con partículas + AoEZone (recomendado)
    public float radius = 3f;
    public float duration = 2f;
   

    // Llamada que usa el GestorSkills
    public override void Fire(GameObject emiter)
    {
        // Por defecto lo spawneamos frente al emiter (puedes cambiar lógica)
        Vector3 spawnPos = emiter.transform.position + emiter.transform.forward * radius * 0.5f;
        Fire(emiter, spawnPos);
    }

    // Sobrecarga para cuando quieras dar una posición concreta (raycast al suelo)
    public void Fire(GameObject emiter, Vector3 position)
    {
        if (aoePrefab == null)
        {
            Debug.LogWarning($"AoESkill '{name}': no hay aoePrefab asignado.");
            return;
        }

        // Instanciamos el prefab
        GameObject zoneObj = Instantiate(aoePrefab, position, Quaternion.identity);

        // Intentamos obtener el componente AoEZone
        AoEZone zone = zoneObj.GetComponent<AoEZone>();

        if (zone == null)
        {
            // Si no existe AoEZone, lo añadimos y nos aseguramos de tener un SphereCollider (isTrigger)
            zone = zoneObj.AddComponent<AoEZone>();

            SphereCollider col = zoneObj.GetComponent<SphereCollider>();
            if (col == null)
            {
                col = zoneObj.AddComponent<SphereCollider>();
            }

            col.isTrigger = true;
            col.radius = radius;
        }

        // Llamamos Init (ya debe existir en AoEZone según diseño)
        zone.Init(emiter, radius, duration, effects);

        // Destruir el objeto visual/controlador al terminar la duración (AoEZone también puede autodestruirse)
        Destroy(zoneObj, duration + 0.1f);
    }
}
