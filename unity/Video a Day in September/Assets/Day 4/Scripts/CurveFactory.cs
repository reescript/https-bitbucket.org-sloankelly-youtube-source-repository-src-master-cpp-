using UnityEngine;

public static class CurveFactory 
{
    public static AnimationCurve Create(float startValue, float endValue)
    {
        var curve = new AnimationCurve();
        curve.AddKey(0f, startValue);
        curve.AddKey(1f, endValue);

        return curve;
    }
}
