using UnityEngine;

public class Bishop : Piece {
    public Bishop(GameState _gameState, uint newColor, Vector2Int newPosition) : base(_gameState, newColor, newPosition) {}
    
    public override Piece GetCopy() {
        Bishop copy = new Bishop(gameState, color, position);
        copy.isMoved = isMoved;
        return copy;
    }

    protected override string GetWhiteSpritePath() {
        return "Sprites/Chess_blt60";
    }

    protected override string GetBlackSpritePath() {
        return "Sprites/Chess_bdt60";
    }

    protected override void GenerateMoveRays() {
        base.GenerateMoveRays();
        MoveNode NWRay = new MoveNode(color, new Vector2Int(position.x + 1, position.y - 1));
        MoveNode NERay = new MoveNode(color, new Vector2Int(position.x + 1, position.y + 1));
        MoveNode SWRay = new MoveNode(color, new Vector2Int(position.x - 1, position.y - 1));
        MoveNode SERay = new MoveNode(color, new Vector2Int(position.x - 1, position.y + 1));
        
        NWRay.ExtendInDirection(new Vector2Int(1, -1));
        NERay.ExtendInDirection(new Vector2Int(1, 1));
        SWRay.ExtendInDirection(new Vector2Int(-1, -1));
        SERay.ExtendInDirection(new Vector2Int(-1, 1));

        moveRays.Add(NWRay);
        moveRays.Add(NERay);
        moveRays.Add(SWRay);
        moveRays.Add(SERay);
    }

    public override string GetName() {
        return "Bishop";
    }
}
