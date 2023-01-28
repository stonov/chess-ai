using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerMono : MonoBehaviour
{
    public GameObject cellPrefab;
    public float cellSize = 60f;
    public int numRanks = 8;
    public int numFiles = 8;
    public Color32 lightSquareColor = new Color32(243, 217, 190, 255);
    public Color32 darkSquareColor = new Color32(209, 173, 136, 255);
    public Color32 unselectedColor = new Color32(0, 0, 0, 0);
    public Color32 clickedColor = new Color32(0, 162, 232, 200);
    public Color32 validMoveColor = new Color32(0, 232, 162, 100);
    private GameState game;
    private SquareMono[,] displayGrid;
    private List<Piece> pieceList;
    private Vector2Int selectedSquarePosition = new Vector2Int(-1,-1);
    private List<Vector2Int> possibleMoves;
    
    void Start()
    {
        float boardWidth = numFiles * cellSize;
        float boardHeight = numRanks * cellSize;
        displayGrid = new SquareMono[numRanks,numFiles];
        for (int rank = 0; rank < numRanks; rank++) {
            for (int file = 0; file < numFiles; file++) {
                // Creating the cell gameobject
                GameObject newCellObject = Instantiate(cellPrefab, transform);
                
                // Setting the anchored position
                RectTransform newCellRectTransform = newCellObject.GetComponent<RectTransform>();
                newCellRectTransform.anchoredPosition = new Vector2(
                    file*cellSize + cellSize/2 - boardWidth/2,
                    rank*cellSize + cellSize/2 - boardHeight/2
                );

                // Performing the cell setup and setting the color
                SquareMono newCell = newCellObject.GetComponent<SquareMono>();
                if ((rank + file) % 2 == 0) {
                    newCell.SetColor(darkSquareColor);
                } else {
                    newCell.SetColor(lightSquareColor);
                }
                newCell.Setup(this, cellSize, new Vector2Int(rank, file));
                displayGrid[rank, file] = newCell;
            }
        }

        pieceList = new List<Piece>();
        possibleMoves = new List<Vector2Int>();

        game = new GameState();
        game.SetupNewGame();

        SyncBoard();
    }

    private void SyncBoard() {
        foreach(Piece item in pieceList) {
            FindSquare(item.GetPosition()).SetEmpty();
        }
        pieceList = game.GetPieceCopies();
        foreach(Piece item in pieceList) {
            if (item != null) {
                FindSquare(item.GetPosition()).SetPiece(item.GetSprite(), item.GetColor());
            }
        }
    }

    private SquareMono FindSquare(Vector2Int position) {
        return displayGrid[position.x, position.y];
    }

    // Determines whether a square is selected
    private bool IsAnySquareSelected() {
        return !(selectedSquarePosition.x == -1 && selectedSquarePosition.y == -1);
    }

    // Selects a square in preparation for a move
    private void SelectSquare(Vector2Int position) {
        RemoveSquareSelection();
        selectedSquarePosition = position;
        FindSquare(position).SetSelectionColor(clickedColor);
        ShowPossibleMoves(game.GetMoves(position));
    }

    // Removes the selection from the selected square
    private void RemoveSquareSelection() {
        if (IsAnySquareSelected()) {
            FindSquare(selectedSquarePosition).SetSelectionColor(unselectedColor);
            selectedSquarePosition = new Vector2Int(-1, -1);
        }
        RemoveShownPossibleMoves();
    }

    // Removes the displayed possible moves
    private void RemoveShownPossibleMoves() {
        foreach(Vector2Int item in possibleMoves) {
            FindSquare(item).SetSelectionColor(unselectedColor);
            FindSquare(item).UnmarkAsPotentialMove();
        }
        possibleMoves.Clear();
    }

    // Displays possible moves
    private void ShowPossibleMoves(List<Vector2Int> _possibleMoves) {
        RemoveShownPossibleMoves();
        possibleMoves = _possibleMoves;
        foreach(Vector2Int item in possibleMoves) {
            FindSquare(item).SetSelectionColor(validMoveColor);
            FindSquare(item).MarkAsPotentialMove();
        }
    }

    // Logic whenever a square is clicked
    public void RegisterSquareClicked(SquareMono clickedSquare) {
        if (IsAnySquareSelected()) {
            if (clickedSquare.IsPotentialMove()) {
                game.RequestMove(selectedSquarePosition, clickedSquare.GetPosition());
                RemoveSquareSelection();
                SyncBoard();
                return;
            }
        }
        if (!clickedSquare.IsEmpty()
            && clickedSquare.GetPieceColor() == game.GetCurrentPlayerColor()
            && !clickedSquare.GetPosition().Equals(selectedSquarePosition)) {
            SelectSquare(clickedSquare.GetPosition());
        } else {
            RemoveSquareSelection();
        }
    }
}
