using Rnd = UnityEngine.Random;

public static class ArrayExtensions
{
    public static T Random<T>(this T[] array)
    {
        if (array == null) return default(T);

        int index = Rnd.Range(0, array.Length);
        return array[index];
    }
}
