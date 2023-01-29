using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PieceType {
    Pawn, Knight, Bishop, Rook, Queen, King
}

public abstract class Piece {
    private bool initialized = false;
    protected uint color;
    protected Sprite sprite;
    protected Vector2Int position;
    protected bool isMoved;
    protected int id = -1;
    protected GameState gameState;
    protected List<MoveNode> moveRays;

    public Piece(GameState _gameState, uint newColor, Vector2Int newPosition) {
        switch (newColor) {
            case 0:
                sprite = Resources.Load<Sprite>(GetWhiteSpritePath()) as Sprite;
                break;
            case 1:
                sprite = Resources.Load<Sprite>(GetBlackSpritePath()) as Sprite;
                break;
            default:
                break;
        }
        moveRays = new List<MoveNode>();
        gameState = _gameState;
        color = newColor;
        SetPosition(newPosition);
        initialized = true;
    }

    public abstract string GetName();
    public abstract PieceType GetPieceType();
    public abstract Piece GetCopy();
    protected virtual void GenerateMoveRays() {
        moveRays.Clear();
    }
    protected abstract string GetWhiteSpritePath();
    protected abstract string GetBlackSpritePath();
    public Sprite GetSprite() {
        return sprite;
    }

    public void SetId(int _id) { id = _id; }
    public int GetId() { return id; }
    public void MarkAsMoved() { isMoved = true; }
    public bool IsMoved() {
        return isMoved;
    }

    public void SetPosition(Vector2Int _position) {
        if (initialized) {
            MarkAsMoved();
        }
        position = _position;
        GenerateMoveRays();
    }

    public Vector2Int GetPosition() {
        return new Vector2Int(position.x, position.y);
    }

    public uint GetColor() {
        return color;
    }

    protected virtual bool IsMovePlayable(MoveNode move) {
        Piece pieceAtMove = gameState.FindPieceCopy(move.GetPosition());
        return pieceAtMove == null
            || pieceAtMove.GetColor() != color;
    }

    public List<Vector2Int> GetMoves() {
        List<Vector2Int> moves = new List<Vector2Int>();
        foreach(MoveNode moveRay in moveRays) {
            List<MoveNode> moveNodes = moveRay.GetMoveNodes();
            foreach(MoveNode moveNode in moveNodes) {
                if (gameState.IsPositionValid(moveNode.GetPosition()) && IsMovePlayable(moveNode)) {
                    moves.Add(moveNode.GetPosition());
                    if (gameState.FindPieceCopy(moveNode.GetPosition()) != null) {
                        break;
                    }
                } else {
                    break;
                }
            }
        }
        return moves;
    }

    public List<MoveNode> GetMoveRays() {
        return moveRays;
    }

    public List<MoveNode> GetAllMoveNodes() {
        List<MoveNode> allMoveNodes = new List<MoveNode>();
        foreach(MoveNode moveRay in moveRays) {
            allMoveNodes.AddRange(moveRay.GetMoveNodes());
        }
        return allMoveNodes;
    }
}
