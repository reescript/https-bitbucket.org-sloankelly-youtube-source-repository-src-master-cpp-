using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class AddObjectToList : MonoBehaviour
{
    public ListBox listBox;
    
    public void AddButton_Click()
    {
        var buttons = new string[]
        {
            "Create",
            "A",
            "ListBox",
            "In",
            "Unity",
            "And","Dynamically",
            "Add","And","Remove","Items","Plus","Selection","Too!"
        }
        .Select(name => new Dropdown.OptionData(name))
        .ToList();

        listBox.AddOptions(buttons);
    }    
}
