using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PieceDisplayerMono : MonoBehaviour
{
    public Image image;
    public RectTransform rectTransform;

    public void SetSize(float newSize) {

        // Updates the collider and image of the cell
        rectTransform.sizeDelta = new Vector2(newSize, newSize);
    }

    public void SetSprite(Sprite newSprite) {
        image.sprite = newSprite;
        image.color = new Color32(255,255,255,255);
    }

    public void SetEmpty() {
        image.color = new Color32(0,0,0,0);
    }
}
