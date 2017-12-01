using UnityEngine;

public class PassedThePipe : MonoBehaviour
{
    public GameController gameController;

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag.Equals("Player"))
        {
            gameController.PipeHasBeenPassed();
        }
    }
}
