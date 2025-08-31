using UnityEngine;
using System.Collections.Generic;

public class SkillDebugger : MonoBehaviour
{
    public PlayableCarrier mage;
    public PlayableCarrier berserk;

    // Guarda el último lastUseTime visto por skill para detectar ejecuciones nuevas
    private Dictionary<Skill, float> lastSeenUseTime = new Dictionary<Skill, float>();

    private void Update()
    {
        if (mage != null) DebugSkills(mage, "Mage");
        if (berserk != null) DebugSkills(berserk, "Berserk");
    }

    private void DebugSkills(PlayableCarrier carrier, string carrierName)
    {
        if (carrier.Skills == null || carrier.Skills.Count == 0)
        {
            Debug.LogWarning($"{carrierName} no tiene skills asignadas!");
            return;
        }

        foreach (var skill in carrier.Skills)
        {
            if (skill == null)
            {
                Debug.LogError($"{carrierName} tiene un slot de skill vacío (null).");
                continue;
            }

            bool isReady = skill.IsReady();
            bool manaEnough = carrier.Mana != null && carrier.Mana.CurrentValue >= skill.manaCost;
            float remainingCd = skill.GetRemainingCooldown();

            Debug.Log(
                $"[{carrierName}] Skill: {skill.skillName} | " +
                $"Ready: {(isReady ? "Sí" : "No")} | " +
                $"Mana suficiente: {(manaEnough ? "Sí" : "No")} | " +
                $"Mana actual: {(carrier.Mana != null ? carrier.Mana.CurrentValue.ToString() : "N/A")} | " +
                $"RemainingCD: {remainingCd:F2}s"
            );

            // Detectar si la skill se ejecutó nuevo (lastUseTime cambió)
            float prev = 0f;
            lastSeenUseTime.TryGetValue(skill, out prev);
            if (!Mathf.Approximately(skill.lastUseTime, prev))
            {
                // hubo un cambio (ejecución)
                lastSeenUseTime[skill] = skill.lastUseTime;
                if (skill.lastUseTime > -Mathf.Infinity)
                    Debug.Log($"[DEBUG] {carrierName} ejecutó {skill.skillName} a las {skill.lastUseTime:F2}s");
            }
        }
    }
}
