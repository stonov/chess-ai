using UnityEngine;

public class Rook : Piece {
    public Rook(GameState _gameState, uint newColor, Vector2Int newPosition) : base(_gameState, newColor, newPosition) {}
    
    public override Piece GetCopy() {
        Rook copy = new Rook(gameState, color, position);
        copy.isMoved = isMoved;
        return copy;
    }

    protected override string GetWhiteSpritePath() {
        return "Sprites/Chess_rlt60";
    }

    protected override string GetBlackSpritePath() {
        return "Sprites/Chess_rdt60";
    }

    protected override void GenerateMoveRays() {
        base.GenerateMoveRays();
        MoveNode NRay = new MoveNode(color, new Vector2Int(position.x + 1, position.y));
        MoveNode ERay = new MoveNode(color, new Vector2Int(position.x, position.y + 1));
        MoveNode SRay = new MoveNode(color, new Vector2Int(position.x - 1, position.y));
        MoveNode WRay = new MoveNode(color, new Vector2Int(position.x, position.y - 1));
        
        NRay.ExtendInDirection(new Vector2Int(1, 0));
        ERay.ExtendInDirection(new Vector2Int(0, 1));
        SRay.ExtendInDirection(new Vector2Int(-1, 0));
        WRay.ExtendInDirection(new Vector2Int(0, -1));

        moveRays.Add(NRay);
        moveRays.Add(ERay);
        moveRays.Add(SRay);
        moveRays.Add(WRay);
    }

    public override string GetName() {
        return "Rook";
    }

    public override PieceType GetPieceType(){
        return PieceType.Rook;
    }
}
