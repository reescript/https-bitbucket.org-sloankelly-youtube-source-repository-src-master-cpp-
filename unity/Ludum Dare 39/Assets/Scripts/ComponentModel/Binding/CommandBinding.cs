using System;
using UnityEngine.UI;

public class CommandBinding : BindingBaseBehaviour
{
    Button button;

    protected override void OnAwake()
    {
        button = GetComponent<Button>();
    }

    protected override void OnPropertyChanged(string propertyName)
    {
        button.onClick.RemoveAllListeners();
        Action newListener = GetBoundValue<Action>();
        button.onClick.AddListener(() => newListener());
    }
}
