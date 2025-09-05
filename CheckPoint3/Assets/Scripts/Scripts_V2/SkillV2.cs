using UnityEngine;

public abstract class SkillV2 : ScriptableObject

{
    public string SkillName;
    public string target;
    public SkillEffect[] effects;

    public abstract void Fire(GameObject emiter);
    public void ApplyEffects(GameObject player, GameObject target)
    {
        foreach (SkillEffect effect in effects)
        {

            effect.Apply(player, target);

        }
    }


}
