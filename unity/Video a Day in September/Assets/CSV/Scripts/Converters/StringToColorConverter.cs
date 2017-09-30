using UnityEngine;

public class StringToColorConverter : System.ComponentModel.TypeConverter
{
	
    public override bool CanConvertFrom(System.ComponentModel.ITypeDescriptorContext context, System.Type sourceType)
    {
        return true;
    }

    public override object ConvertFrom(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
    {
        string[] elements = value.ToString().Split(",".ToCharArray());

        float r = float.Parse(elements[0]);
        float g = float.Parse(elements[1]);
        float b = float.Parse(elements[2]);
        float a = float.Parse(elements[3]);

        return new Color(r, g, b, a);
    }
}
