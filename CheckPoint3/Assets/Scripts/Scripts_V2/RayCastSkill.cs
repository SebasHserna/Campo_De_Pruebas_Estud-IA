using UnityEngine;


[CreateAssetMenu(menuName = "Skill/RaycastSkill")]
public class RayCastSkill : SkillV2
{
    public float range;    // Longitud del rayo, alcance de la habilidad

    public override void Fire(GameObject emiter)
    {
        emiter.GetComponent<ActionEmiter>().Cast(new Ray(), this.range);
    }
}
