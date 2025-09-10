using UnityEngine;

public class ManaArea : MonoBehaviour
{
    public int manaAmount = 10;

    private void OnTriggerEnter(Collider other)
    {
        PlayableCarrier pc = other.GetComponent<PlayableCarrier>();
        if (pc != null)
        {
            pc.Mana.AffectValue(manaAmount);
            Debug.Log($"{pc.name} recuperó {manaAmount} de mana. Mana: {pc.Mana.CurrentValue}");
        }
    }
}
