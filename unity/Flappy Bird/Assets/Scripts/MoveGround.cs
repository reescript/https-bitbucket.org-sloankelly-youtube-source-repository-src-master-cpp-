using UnityEngine;

public class MoveGround : MonoBehaviour
{
    public int groundPieceWidth = 154;
    public float speed = 77;

    GameObject caboose;
    GameObject front;
    
    public GameObject ground;
    public GameObject first;
    public GameObject second;
    
    void Start()
    {
        caboose = second;
        front = first;
    }

    void Update()
    {
        ground.transform.position -= new Vector3(speed * Time.deltaTime, 0, 0);

        if (caboose.transform.position.x <= -8)
        {
            front.transform.localPosition = caboose.transform.localPosition + new Vector3(groundPieceWidth, 0, 0);
            GameObject temp = caboose;
            caboose = front;
            front = temp;
        }
    }
}
