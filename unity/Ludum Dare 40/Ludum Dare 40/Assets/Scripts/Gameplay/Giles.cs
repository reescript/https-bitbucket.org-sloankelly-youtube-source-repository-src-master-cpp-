using UnityEngine;

public class Giles : MonoBehaviour
{
    Vector3 lastKnownGood = Vector3.zero;
    float speed = 8f;

    private void Update()
    {
        float x = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        float y = Input.GetAxis("Vertical") * Time.deltaTime * speed;

        lastKnownGood = transform.position;
        transform.position += new Vector3(x, y, 0);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        transform.position = lastKnownGood;
    }
}
