using UnityEngine;



[CreateAssetMenu(menuName = "Skill/ProjectileSkill")]

public class ProjectileSkill : SkillV2
{
    
    public Rigidbody projectile;
    public float speed;


    public override void Fire(GameObject emiter)
    {
        emiter.GetComponent<ActionEmiter>().Launch(this.projectile, this.speed);
    }
}
