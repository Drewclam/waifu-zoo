using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour {
    public delegate void TileClickAction();
    public static event TileClickAction OnEmptyTileClick;
    public bool isValid;
    public int col;
    public int row;

    public Sprite flipPlaceholder;

    Image image;

    private void Awake() {
        image = GetComponent<Image>();
    }

    public void Click() {
        image.sprite = flipPlaceholder;
        if (isValid) {
            ValidClick();
            return;
        }
        InvalidClick();
    }

    public void SetColumn(int value) {
        col = value;
    }

    public void SetRow(int value) {
        row = value;
    }

    public void SetSprite() {
        image.sprite = null;

    }

    void ValidClick() {

    }

    void InvalidClick() {
        OnEmptyTileClick();
    }
}
