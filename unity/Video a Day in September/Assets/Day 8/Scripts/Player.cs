using UnityEngine;

public class Player
{
    private int numItems;

    public float Health { get; set; }

    public float Armour { get; set; }

    public int NumberOfItems { get; set; }

    public void Move() {  Debug.Log("This is the move function that was called"); }

    public void Jump() { Debug.Log("Jump"); }

    public void TakeHit() {Debug.Log("Take Hit"); }

    public Player()
    {
    }
}