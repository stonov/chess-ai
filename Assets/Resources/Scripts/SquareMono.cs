﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/**
 * This class contains the data for each cell
 */
public class SquareMono : MonoBehaviour, IPointerDownHandler {
    // Input variables from outside tis class
    public Image cellImage;
    public RectTransform cellRectTransform;
    public BoxCollider2D cellCollider;

    // Related to the square selection child
    public GameObject squareSelectionPrefab;
    SquareSelectionMono squareSelectionMono;
    GameObject squareSelectionObject;

    // Related to the piece displayer child
    public GameObject pieceDisplayerPrefab;
    PieceDisplayerMono pieceDisplayerMono;
    GameObject pieceDisplayerObject;

    ControllerMono controller;
    // The position of this cell on the board
    Vector2Int position;
    bool isEmpty;
    bool isPotentialMove;
    uint pieceColor;

    // Used when creating the cell
    // Sets up the child piece displayer object as well
    public void Setup(ControllerMono newController, float cellSize, Vector2Int newPosition) {
        controller = newController;
        
        squareSelectionObject = Instantiate(squareSelectionPrefab, transform);
        squareSelectionMono = squareSelectionObject.GetComponent<SquareSelectionMono>();

        pieceDisplayerObject = Instantiate(pieceDisplayerPrefab, transform);
        pieceDisplayerMono = pieceDisplayerObject.GetComponent<PieceDisplayerMono>();
        pieceDisplayerMono.SetEmpty();

        SetSize(cellSize);
        SetPosition(newPosition);
        isEmpty = true;
        isPotentialMove = false;
    }

    // A couple of setters and getters
    public void SetColor(Color32 newColor) { cellImage.color = newColor; }
    public void SetSelectionColor(Color32 newColor) {
        squareSelectionMono.SetColor(newColor);
    }
    public void SetPosition(Vector2Int newPosition) {
        position.x = newPosition.x;
        position.y = newPosition.y;
    }
    public void SetPiece(Sprite pieceSprite, uint newPieceColor) {
        pieceDisplayerMono.SetSprite(pieceSprite);
        isEmpty = false;
        pieceColor = newPieceColor;
    }
    public void SetEmpty() {
        pieceDisplayerMono.SetEmpty();
        isEmpty = true;
    }

    public void MarkAsPotentialMove() { isPotentialMove = true; }
    public void UnmarkAsPotentialMove() { isPotentialMove = false; }

    public Vector2Int GetPosition() { return position; }
    public bool IsEmpty() { return isEmpty; }
    public bool IsPotentialMove() { return isPotentialMove; }
    public uint GetPieceColor() { return pieceColor; }

    // Used to change the size of the cell
    public void SetSize(float newSize) {
        // Updates the collider and image of the cell
        cellRectTransform.sizeDelta = new Vector2(newSize, newSize);
        cellCollider.size = new Vector2(newSize, newSize);
        squareSelectionMono.SetSize(newSize);
        pieceDisplayerMono.SetSize(newSize);
    }

    // Activated whenever somebody clicks on the cell
    public void OnPointerDown(PointerEventData pointerEventData) {
        controller.RegisterSquareClicked(this);
    }
}
