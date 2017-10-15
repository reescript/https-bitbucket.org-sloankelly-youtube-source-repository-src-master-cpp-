using UnityEngine;

public class LivesController : MonoBehaviour
{
    public GameObject[] lives;

    public void UpdateLives(int livesLeft)
    {
        for (int i = 0; i < lives.Length; i++)
        {
            lives[i].SetActive(i < livesLeft);
        }
    }
}
