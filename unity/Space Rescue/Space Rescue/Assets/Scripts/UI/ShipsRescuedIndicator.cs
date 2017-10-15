using UnityEngine;
using UnityEngine.UI;

public class ShipsRescuedIndicator : MonoBehaviour
{
    Text indicatorText;

    public GameController gameController;

    void Awake()
    {
        indicatorText = GetComponent<Text>();    
    }

    void Update ()
    {
        indicatorText.text = gameController.shipsSaved.ToString();
	}
}
