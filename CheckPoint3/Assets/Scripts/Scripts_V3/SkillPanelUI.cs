using UnityEngine;

public class SkillPanelUI : MonoBehaviour
{
    public SkillSlotUI[] slots;
    public PlayableCarrier player;

    private void Start()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < player.Skill.Count && player.Skill[i] != null)
                slots[i].AssignSkill(player.Skill[i], player);
        }
    }
}
