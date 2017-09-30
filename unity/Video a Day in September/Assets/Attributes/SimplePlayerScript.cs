using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePlayerScript : MonoBehaviour 
{

    [Header("Characteristics")]
    [Tooltip("This is the strength of the armour, 0 is no armour, 100 is max armour")]
    [Range(0, 100)]
    public int armour;

    [Tooltip("The current health of the player, when it reaches 0 the player is dead. 1 represents full health")]
    [Range(0, 1)]
    [ContextMenuItem("Reset to Default", "ResetHealthToDefault")]
    public float health = 0.5f;

    [Header("Player Specifics")]
    [Tooltip("The name of the player")]
    public string playerName = "Name of Player";

    [Multiline(15)]
    [Tooltip("Player's Biography")]
    public string playerInfo = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis ante ex, hendrerit id lorem vel, commodo tempor leo. Proin commodo fermentum dictum. Donec rhoncus a diam sit amet cursus. In blandit sem sit amet luctus posuere. Nullam eleifend facilisis erat, eu facilisis diam efficitur sed. Aenean vitae mi eu justo posuere volutpat. Aenean vitae purus vitae justo maximus lacinia. Aenean eget nisi in turpis luctus ultricies in nec velit. Nullam non volutpat quam. Proin at mauris eget lacus condimentum commodo. Ut scelerisque suscipit mauris vel convallis. Vivamus semper tristique molestie. Ut eu tristique nunc. Sed sagittis sem id justo ullamcorper fermentum. Nam iaculis lectus semper risus finibus, in posuere tellus imperdiet.\n\n";


    private void ResetHealthToDefault()
    {
        health = 0.5f;
    }
}
