using UnityEngine;

/// <summary>
/// Background class to draw the appropriate shape for each room. Requires SpriteRenderer component.
/// </summary>
[RequireComponent(typeof(SpriteRenderer))]
public class RoomBackground : MonoBehaviour
{
    #region Private member fields

    // The last room number
    int lastRoomNumber = 0;

    // The sprite renderer component (required for this script)
    SpriteRenderer spriteRenderer;

    #endregion

    #region Public properties

    [Tooltip("The room shape number")]
    public int currentRoomNumber;

    [Tooltip("The background images used to draw the room's shape")]
    public Sprite[] rooms;

    #endregion

    #region Unity messages

    /// <summary>
    /// Cache the SpriteRenderer component.
    /// </summary>
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// Changes the background display if the currentRoomNumber field changes
    /// </summary>
    void Update()
    {
        // Did it change?
        if (lastRoomNumber != currentRoomNumber)
        {
            // Clamp the room number to within the rooms array
            lastRoomNumber = currentRoomNumber % rooms.Length;

            // Set the current sprite to the room shape
            spriteRenderer.sprite = rooms[lastRoomNumber];
        }
    }

    #endregion
}
