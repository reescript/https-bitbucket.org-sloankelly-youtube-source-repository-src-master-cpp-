using UnityEngine;

public class Pipework : MonoBehaviour
{
    public Pipe top;
    public Pipe bottom;

    [HideInInspector]
    public bool hit = false;
    
	void Update ()
    {
		if (top.hit || bottom.hit)
        {
            hit = true;
        }
	}
}
