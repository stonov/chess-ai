using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Piece {
    protected string whiteSpritePath;
    protected string blackSpritePath;
    protected uint color;
    protected Sprite sprite;
    protected Vector2Int position;
    protected int id = -1;
    public Piece(uint newColor, Vector2Int newPosition) {
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
        color = newColor;
        position = newPosition;
    }
    
    protected abstract string GetWhiteSpritePath();
    protected abstract string GetBlackSpritePath();
    public Sprite GetSprite() {
        return sprite;
    }

    public void SetId(int _id) { id = _id; }
    public int GetId() { return id; }
    public void SetPosition(Vector2Int _position) { position = _position; }

    public Vector2Int GetPosition() {
        return new Vector2Int(position.x, position.y);
    }

    public uint GetColor() {
        return color;
    }

    public virtual bool IsEmpty() {
        return true;
    }

    public virtual string GetName() {
        return "None";
    }

    public virtual List<Vector2Int> GetMoves() {
        return new List<Vector2Int>();
    }
}
