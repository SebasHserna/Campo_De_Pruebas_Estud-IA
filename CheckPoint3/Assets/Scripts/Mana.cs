using UnityEngine;

public enum FillType
{
    Instant,
    ByTime
}

public class Mana : Stat
{
    private FillType _fillType;
  

    // valores de regeneración
    private float regenRate = 1f;       // cuánto regenera
    private float regenInterval = 2f;   // cada cuánto tiempo
    private float regenTimer;

    // guardamos defaults para restaurar después de un buff
    private float defaultRate;
    private float defaultInterval;



    public Mana(int minValue, int maxValue, FillType fillType = FillType.Instant, bool iniWithMax = true) : base(minValue, maxValue, iniWithMax)
    {
        _fillType = fillType;
    }
    public FillType FillType => _fillType;
    public override void AffectValue(int value)
    {
        switch(_fillType)
        {
            case FillType.Instant:
                base.AffectValue(value);
                break;
            case FillType.ByTime:
                base.AffectValue(value);

                break;
        }
    }
    public void Tick(float deltaTime)
    {
        if (_fillType != FillType.ByTime) return;

        regenTimer += deltaTime;
        if (regenTimer >= regenInterval)
        {
            base.AffectValue((int)regenRate);
            regenTimer = 0f;
        }
    }

    // para buffs
    public void SetRegenValues(float rate, float interval)
    {
        regenRate = rate;
        regenInterval = interval;
    }

    public void ResetRegenValues()
    {
        regenRate = defaultRate;
        regenInterval = defaultInterval;
    }

    public FillType GetFillType() => _fillType;
}

