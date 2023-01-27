using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerMono : MonoBehaviour
{
    public GameObject cellPrefab;
    public float cellSize = 60f;
    public Color32 lightSquareColor = new Color32(243, 217, 190, 255);
    public Color32 darkSquareColor = new Color32(232, 180, 128, 255);
    public Color32 unselectedColor = new Color32(0, 0, 0, 0);
    public Color32 clickedColor = new Color32(0, 162, 232, 200);
    public Color32 validMoveColor = new Color32(0, 232, 162, 100);
    
    void Start()
    {
        float boardWidth = 8 * cellSize;
        for (int rank = 0; rank < 8; rank++) {
            for (int file = 0; file < 8; file++) {
                // Creating the cell gameobject
                GameObject newCellObject = Instantiate(cellPrefab, transform);
                
                // Setting the anchored position
                RectTransform newCellRectTransform = newCellObject.GetComponent<RectTransform>();
                newCellRectTransform.anchoredPosition = new Vector2(
                    file*cellSize + cellSize/2 - boardWidth/2,
                    rank*cellSize + cellSize/2 - boardWidth/2
                );

                // Performing the cell setup and setting the color
                SquareMono newCell = newCellObject.GetComponent<SquareMono>();
                if ((rank + file) % 2 == 0) {
                    newCell.SetColor(darkSquareColor);
                } else {
                    newCell.SetColor(lightSquareColor);
                }
                newCell.Setup(this, cellSize, new Vector2Int(rank, file));
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RegisterSquareClicked(SquareMono square) {
        square.SetSelectionColor(clickedColor);
    }
}
