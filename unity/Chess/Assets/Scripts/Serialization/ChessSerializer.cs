using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class ChessSerializer
{
    string GetSaveLocation()
    {
        string folder = Application.persistentDataPath;
        string file = "chess.json";
        string fullPath = Path.Combine(folder, file);
        return fullPath;
    }

    string GetSaveLocationBinary()
    {
        string folder = Application.persistentDataPath;
        string file = "chess.dat";
        string fullPath = Path.Combine(folder, file);
        return fullPath;
    }

    public Board LoadGameBinary()
    {
        string filePath = GetSaveLocationBinary();
        if (File.Exists(filePath))
        {
            using (Stream s = File.OpenRead(filePath))
            {
                using (BinaryReader r = new BinaryReader(s))
                {
                    string head = new string(r.ReadChars(4));
                    if (!head.Equals("CHES"))
                    {
                        return CreateNewGame();
                    }

                    int numRecords = r.ReadInt32();

                    Board board = new Board();
                    board.pieces = new List<Piece>();

                    for (int i = 0; i < numRecords; i++)
                    {
                        string pieceType = new string(r.ReadChars(1));
                        float x = r.ReadSingle();
                        float y = r.ReadSingle();
                        Piece piece = CreatePiece(pieceType, new Vector2(x, y));
                        board.pieces.Add(piece);
                    }

                    return board;
                }
            }
        }
        else
        {
            return CreateNewGame();
        }
    }

    public void SaveGameBinary(Board board)
    {
        string filePath = GetSaveLocationBinary();

        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }

        using (Stream s = File.OpenWrite(filePath))
        {
            using (BinaryWriter w = new BinaryWriter(s))
            {
                // Write out a header
                //     File type (4* char)
                w.Write("CHES".ToCharArray());
                //     Number of records (integer) ( 4 bytes)
                w.Write(board.pieces.Count);

                // Body (records repeated) -- each record is 9 bytes
                //     {
                //     Piece type   - 1 character (1 byte)
                //     x - co -ordinate - 1 float (4 bytes)
                //     y - co -ordinate  - 1 float (4 bytes)
                foreach (Piece p in board.pieces)
                {
                    w.Write(p.type[0]);
                    w.Write(p.positon.x);
                    w.Write(p.positon.y);
                }
            }
        }
    }

    public Board LoadGame()
    {
        string fullPath = GetSaveLocation();
        if (File.Exists(fullPath))
        {
            string jsonString = File.ReadAllText(fullPath);
            return JsonUtility.FromJson<Board>(jsonString);
        }
        else
        {
            return CreateNewGame();
        }
    }

    public void SaveGame(Board board)
    {
        string json = JsonUtility.ToJson(board);
        string fullPath = GetSaveLocation();

        if (File.Exists(fullPath))
        {
            File.Delete(fullPath);
        }

        File.WriteAllText(fullPath, json);
    }

    public Board CreateNewGame()
    {
        Board board = new Board();
        board.pieces = new List<Piece>();

        CreatePawns(board, "p", -1);
        CreatePawns(board, "P", -6);

        board.pieces.Add(CreatePiece("r", new Vector2(0, 0)));
        board.pieces.Add(CreatePiece("r", new Vector2(7, 0)));

        board.pieces.Add(CreatePiece("R", new Vector2(0, -7)));
        board.pieces.Add(CreatePiece("R", new Vector2(7, -7)));

        board.pieces.Add(CreatePiece("n", new Vector2(1, 0)));
        board.pieces.Add(CreatePiece("n", new Vector2(6, 0)));

        board.pieces.Add(CreatePiece("N", new Vector2(1, -7)));
        board.pieces.Add(CreatePiece("N", new Vector2(6, -7)));

        board.pieces.Add(CreatePiece("b", new Vector2(2, 0)));
        board.pieces.Add(CreatePiece("b", new Vector2(5, 0)));

        board.pieces.Add(CreatePiece("B", new Vector2(2, -7)));
        board.pieces.Add(CreatePiece("B", new Vector2(5, -7)));

        board.pieces.Add(CreatePiece("q", new Vector2(3, 0)));
        board.pieces.Add(CreatePiece("k", new Vector2(4, 0)));

        board.pieces.Add(CreatePiece("Q", new Vector2(3, -7)));
        board.pieces.Add(CreatePiece("K", new Vector2(4, -7)));

        return board;
    }

    void CreatePawns(Board board, string pawnColour, float row)
    {
        for (int i = 0; i < 8; i++)
        {
            board.pieces.Add(CreatePiece(pawnColour, new Vector2(i, row)));
        }
    }

    Piece CreatePiece(string pieceType, Vector2 pos)
    {
        Piece piece = new Piece() { positon = pos, type = pieceType };
        return piece;
    }
}
