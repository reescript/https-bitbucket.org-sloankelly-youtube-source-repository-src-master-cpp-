using UnityEngine;
using System.Collections.Generic;

public class BoardRenderer : MonoBehaviour
{
    Dictionary<string, Sprite> sprites;

    GameObject[] pieces;

    public Sprite blackPawn;
    public Sprite blackRook;
    public Sprite blackBishop;
    public Sprite blackKnight;
    public Sprite blackQueen;
    public Sprite blackKing;

    public Sprite whitePawn;
    public Sprite whiteRook;
    public Sprite whiteBishop;
    public Sprite whiteKnight;
    public Sprite whiteQueen;
    public Sprite whiteKing;

    public GameObject pieceTemplate;

    public void Initialize(Board board)
    {
        // TODO: Clean up pieces and move them off the board

        for (int i = 0; i < board.pieces.Count; i++)
        {
            if (pieces[i] == null)
            {
                pieces[i] = Instantiate(pieceTemplate);
            }

            pieces[i].name = board.pieces[i].type;

            if (sprites.ContainsKey(pieces[i].name))
            {
                pieces[i].GetComponent<SpriteRenderer>().sprite = sprites[pieces[i].name];
            }
            else
            {
                UnityEngine.Debug.Log("Can't find piece of type '" + pieces[i].name);
            }
            pieces[i].transform.position = board.pieces[i].positon;
        }
    }

    void Awake()
    {
        sprites = new Dictionary<string, Sprite>();
        sprites["p"] = blackPawn;
        sprites["r"] = blackRook;
        sprites["n"] = blackKnight;
        sprites["b"] = blackBishop;
        sprites["q"] = blackQueen;
        sprites["k"] = blackKing;

        sprites["P"] = whitePawn;
        sprites["R"] = whiteRook;
        sprites["N"] = whiteKnight;
        sprites["B"] = whiteBishop;
        sprites["Q"] = whiteQueen;
        sprites["K"] = whiteKing;

        pieces = new GameObject[32];
    }
}
