using UnityEngine;

public class Knight : Piece {
    public Knight(GameState _gameState, uint newColor, Vector2Int newPosition) : base(_gameState, newColor, newPosition) {}
    
    public override Piece GetCopy() {
        Knight copy = new Knight(gameState, color, position);
        copy.isMoved = isMoved;
        return copy;
    }

    protected override string GetWhiteSpritePath() {
        return "Sprites/Chess_nlt60";
    }

    protected override string GetBlackSpritePath() {
        return "Sprites/Chess_ndt60";
    }

    protected override void GenerateMoveRays() {
        base.GenerateMoveRays();
        moveRays.Add(new MoveNode(color, new Vector2Int(position.x + 1, position.y + 2)));
        moveRays.Add(new MoveNode(color, new Vector2Int(position.x + 2, position.y + 1)));
        moveRays.Add(new MoveNode(color, new Vector2Int(position.x + 1, position.y - 2)));
        moveRays.Add(new MoveNode(color, new Vector2Int(position.x + 2, position.y - 1)));
        moveRays.Add(new MoveNode(color, new Vector2Int(position.x - 1, position.y + 2)));
        moveRays.Add(new MoveNode(color, new Vector2Int(position.x - 2, position.y + 1)));
        moveRays.Add(new MoveNode(color, new Vector2Int(position.x - 1, position.y - 2)));
        moveRays.Add(new MoveNode(color, new Vector2Int(position.x - 2, position.y - 1)));
    }

    public override string GetName() {
        return "Knight";
    }
}
