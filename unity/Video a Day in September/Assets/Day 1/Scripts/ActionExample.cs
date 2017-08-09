using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionExample : MonoBehaviour 
{

    public Dropdown dropDown;

    void Start()
    {
        List<string> family = new List<string>();
        family.AddRange(new string[] { "Fred", "Wilma", "Barney", "Betty" });

        family.ForEach((s) => 
        {
            var option = new Dropdown.OptionData() { text = s };
            dropDown.options.Add(option);
        });
    }
}
