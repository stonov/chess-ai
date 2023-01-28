using UnityEngine;

public class Queen : Piece {
    public Queen(GameState _gameState, uint newColor, Vector2Int newPosition) : base(_gameState, newColor, newPosition) {}
    
    public override Piece GetCopy() {
        Queen copy = new Queen(gameState, color, position);
        copy.isMoved = isMoved;
        return copy;
    }

    protected override string GetWhiteSpritePath() {
        return "Sprites/Chess_qlt60";
    }

    protected override string GetBlackSpritePath() {
        return "Sprites/Chess_qdt60";
    }

    protected override void GenerateMoveRays() {
        base.GenerateMoveRays();
        MoveNode NRay = new MoveNode(color, new Vector2Int(position.x + 1, position.y));
        MoveNode ERay = new MoveNode(color, new Vector2Int(position.x, position.y + 1));
        MoveNode SRay = new MoveNode(color, new Vector2Int(position.x - 1, position.y));
        MoveNode WRay = new MoveNode(color, new Vector2Int(position.x, position.y - 1));
        MoveNode NWRay = new MoveNode(color, new Vector2Int(position.x + 1, position.y - 1));
        MoveNode NERay = new MoveNode(color, new Vector2Int(position.x + 1, position.y + 1));
        MoveNode SWRay = new MoveNode(color, new Vector2Int(position.x - 1, position.y - 1));
        MoveNode SERay = new MoveNode(color, new Vector2Int(position.x - 1, position.y + 1));
        
        NRay.ExtendInDirection(new Vector2Int(1, 0));
        ERay.ExtendInDirection(new Vector2Int(0, 1));
        SRay.ExtendInDirection(new Vector2Int(-1, 0));
        WRay.ExtendInDirection(new Vector2Int(0, -1));
        NWRay.ExtendInDirection(new Vector2Int(1, -1));
        NERay.ExtendInDirection(new Vector2Int(1, 1));
        SWRay.ExtendInDirection(new Vector2Int(-1, -1));
        SERay.ExtendInDirection(new Vector2Int(-1, 1));

        moveRays.Add(NRay);
        moveRays.Add(ERay);
        moveRays.Add(SRay);
        moveRays.Add(WRay);
        moveRays.Add(NWRay);
        moveRays.Add(NERay);
        moveRays.Add(SWRay);
        moveRays.Add(SERay);
    }

    public override string GetName() {
        return "Queen";
    }
}
