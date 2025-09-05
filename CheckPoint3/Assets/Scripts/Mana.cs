using UnityEngine;

public enum FillType
{
    Instant,
    ByTime
}

public class Mana : Stat
{
    private FillType _fillType;

    public Mana(int minValue, int maxValue, FillType fillType = FillType.Instant, bool iniWithMax = true) : base(minValue, maxValue, iniWithMax)
    {
        _fillType = fillType;
    }

    public override void AffectValue(int value)
    {
        switch(_fillType)
        {
            case FillType.Instant:
                base.AffectValue(value);
                break;
            case FillType.ByTime:
                

                break;
        }
    }
}