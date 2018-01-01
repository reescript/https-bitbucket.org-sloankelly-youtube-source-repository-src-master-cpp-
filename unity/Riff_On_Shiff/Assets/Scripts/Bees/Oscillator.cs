using UnityEngine;

public class Oscillator : MonoBehaviour
{
    float distance;
    float offset;
    float startAngle;
    float startHeight;

    public void UpdateAngle(float angle, float size)
    {
        var d = new Vector3(transform.localPosition.x, 0, transform.localPosition.z).magnitude;
        var offset = MathHelper.Map(d, 0, size, -Mathf.PI, Mathf.PI);
        var a = angle + offset;
        var h = Mathf.Floor(MathHelper.Map(Mathf.Sin(a), -1, 1, 10, 30));

        transform.localScale = new Vector3(1, h, 1);
    }

    public void Init(float distance, float offset, float startAngle, float startHeight)
    {
        this.distance = distance;
        this.offset = offset;
        this.startAngle = startAngle;
        this.startHeight = startHeight;
    }
}
