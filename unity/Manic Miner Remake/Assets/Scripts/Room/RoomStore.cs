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

            //// Read in the next 8 bytes that represent the shape
            //SpriteTexture tex = new SpriteTexture(8, 8, new Vector2(0, 1));
            //tex.Clear(new Color(0, 0, 0, 0));
            //for (int y = 0; y < 8;y++)
            //{
            //    tex.SetLine(y, importer.Read());
            //}

            byte[] blockData = importer.ReadBytes(8);

            data.Blocks[attr] = blockData; // tex.Apply();
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
        importer.Read();

        // TODO: Import the positions of the items
        for (var j = 0; j < 5; j++)
        {
            byte attr = importer.Read();
            if (attr == 255) break;
            if (attr == 0) continue;

            byte secondGfxBuf = importer.Read();
            short keyPosRaw = importer.ReadShort();
            CellPoint keyPos = new CellPoint(keyPosRaw.GetX(), keyPosRaw.GetY());

            // read dummy byte
            importer.Read();

            data.RoomKeys.Add(new RoomKey(attr, keyPos));
        }

        _rooms.Add(data);
    }
}
