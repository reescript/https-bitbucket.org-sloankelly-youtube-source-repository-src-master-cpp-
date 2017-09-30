using UnityEngine;
using System.ComponentModel;

public class StringToVector3Converter : TypeConverter
{
	public override bool CanConvertFrom(System.ComponentModel.ITypeDescriptorContext context, System.Type sourceType)
	{
		return true;
	}

	public override object ConvertFrom(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
	{
		string[] elements = value.ToString().Split(",".ToCharArray());

		float x = float.Parse(elements[0]);
		float y = float.Parse(elements[1]);
		float z = float.Parse(elements[2]);

		return new Vector3(x, y, z);
	}
}
