using UnityEditor.Timeline;
using UnityEngine;
using UnityEngine.Rendering;

public class ActionEmiter : MonoBehaviour
{
    public Transform mainCam;

    private void Start()
    {
        transform.rotation = this.mainCam.rotation;
    }

    private void Update()
    {
        transform.rotation = this.mainCam.rotation;

    }

    // Metodos publicos del emisor

    // Lanza un proyectil a una velocidad determinada
    public void Launch(Rigidbody projectile,float speed)
    {
        //instanciar un rigidbody creando clones temporales
        Rigidbody projectileInGame = Instantiate(projectile, transform.position, transform.rotation) as Rigidbody;
        // Ignorar colisión con el Player
        Collider playerCollider = GetComponentInParent<Collider>();
        Collider projCollider = projectileInGame.GetComponent<Collider>();
        if (playerCollider != null && projCollider != null)
        {
            Physics.IgnoreCollision(projCollider, playerCollider);
        }

        //Asigna una direccion y velocidad al rigidbody
        projectileInGame.AddForce(transform.forward * speed);
    }

    //Emite un rayo con una longitud determinada
    public void Cast(Ray ray, float range)
    {
        ray.origin = transform.position; //El origen del rayo es la posicion del emisor
        ray.direction = transform.forward; // la direccion del rayo es hacia donde apunta el emisor (hacia adelante)
        Physics.Raycast(ray, range);       //Emitimos el rayo con la distancia dada

        Debug.DrawRay(ray.origin, ray.direction * range, Color.green, 5);
    }
}
