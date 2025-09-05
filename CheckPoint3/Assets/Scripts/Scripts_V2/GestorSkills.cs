using UnityEngine;

public class GestorSkills : MonoBehaviour
{
    public SkillV2 fireBall;
    public SkillV2 rayo1;
    public SkillV2 AoE;

    void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            this.fireBall.Fire(this.gameObject);
        }
        else if (Input.GetKeyDown("2"))
        {
            this.rayo1.Fire(this.gameObject);
        }

        else if (Input.GetKeyDown("3"))
        {
            this.AoE.Fire(this.gameObject);
        }
    }
}
