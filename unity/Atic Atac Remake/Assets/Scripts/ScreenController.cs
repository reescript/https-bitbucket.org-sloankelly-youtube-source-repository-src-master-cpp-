using UnityEngine;

public class ScreenController : MonoBehaviour
{
    int currentScreen = -1;
    
    [Tooltip("The number of the room to be drawn on screen")]
    public int screenId = -1;

    [Tooltip("The map loader script")]
    public MapLoader mapLoader;

    [Tooltip("This is the image that represents the shape of the room")]
    public RoomBackground roomShape;

    [Tooltip("The background object instances")]
    public RoomBackground[] bobs;

    void Update()
    {
        if (screenId != currentScreen && mapLoader.IsLoaded)
        {
            currentScreen = screenId % mapLoader.Screens.Count;
            DescribeTheRoom(mapLoader.Screens[currentScreen]);
        }
    }

    void DescribeTheRoom(Screen scr)
    {
        // Clear the bobs -- set the ids to 0, x and y to 0
        foreach (RoomBackground b in bobs)
        {
            b.transform.position = Vector3.zero;    // Reset to origin
            b.currentRoomNumber = 0;                // The current object number
        }

        // Set the screen shape
        roomShape.currentRoomNumber = scr.screenShape;

        // Set the new bobs to their object position in the room
        int index = 0;
        foreach (var obj in scr.objects)
        {
            bobs[index].currentRoomNumber = obj.graphic;
            bobs[index].transform.position = new Vector3(obj.x, -obj.y);
            index++;
        }

        // TODO: Keep a note of the room's colour
    }
}
