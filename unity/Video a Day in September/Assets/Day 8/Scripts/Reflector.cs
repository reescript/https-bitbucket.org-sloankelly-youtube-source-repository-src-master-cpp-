using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reflector : MonoBehaviour {

	// Use this for initialization
	void Start () {

        var player = new Player();

        var playerType = typeof(Player);

        PropertyInfo[] properties = playerType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

        foreach(var prop in properties)
        {
            print(prop.Name + " is " + prop.PropertyType.Name);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
