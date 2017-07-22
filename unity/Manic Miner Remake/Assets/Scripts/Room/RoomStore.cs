using System.Collections.Generic;
using UnityEngine;

public class RoomStore : MonoBehaviour
{
    public string snapshotFile = "manicminer";
    private List<RoomData> _rooms;

    public IList<RoomData> Rooms { get { return _rooms; } }

    public bool IsReady { get; private set; }

    void Start()
    {
        _rooms = new List<RoomData>();

        using (SnapshotImporter importer = new SnapshotImporter(snapshotFile))
        {
            int offset = 45056;

            for (int i = 0; i < 20; i++)
            {
                // Move to the offset 
                importer.Seek(offset);

                // Import Room
                ImportRoom(importer);

                // Move to the next room
                offset += 1024;
            }
        }

        IsReady = true;
    }

    void ImportRoom(SnapshotImporter importer)
    {
        RoomData data = new RoomData();
        
        // Read in the screen attributes
        byte[] buf = importer.ReadBytes(512);
        int i = 0;

        //string s = "";

        for (int y = 0; y < 16; y++)
        {
            for (int x = 0; x < 32; x++)
            {
                //s += " " + buf[i];
                data.Attributes[i] = buf[i];
                i++;
            }
            //s += "\r\n";
        }

        //print(s);

        // Read in the room name
        data.RoomName = importer.ReadString(32);
        
        // Read in the block graphics
        for (i = 0; i < 8; i++)
        {
            // Read in the first byte that represents the attribute
            byte attr = importer.Read();
            byte[] blockData = importer.ReadBytes(8);
            data.Blocks[attr] = blockData;
        }

        // Read Miner Willy's start position

        // TODO: Read 1 byte for y-offset
        importer.Read();
        // TODO: Read 1 byte for sprite willy starts at
        importer.Read();
        // TODO: Read 1 byte for direction facing
        importer.Read();
        // TODO: Read 1 byte, should always be 0
        importer.Read();

        short rawMWPos = importer.ReadShort();
        CellPoint startPos = new CellPoint(rawMWPos.GetX(), rawMWPos.GetY());
        data.MinerWillyStart = startPos;
        importer.Read(); // Should always be zero??

        // TODO: Conveyor belt (4 bytes for the conveyor belt)
        importer.ReadBytes(4);

        // TODO: Border colour
        int borderColour = importer.Read();

        // TODO: Import the positions of the items
        bool addKey = true;
        for (var j = 0; j < 5; j++)
        {
            byte attr = importer.Read();
            //if (attr == 255) addKey = false;
            //if (attr == 0) addKey = false;

            byte secondGfxBuf = importer.Read();
            short keyPosRaw = importer.ReadShort();
            CellPoint keyPos = new CellPoint(keyPosRaw.GetX(), keyPosRaw.GetY());

            // read dummy byte
            int dummy = importer.Read();

            if (addKey)
            {
                data.RoomKeys.Add(new RoomKey(attr, keyPos));
            }

            addKey = true;
        }

        /*
         * Offset 628 is set at 0 in all Manic Miner rooms. Its counterpart in the runtime work area 
         * (see Appendix F) "is used to decide whether or not all the items have yet been collected; 
         * just before the items are displayed it is set to zero, then when an uncollected item is 
         * displayed, it is changed to the attribute of that item. Thus, after printing items, if 
         * this byte is still zero then all items must have been collected." [Garry Lancaster]

            Offset 654 is always set at 255. 
         */
        byte d1 = importer.Read();
        byte d2 = importer.Read();

        // PORTAL
        byte portalColour = importer.Read();
        byte[] portalShape = importer.ReadBytes(32);
        
        short portalPosRaw = importer.ReadShort();
        importer.ReadShort(); // Bit of a fudge, because we're gonna ignore the SECOND short...

        data.AddPortal(portalColour, portalShape, portalPosRaw.GetX(), portalPosRaw.GetY());

        data.KeyShape = importer.ReadBytes(8);

        byte airFirst = importer.Read();
        byte airSize = (byte)((airFirst - 32) - 4);
        byte airPixels = importer.Read();

        data.AirSupply = new AirSupply() { Length = airSize, Tip = airPixels };

        _rooms.Add(data);
    }
}
