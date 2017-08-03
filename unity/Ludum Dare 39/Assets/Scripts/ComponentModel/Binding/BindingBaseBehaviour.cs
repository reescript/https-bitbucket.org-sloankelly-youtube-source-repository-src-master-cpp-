using UnityEngine;
using System.Reflection;
using System.ComponentModel;
using System;

public abstract class BindingBaseBehaviour : MonoBehaviour
{
    PropertyInfo propertyInfo;

    IValueConverter converterInstance;

    public PropertyChangedBehaviour target;

    public string property;

    public string valueConverter;

    void Awake()
    {
        propertyInfo = target.GetType().GetProperty(property, BindingFlags.Public | BindingFlags.Instance);
        target.PropertyChanged += Target_PropertyChanged;

        if (!string.IsNullOrEmpty(valueConverter))
        {
            Type converterType = Type.GetType(valueConverter);
            if (converterType != null)
            {
                converterInstance = (IValueConverter)Activator.CreateInstance(converterType);
            }
        }

        OnAwake();

        var tmp = propertyInfo.GetValue(target, null);

        if (tmp != null)
        {
            Target_PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
    }

    private void Target_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (property.Equals(e.PropertyName))
            OnPropertyChanged(e.PropertyName);
    }

    protected T GetBoundValue<T>()
    {
        object currentValue = propertyInfo.GetValue(target, null);

        if (converterInstance != null)
        {
            currentValue = converterInstance.Convert(currentValue);
        }

        return (T)Convert.ChangeType(currentValue, typeof(T));
    }

    protected T GetBoundValue<T>(IValueConverter converter)
    {
        object intermediate = converter.Convert(propertyInfo.GetValue(target, null));
        return (T)Convert.ChangeType(intermediate, typeof(T));
    }

    protected abstract void OnAwake();
    protected abstract void OnPropertyChanged(string propertyName);
}
