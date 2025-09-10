using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] private int damage = 10;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float lifeTime = 5f;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        PlayableCarrier target = other.GetComponent<PlayableCarrier>();
        if (target != null)
        {
            target.health.AffectValue(-damage); 
            Destroy(gameObject);
        }
    }
}
