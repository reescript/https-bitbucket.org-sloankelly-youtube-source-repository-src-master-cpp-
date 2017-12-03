using System.Collections;
using UnityEngine;

public class Sheep : MonoBehaviour
{
    float updateDirectionTime;

    Vector3 speedVec;

    public float speed = 4f;

    public SheepState sheepState = SheepState.MovingAround;

    public System.Action SheepEnteredSheerOMatic = () => { };

    public void GetSheared()
    {
        sheepState = SheepState.EnteringShearOMatic;
        var spiralFade = gameObject.AddComponent<SpiralFade>();
        spiralFade.endAction = () =>
        {
            SheepEnteredSheerOMatic();
            Destroy(gameObject);
        };
    }

    public void Fling(Vector3 euler, float speed)
    {
        speedVec = euler * speed;
        sheepState = SheepState.MovingAround;
        GetComponent<Collider2D>().enabled = true;
    }

    public void Captured()
    {
        sheepState = SheepState.HeldInTractorBeam;
        GetComponent<Collider2D>().enabled = false;
    }
    
    private void Start()
    {
        updateDirectionTime = 5 + Random.value * 5;
        speedVec = Random.insideUnitCircle * speed;
        StartCoroutine(RandomDirection());
    }

    private void Update()
    {
        if (sheepState!= SheepState.MovingAround) return;
        transform.position += speedVec * Time.deltaTime;
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        transform.position -= speedVec * Time.deltaTime;
        speedVec *= -1;
    }

    private IEnumerator RandomDirection()
    {
        while (true)
        {
            yield return new WaitForSeconds(updateDirectionTime);
            speedVec = Random.insideUnitCircle * speed;
            updateDirectionTime = 5 + Random.value * 5;
        }
    }
}
