using UnityEngine;

public class ActionEmiter : MonoBehaviour
{
    [Header("References")]
    public Transform firePoint;       // Asigna en el inspector
    public Camera playerCamera;       // Opcional, si quieres apuntar con la cámara

    public void Launch(Rigidbody projectile, float speed, ProjectileSkill_M skill)
    {
        if (firePoint == null)
        {
            Debug.LogError("No hay FirePoint asignado en ActionEmiter de " + gameObject.name);
            return;
        }

        // Instanciar en el FirePoint
        Rigidbody projInstance = Instantiate(projectile, firePoint.position, firePoint.rotation);

        // Referencia a la skill
        Projectile projScript = projInstance.GetComponent<Projectile>();
        if (projScript != null)
            projScript.projectileSkill = skill;

        // Dirección: cámara si existe, si no usa firePoint
        Vector3 direction = playerCamera != null ? playerCamera.transform.forward : firePoint.forward;

        // Empuje
        projInstance.AddForce(direction * speed, ForceMode.Impulse);

        //  Evitar colisión con el lanzador
        Collider myCollider = GetComponent<Collider>();
        Collider projCollider = projInstance.GetComponent<Collider>();
        if (myCollider != null && projCollider != null)
        {
            Physics.IgnoreCollision(projCollider, myCollider);
        }
    }
}
