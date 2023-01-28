using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceHologram
{
    public Sprite sprite;
    public Vector2Int position;
    public uint color;
}

public class GameState
{
    private Dictionary<int, Piece> pieceList;
    private Piece[,] pieceGrid;
    private int currentPlayerColor;
    private int piecesAddedCount;

    public GameState() {
        pieceList = new Dictionary<int, Piece>();
        pieceGrid = new Piece[8,8];
    }

    private void AddPieceToBoard(Piece piece) {
        pieceList.Add(piecesAddedCount, piece);
        pieceGrid[piece.GetPosition().x, piece.GetPosition().y] = piece;
        piece.SetId(piecesAddedCount);
        piecesAddedCount++;
    }

    private void RemovePieceFromBoard(Piece piece) {
        pieceList.Remove(piece.GetId());
        pieceGrid[piece.GetPosition().x, piece.GetPosition().y] = null;
    }

    private void MovePiece(Piece piece, Vector2Int end) {
        Piece pieceAtEnd = FindPiece(end);
        if (pieceAtEnd != null) {
            RemovePieceFromBoard(pieceAtEnd);
        }
        RemovePieceFromBoard(piece);
        piece.SetPosition(end);
        AddPieceToBoard(piece);
    }

    private Piece FindPiece(Vector2Int position) {
        if (position.x >= 0 && position.x <= 7 && position.y >= 0 && position.y <= 7) {
            return pieceGrid[position.x, position.y];
        } else {
            return null;
        }
    }

    private void AdvanceTurn() {
        currentPlayerColor = 1 - currentPlayerColor;
    }

    public void SetupNewGame() {
        pieceList.Clear();
        piecesAddedCount = 0;
        for (int i = 0; i < 8; i++) {
            Pawn newWPawn = new Pawn(0, new Vector2Int(1,i));
            Pawn newBPawn = new Pawn(1, new Vector2Int(6,i));
            AddPieceToBoard(newWPawn);
            AddPieceToBoard(newBPawn);
        }
        currentPlayerColor = 0;
    }

    public List<PieceHologram> GetPieceHolograms() {
        List<PieceHologram> pieceHolograms = new List<PieceHologram>();
        foreach (KeyValuePair<int, Piece> item in pieceList){
            PieceHologram pieceHologram = new PieceHologram();
            pieceHologram.position = item.Value.GetPosition();
            pieceHologram.sprite = item.Value.GetSprite();
            pieceHologram.color = item.Value.GetColor();
            pieceHolograms.Add(pieceHologram);
        }
        return pieceHolograms;
    }

    public int GetCurrentPlayerColor() {
        return currentPlayerColor;
    }

    public List<Vector2Int> GetMoves(Vector2Int position) {
        Piece selectedPiece = FindPiece(position);
        if (selectedPiece != null && !selectedPiece.IsEmpty()) {
            return selectedPiece.GetMoves();
        } else {
            return new List<Vector2Int>();
        }
    }

    public void RequestMove(Vector2Int start, Vector2Int end) {
        Piece selectedPiece = FindPiece(start);
        if (selectedPiece == null) {
            return;
        }
        List<Vector2Int> moves = selectedPiece.GetMoves();
        foreach(Vector2Int move in moves) {
            if (move.Equals(end)) {
                MovePiece(selectedPiece, end);
                AdvanceTurn();
                return;
            }
        }
    }
}
