using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SquareSelectionMono : MonoBehaviour
{
    public Image selectionImage;
    public RectTransform rectTransform;

    void Start() {
        SetColor(new Color32(0,0,0,0));
    }

    public void SetColor(Color32 newColor) {
        selectionImage.color = newColor;
    }

    public void SetSize(float newSize) {

        // Updates the collider and image of the cell
        rectTransform.sizeDelta = new Vector2(newSize, newSize);
    }
}
