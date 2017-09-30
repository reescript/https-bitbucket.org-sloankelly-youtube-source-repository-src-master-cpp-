using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuncExample : MonoBehaviour 
{

    public Dropdown dropDown;

    void Start()
    {
        string[] family = new string[] { "Fred", "Wilma", "Betty", "Barney" };

        var familyList = family.Select(s => new Dropdown.OptionData() { text = s })
                               .ToList();

        dropDown.AddOptions(familyList);
    }
}
