using UnityEngine;

[CreateAssetMenu(menuName = "SkillEffect/RawDamage")]
public class RawDamage : SkillEffect
{
    public int damagePoints; // Da�o total a aplicar
    public float duration;         // Tiempo en segundos para aplicar el da�o
    public int ticks;               // N�mero de "golpes" en los que se reparte el da�o

    public override void Apply(GameObject player, GameObject target)
    {
        if (this.applyToTarget)
            this.applyTo = target;
        else
            this.applyTo = player;

        // Buscar un Carrier en el objeto
        Carrier carrier = this.applyTo.GetComponent<Carrier>();
        if (carrier != null)
        {
            // Si es un NPC
            NonPlayableCarrier npc = carrier as NonPlayableCarrier;
            if (npc != null)
            {
                npc.TakeDamageOverTime(damagePoints, duration, ticks);
                return;
            }

            // Si en el futuro tienes otro tipo de Carrier (ej: PlayerCarrier)
            // puedes poner aqu� el tratamiento respectivo
        }
        else
        {
            Debug.LogWarning($"[{name}] No se encontr� un Carrier en {this.applyTo.name}");
        }
    }
}

      

