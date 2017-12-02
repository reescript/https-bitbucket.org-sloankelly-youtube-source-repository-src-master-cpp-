using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class PropertyToString : MonoBehaviour
{
    Text text;

    public string outputFormat = "Property Value: {0}";

    [Tooltip("The name of the component that owns the property")]
    public string componentName;

    [Tooltip("The name of the property")]
    public string propertyName = "MyProperty";

    public float updateRate = 2f;

    public GameObject targetObject;
    
    private void Awake()
    {
        text = GetComponent<Text>();
    }

    private IEnumerator Start()
    {
        var componentType = Type.GetType(componentName);
        if (componentType == null)
        {
            Destroy(this);
            yield break;
        }

        var propInfo = componentType.GetProperty(propertyName, System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);
        if (propInfo == null)
        {
            Destroy(this);
            yield break;
        }

        var component = targetObject.GetComponent(componentType);
        if (component == null)
        {
            Destroy(this);
            yield break;
        }

        while (true)
        {
            var propValue = propInfo.GetValue(component, null);
            var stringify = propValue == null ? "<NULL>" : propValue.ToString();

            text.text = string.Format(outputFormat, stringify);
            yield return new WaitForSeconds(updateRate);
        }
    }
}
