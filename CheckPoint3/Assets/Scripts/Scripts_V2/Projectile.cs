using UnityEngine;

public class Projectile : MonoBehaviour
{
  
    public ProjectileSkill projectileSkill;
    public GameObject fireExplosion;

    private void Start()
    {
        // Autodestruir después de 5 segundos si no impacta
        Destroy(gameObject, 5f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log($"Hit enemy! Speed: {this.projectileSkill.speed}");
        }
        else
        {
            Debug.Log($"Collided with {collision.gameObject.name}");
        }

        // Instanciar explosión
        if (fireExplosion != null)
        {
            GameObject explosion = Instantiate(fireExplosion, transform.position, transform.rotation);
            Destroy(explosion, 0.5f);
        }

        // Destruir proyectil SIEMPRE al chocar
        Destroy(gameObject);
    }

}
