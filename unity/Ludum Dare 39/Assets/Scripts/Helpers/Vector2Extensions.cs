using UnityEngine;

public static class Vector2Extensions
{
    public static float AreaSize(this Vector2 vec)
    {
        return vec.x * vec.y;
    }
}
