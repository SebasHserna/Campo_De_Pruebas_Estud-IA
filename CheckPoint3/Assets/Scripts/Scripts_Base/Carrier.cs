using UnityEngine;

public abstract class Carrier : MonoBehaviour
{
    [Header("BASE")]
    [Header("Parameters")]
    [SerializeField, Min(1)] protected int maxHealth = 1;

    public Health _health;

    public Health Health => _health;

    protected virtual void Awake()
    {
        _health = new Health(0, maxHealth);
    }
}