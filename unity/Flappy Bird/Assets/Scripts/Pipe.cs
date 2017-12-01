using UnityEngine;

public class Pipe : MonoBehaviour
{
    [HideInInspector]
    public bool hit = false;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            hit = true;
        }
    }
}
