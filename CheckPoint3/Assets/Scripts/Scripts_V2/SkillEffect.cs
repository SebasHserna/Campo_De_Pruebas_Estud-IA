using UnityEngine;

public abstract class SkillEffect : ScriptableObject

{
    public bool applyToTarget;
    protected GameObject applyTo;

    public abstract void Apply(GameObject player, GameObject target);
 
}
