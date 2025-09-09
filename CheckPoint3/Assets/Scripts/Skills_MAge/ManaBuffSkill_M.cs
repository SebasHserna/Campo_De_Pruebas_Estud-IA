using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Skills/Mage/ManaBuffSkill")]
public class ManaBuffSkill : Skill
{
    [Header("Buff Settings")]
    public float boostedRegenRate = 5f;   // cuánto regenera por tick
    public float boostedInterval = 0.5f;  // cada cuánto tiempo
    public float buffDuration = 5f;       // duración del buff

    public override void Activate(PlayableCarrier user)
    {
        if (user == null || user.Mana == null || user.Mana.FillType != FillType.ByTime)
        {
            Debug.Log($"{skillName} no tiene efecto porque {user?.name ?? "caster"} no usa regeneración por tiempo.");
            return;
        }

        user.StartCoroutine(ApplyBuff(user));
    }
    private IEnumerator ApplyBuff(PlayableCarrier user)
    {
        Mana mana = user.Mana;

        // Guardar valores originales
        float originalRate = 1f;
        float originalInterval = 2f;

        // Aplicar buff
        mana.SetRegenValues(boostedRegenRate, boostedInterval);

        Debug.Log($"{user.name} activó {skillName}, regen de mana mejorada.");

        yield return new WaitForSeconds(buffDuration);

        // Restaurar valores
        mana.SetRegenValues(originalRate, originalInterval);

        Debug.Log($"{skillName} terminó, regen de mana volvió a la normalidad.");
    }
}
