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
            Debug.Log($"{pc.name} recibi� {damage} de da�o. Health: {pc.Health.CurrentValue}");
        }
    }
}
