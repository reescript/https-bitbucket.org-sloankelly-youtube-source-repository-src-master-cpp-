public class NormalizedIntegerConverter : IValueConverter
{
    public object Convert(object value)
    {
        // Going from 0 -> 100 to 0 -> 1
        float converted = (int.Parse(value.ToString()) / 100f);
        return converted;
    }
}
