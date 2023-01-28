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

    protected override bool IsMovePlayable(MoveNode move) {
        Piece pieceAtMove = gameState.FindPieceCopy(move.GetPosition());
        if (move.IsAttack()) {
            return pieceAtMove != null
                && (pieceAtMove.GetColor() != color);
        } else {
            return pieceAtMove == null;
        }
    }

    public override string GetName() {
        return "Pawn";
    }
}
