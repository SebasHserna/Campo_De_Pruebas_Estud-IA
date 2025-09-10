using UnityEngine;



public class Projectile : MonoBehaviour
{
    public ProjectileSkill_M projectileSkill;
    public GameObject fireExplosion;

    private void Start()
    {
        // Autodestruir después de 5 segundos si no impacta
        Destroy(gameObject, 5f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Solo reaccionar si el objeto golpeado tiene el tag "Enemy"
        if (collision.gameObject.CompareTag("Enemy"))
        {
            NonPlayableCarrier npc = collision.gameObject.GetComponent<NonPlayableCarrier>();
            if (npc != null)
            {
                npc.TakeDamage(Mathf.RoundToInt(projectileSkill.damage));
                Debug.Log($"Hit {collision.gameObject.name}, daño aplicado: {projectileSkill.damage}");
            }
        }

        // Instanciar partículas de explosión
        if (fireExplosion != null)
        {
            GameObject explosion = Instantiate(fireExplosion, transform.position, transform.rotation);
            Destroy(explosion, 0.5f);
        }

        // Destruir proyectil siempre al impactar
        Destroy(gameObject);
    }
}
