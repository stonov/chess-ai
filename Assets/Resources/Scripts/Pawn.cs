using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : Piece {
    private int moveDirection;
    public Pawn(uint newColor, Vector2Int newPosition) : base(newColor, newPosition){}
    protected override string GetWhiteSpritePath() {
        return "Sprites/Chess_plt60";
    }

    protected override string GetBlackSpritePath() {
        return "Sprites/Chess_pdt60";
    }

    override public bool IsEmpty() {
        return false;
    }

    override public string GetName() {
        return "Pawn";
    }

    override public List<Vector2Int> GetMoves() {
        List<Vector2Int> moves = new List<Vector2Int>();
        for (int i = 0; i < 8; i++) {
            for (int j = 0; j < 8; j++) {
                if (i != position.x && j != position.y) {
                    moves.Add(new Vector2Int(i, j));
                }
            }
        }
        return moves;
    }
}
