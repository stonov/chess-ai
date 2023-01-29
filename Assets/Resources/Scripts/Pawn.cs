using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : Piece {
    private int moveDirection;
    public Pawn(GameState _gameState, uint newColor, Vector2Int newPosition) : base(_gameState, newColor, newPosition) {
        if (newColor == 0) {
            moveDirection = 1;
        } else {
            moveDirection = -1;
        }
        GenerateMoveRays();
    }

    public override Piece GetCopy() {
        Pawn copy = new Pawn(gameState, color, position);
        copy.isMoved = isMoved;
        return copy;
    }

    protected override void GenerateMoveRays() {
        base.GenerateMoveRays();
        MoveNode leftAttack = new MoveNode(color, new Vector2Int(position.x + moveDirection, position.y - 1));
        MoveNode rightAttack = new MoveNode(color, new Vector2Int(position.x + moveDirection, position.y + 1));
        MoveNode forwardStep = new MoveNode(false, color, new Vector2Int(position.x + moveDirection, position.y));
        moveRays.Add(leftAttack);
        moveRays.Add(rightAttack);
        moveRays.Add(forwardStep);

        if (!IsMoved()) {
            MoveNode forwardStepFar = new MoveNode(forwardStep, false, color, new Vector2Int(position.x + moveDirection*2, position.y));
            forwardStep.SetNextMoveNode(forwardStepFar);
        }
    }

    protected override string GetWhiteSpritePath() {
        return "Sprites/Chess_plt60";
    }

    protected override string GetBlackSpritePath() {
        return "Sprites/Chess_pdt60";
    }
    
    private bool IsEnPassantAvailable(MoveNode move) {
        Vector2Int movePosition = move.GetPosition();
        Piece pieceBehindMove = gameState.FindPieceCopy(new Vector2Int(movePosition.x - moveDirection, movePosition.y));
        List<Step> history = gameState.GetHistory();
        if (history.Count < 1 || pieceBehindMove == null) {
            return false;
        }

        Step lastStep = history[history.Count - 1];
        bool isEnemyPawnBehindMove =
            pieceBehindMove != null
            && pieceBehindMove.GetPieceType() == PieceType.Pawn
            && pieceBehindMove.GetColor() != color;
        bool didEnemyPawnMoveLastTurn = lastStep.GetEnd().Equals(pieceBehindMove.GetPosition());
        bool wasEnemyPawnAtStartLastTurn = lastStep.GetStart().x == 1 || lastStep.GetStart().x == 6;

        return isEnemyPawnBehindMove && didEnemyPawnMoveLastTurn && wasEnemyPawnAtStartLastTurn;
    }

    protected override bool IsMovePlayable(MoveNode move) {
        Vector2Int movePosition = move.GetPosition();
        Piece pieceAtMove = gameState.FindPieceCopy(movePosition);
        if (move.IsAttack()) {
            return (pieceAtMove != null && (pieceAtMove.GetColor() != color)) || IsEnPassantAvailable(move);
        } else {
            return pieceAtMove == null;
        }
    }

    public override string GetName() {
        return "Pawn";
    }

    public override PieceType GetPieceType(){
        return PieceType.Pawn;
    }
}
