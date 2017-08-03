using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    Animator anim;

    public void Walk()
    {
        anim.SetTrigger("PlayerWalking");
    }

    public void Stop()
    {
        anim.SetTrigger("PlayerStopped");
    }

    void Awake()
    {
        anim = GetComponent<Animator>();
    }
}
