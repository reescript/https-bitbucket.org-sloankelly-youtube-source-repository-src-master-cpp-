using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TextBinding : BindingBaseBehaviour
{
    Text text;

    protected override void OnAwake()
    {
        text = GetComponent<Text>();
    }

    protected override void OnPropertyChanged(string propertyName)
    {
        text.text = GetBoundValue<string>();
    }
}
