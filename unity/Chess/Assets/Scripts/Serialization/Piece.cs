using System;
using UnityEngine;

/// <summary>
/// A chess piece.
/// </summary>
[Serializable]
public class Piece
{
    // Piece type PNBRQK
    public string type;

    // Position on the board
    public Vector2 positon;
}
