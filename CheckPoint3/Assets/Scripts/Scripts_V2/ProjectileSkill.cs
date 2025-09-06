using UnityEngine;



[CreateAssetMenu(fileName = "NewProjectileSkill", menuName = "Skills/Projectile")]

public class ProjectileSkill : SkillV2
{
    
    public Rigidbody projectile;
    public float speed;


    public override void Fire(GameObject emiter)
    {
        emiter.GetComponent<ActionEmiter>().Launch(this.projectile, this.speed);
    }
}
