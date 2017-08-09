using UnityEngine;
using UnityEngine.UI;

public class PopulateLabel : MonoBehaviour 
{
    public string[] names;
    public int[] someInts;
    public Text textBox;

	// Use this for initialization
	void Start () 
    {
        textBox.text = names.Random() + someInts.Random();
	}
}
