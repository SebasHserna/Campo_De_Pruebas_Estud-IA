using UnityEngine;

public class DamageArea : MonoBehaviour
{
    public int damage = 10;

    private void OnTriggerEnter(Collider other)
    {
        PlayableCarrier pc = other.GetComponent<PlayableCarrier>();
        if (pc != null)
        {
            pc.Health.AffectValue(-damage);
            Debug.Log($"{pc.name} recibió {damage} de daño. Health: {pc.Health.CurrentValue}");
        }
    }
}
