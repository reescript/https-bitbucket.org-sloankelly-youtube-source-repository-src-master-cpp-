using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class AddObjectToList : MonoBehaviour
{
    public ListBox listBox;

    public Sprite warningIcon;
    
    public void AddButton_Click()
    {
        var buttons = new string[]
        {
            "Fred",
            "Barney",
            "Wilma",
            "Betty",
            "Pebbles",
            "Bam-bam"
        }
        .Select(name => new Dropdown.OptionData(name))
        .ToList();

        listBox.AddOptions(buttons);
    }    

    public void Confirm_Clear()
    {
        DialogBox.Instance.SetTitle("Delete Item?")
                          .SetText("Are you sure you want to remove that item?")
                          .SetImage(warningIcon)
                          .AddButton("Yes", listBox.RemoveSelected)
                          .AddButton("Remove All", listBox.ClearOptions)
                          .AddCancelButton("No")
                          .Show();
    }
}
