using UnityEngine;

public class ProjectileDamage : MonoBehaviour
{
    private GameObject owner;
    private string targetTag;
    private float damage;
    private float lifeTime;

    public void Init(GameObject owner, string targetTag, float damage, float lifeTime)
    {
        this.owner = owner;
        this.targetTag = targetTag;
        this.damage = damage;
        this.lifeTime = lifeTime;

        Destroy(gameObject, lifeTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == owner) return;
        if (!collision.gameObject.CompareTag(targetTag)) return;

        // Aplica daño directo
        var carrier = collision.gameObject.GetComponent<Carrier>();
        if (carrier != null)
        {
            carrier.Health.AffectValue(-(int)damage);
        }

        Destroy(gameObject);
    }
}
