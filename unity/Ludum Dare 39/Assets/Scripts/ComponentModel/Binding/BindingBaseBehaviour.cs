using UnityEngine;
using System.Reflection;
using System.ComponentModel;
using System;

public abstract class BindingBaseBehaviour : MonoBehaviour
{
    PropertyInfo propertyInfo;

    public PropertyChangedBehaviour target;

    public string property;
    
    void Awake()
    {
        propertyInfo = target.GetType().GetProperty(property, BindingFlags.Public | BindingFlags.Instance);
        target.PropertyChanged += Target_PropertyChanged;
        OnAwake();
    }

    private void Target_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (property.Equals(e.PropertyName))
            OnPropertyChanged(e.PropertyName);
    }

    protected T GetBoundValue<T>()
    {
        return (T)Convert.ChangeType(propertyInfo.GetValue(target, null), typeof(T));
    }

    protected T GetBoundValue<T>(IValueConverter converter)
    {
        object intermediate = converter.Convert(propertyInfo.GetValue(target, null));
        return (T)Convert.ChangeType(intermediate, typeof(T));
    }

    protected abstract void OnAwake();
    protected abstract void OnPropertyChanged(string propertyName);
}
