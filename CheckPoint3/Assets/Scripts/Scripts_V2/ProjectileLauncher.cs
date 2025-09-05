using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
    
  public void Launch(Rigidbody projectile, float speed)
    {
        Rigidbody projectileInGame = Instantiate(projectile, transform.position, transform.rotation) as Rigidbody;

        projectileInGame.AddForce(transform.forward * speed);
    }


}
