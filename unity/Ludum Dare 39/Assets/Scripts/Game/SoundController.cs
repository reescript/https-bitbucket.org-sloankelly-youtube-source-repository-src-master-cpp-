using UnityEngine;

public class SoundController : MonoBehaviour
{
    public AudioSource audio;

    public AudioClip fireWeapon;
    public AudioClip treeDestroyed;
    public AudioClip powerUpRadio;
    public AudioClip radioFull; 
    public AudioClip pickUpBattery;
    public AudioClip wahWah;

    internal void MissionFailed()
    {
        audio.PlayOneShot(wahWah);
    }

    internal void FireWeapon()
    {
        audio.PlayOneShot(fireWeapon);
    }

    internal void TreeDestroyed()
    {
        audio.PlayOneShot(treeDestroyed);
    }

    internal void PickUpBattery()
    {
        audio.PlayOneShot(pickUpBattery);
    }

    internal void PowerUpRadio()
    {
        audio.PlayOneShot(powerUpRadio);
    }

    internal void RadioFull()
    {
        audio.PlayOneShot(radioFull);
    }
}
