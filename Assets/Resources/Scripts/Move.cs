using System.Collections.Generic;
using UnityEngine;

public class MoveNode {
    int id = -1;
    private bool isAttack;
    private Vector2Int position;
    private MoveNode prevNode;
    private MoveNode nextNode;
    private uint parentColor;

    public MoveNode(uint _parentColor, Vector2Int _position) {
        isAttack = true;
        position = _position;
        parentColor = _parentColor;
        prevNode = null;
        nextNode = null;
    }

    public MoveNode(MoveNode _prevNode, uint _parentColor, Vector2Int _position) {
        position = _position;
        isAttack = true;
        parentColor = _parentColor;
        prevNode = _prevNode;
        nextNode = null;
    }

    public MoveNode(bool _isAttack, uint _parentColor, Vector2Int _position) {
        isAttack = _isAttack;
        position = _position;
        parentColor = _parentColor;
        prevNode = null;
        nextNode = null;
    }

    public MoveNode(MoveNode _prevNode, bool _isAttack, uint _parentColor, Vector2Int _position) {
        position = _position;
        isAttack = _isAttack;
        parentColor = _parentColor;
        prevNode = _prevNode;
        nextNode = null;
    }

    public int GetId() { return id; }
    public void SetId(int _id) { id = _id; }
    public void SetNextMoveNode(MoveNode _nextNode) {
        nextNode = _nextNode;
    }

    public bool IsAttack() { return isAttack; }
    public Vector2Int GetPosition() { return new Vector2Int(position.x, position.y); }
    public List<MoveNode> GetMoveNodes() {
        List<MoveNode> moves;
        if (nextNode != null) {
            moves = nextNode.GetMoveNodes();
        } else {
            moves = new List<MoveNode>();
        }
        moves.Insert(0, this);
        return moves;
    }

    public void ExtendInDirection(Vector2Int direction) {
        if (position.x < 0 || position.y < 0 || position.x > 7 || position.y > 7) {
            return;
        }
        MoveNode child = new MoveNode(this, parentColor, new Vector2Int(position.x + direction.x, position.y + direction.y));
        SetNextMoveNode(child);
        child.ExtendInDirection(direction);
    }
}
