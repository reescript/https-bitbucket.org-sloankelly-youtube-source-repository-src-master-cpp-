public class DoubleDigitConverter : IValueConverter
{
    public object Convert(object value)
    {
        string output = string.Format("{0:00}", int.Parse(value.ToString()));
        return output;
    }    
}
