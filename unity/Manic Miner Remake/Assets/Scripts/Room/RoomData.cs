using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomData
{
    int[] _attrs;

    public string RoomName { get; set; }

    public int[] Attributes { get { return _attrs; } }

    public Dictionary<int, byte[]> Blocks { get; private set; }

    public List<RoomKey> RoomKeys { get; private set; }

    public CellPoint MinerWillyStart { get; set; }

    public RoomData()
    {
        _attrs = new int[32 * 16];
        Blocks = new Dictionary<int, byte[]>();
        RoomKeys = new List<RoomKey>();
    }

    public void SetAttr(int x, int y, int attr)
    {
        _attrs[y * 32 + x] = attr;
    }
}
