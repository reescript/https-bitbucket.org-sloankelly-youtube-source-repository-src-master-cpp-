using UnityEngine;
using UnityEngine.UI;

public class EchoIndex : MonoBehaviour {
    
	public void IndexChanged (int newIndex)
    {
        var text = GetComponent<Text>();
        text.text = newIndex.ToString();
    }
	
}
