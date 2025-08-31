using UnityEngine;

public abstract class Carrier : MonoBehaviour
{
    [Header("BASE")]
    [Header("Parameters")]
    [SerializeField, Min(1)] private int maxHealth = 1;

    private Health _health;

    public Health Health => _health;

    protected virtual void Awake()
    {
        _health = new Health(0, maxHealth);
    }
}