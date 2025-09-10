using UnityEngine;
using UnityEngine.UI;

public class SkillSlotUI : MonoBehaviour
{


    private Skill assignedSkill;
    private PlayableCarrier user;

    public void AssignSkill(Skill skill, PlayableCarrier carrier)
    {
        assignedSkill = skill;
        user = carrier;
    }

    

    private void Update()
    {
        if (assignedSkill == null || user == null) return;

        // Mostrar el cooldown en el overlay
        
        // Ejemplo: vincular teclas 1, 2, 3 con las skills en orden
        if (Input.GetKeyDown(KeyCode.Alpha1) && IsAssignedSkill(0))
            TryUseSkill();

        if (Input.GetKeyDown(KeyCode.Alpha2) && IsAssignedSkill(1))
            TryUseSkill();

        if (Input.GetKeyDown(KeyCode.Alpha3) && IsAssignedSkill(2))
            TryUseSkill();
    }

    private bool IsAssignedSkill(int index)
    {
        return user.Skill.Count > index && user.Skill[index] == assignedSkill;
    }

    private void TryUseSkill()
    {
        if (!assignedSkill.CanUse(user))
        {
            Debug.Log($"{user.name} no puede usar {assignedSkill.skillName}");
            return;
        }

        assignedSkill.UseSkill(user);
    }
}
