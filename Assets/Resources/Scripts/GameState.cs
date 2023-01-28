using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState
{
    private Dictionary<int, Piece> pieceList;
    private Piece[,] pieceGrid;
    private Dictionary<int, MoveNode>[,] moveGrid;
    private int currentPlayerColor;
    private int piecesAddedCount;
    private int movesAddedCount;

    public GameState() {
        pieceList = new Dictionary<int, Piece>();
        pieceGrid = new Piece[8,8];
        moveGrid = new Dictionary<int, MoveNode>[8,8];
        for (int i = 0; i < 8; i++) {
            for (int j = 0; j < 8; j++) {
                moveGrid[i,j] = new Dictionary<int, MoveNode>();
            }
        }
    }

    private void AddPieceToBoard(Piece piece) {
        piece.SetId(piecesAddedCount);
        pieceList.Add(piece.GetId(), piece);
        pieceGrid[piece.GetPosition().x, piece.GetPosition().y] = piece;
        RegisterPieceMoves(piece);
        piecesAddedCount++;
    }

    private void RemovePieceFromBoard(Piece piece) {
        pieceList.Remove(piece.GetId());
        pieceGrid[piece.GetPosition().x, piece.GetPosition().y] = null;
        piece.SetId(-1);
        UnregisterPieceMoves(piece);
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

    public bool IsPositionValid(Vector2Int position) {
        return (position.x >= 0 && position.x <= 7 && position.y >= 0 && position.y <= 7);
    }

    private Dictionary<int, MoveNode> FindMoveDict(Vector2Int position) {
        if (IsPositionValid(position)) {
            return moveGrid[position.x, position.y];
        } else {
            Debug.Log(position + " is an invalid position.");
            return null;
        }
    }

    private Piece FindPiece(Vector2Int position) {
        if (IsPositionValid(position)) {
            return pieceGrid[position.x, position.y];
        } else {
            Debug.Log(position + " is an invalid position.");
            return null;
        }
    }

    public Piece FindPieceCopy(Vector2Int position) {
        if (IsPositionValid(position)) {
            return pieceGrid[position.x, position.y];
        } else {
            return null;
        }
    }

    private void AdvanceTurn() {
        currentPlayerColor = 1 - currentPlayerColor;
    }

    private void RegisterPieceMoves(Piece piece) {
        List<MoveNode> allMoveNodes = piece.GetAllMoveNodes();
        foreach(MoveNode move in allMoveNodes) {
            RegisterMoveNode(move);
        }
    }

    private void UnregisterPieceMoves(Piece piece) {
        List<MoveNode> allMoveNodes = piece.GetAllMoveNodes();
        foreach(MoveNode move in allMoveNodes) {
            UnregisterMoveNode(move);
        }
    }

    private void RegisterMoveNode(MoveNode move) {
        if (!IsPositionValid(move.GetPosition())) {
            return;
        }
        move.SetId(movesAddedCount);
        FindMoveDict(move.GetPosition()).Add(move.GetId(), move);
        movesAddedCount++;
    }

    private void UnregisterMoveNode(MoveNode move) {
        if (!IsPositionValid(move.GetPosition())) {
            return;
        }
        FindMoveDict(move.GetPosition()).Remove(move.GetId());
        move.SetId(-1);
    }

    public void SetupNewGame() {
        pieceList.Clear();
        for (int i = 0; i < 8; i++) {
            for (int j = 0; j < 8; j++) {
                moveGrid[i,j].Clear();
            }
        }

        piecesAddedCount = 0;
        movesAddedCount = 0;
        for (int i = 0; i < 8; i++) {
            Pawn newWPawn = new Pawn(this, 0, new Vector2Int(1,i));
            Pawn newBPawn = new Pawn(this, 1, new Vector2Int(6,i));
            AddPieceToBoard(newWPawn);
            AddPieceToBoard(newBPawn);
        }
        AddPieceToBoard(new Knight(this, 0, new Vector2Int(0, 1)));
        AddPieceToBoard(new Knight(this, 0, new Vector2Int(0, 6)));
        AddPieceToBoard(new Knight(this, 1, new Vector2Int(7, 1)));
        AddPieceToBoard(new Knight(this, 1, new Vector2Int(7, 6)));

        AddPieceToBoard(new Bishop(this, 0, new Vector2Int(0, 2)));
        AddPieceToBoard(new Bishop(this, 0, new Vector2Int(0, 5)));
        AddPieceToBoard(new Bishop(this, 1, new Vector2Int(7, 2)));
        AddPieceToBoard(new Bishop(this, 1, new Vector2Int(7, 5)));

        AddPieceToBoard(new Queen(this, 0, new Vector2Int(0, 3)));
        AddPieceToBoard(new Queen(this, 1, new Vector2Int(7, 3)));

        AddPieceToBoard(new Rook(this, 0, new Vector2Int(0, 0)));
        AddPieceToBoard(new Rook(this, 0, new Vector2Int(0, 7)));
        AddPieceToBoard(new Rook(this, 1, new Vector2Int(7, 0)));
        AddPieceToBoard(new Rook(this, 1, new Vector2Int(7, 7)));
        currentPlayerColor = 0;
    }

    public List<Piece> GetPieceCopies() {
        List<Piece> pieceCopies = new List<Piece>();
        foreach (KeyValuePair<int, Piece> item in pieceList){
            Piece copy = item.Value.GetCopy();
            pieceCopies.Add(copy);
        }
        return pieceCopies;
    }

    public int GetCurrentPlayerColor() {
        return currentPlayerColor;
    }

    public List<Vector2Int> GetMoves(Vector2Int position) {
        Piece selectedPiece = FindPiece(position);
        if (selectedPiece != null) {
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
