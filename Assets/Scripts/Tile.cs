using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour {
    public delegate void TileClickAction(int id);
    public static event TileClickAction OnTileClick;
    public int id = -1;
    public int col;
    public int row;

    public Sprite flipPlaceholder;

    Image image;

    private void Awake() {
        image = GetComponent<Image>();
    }

    public void Click() {
        image.sprite = flipPlaceholder;
        int oldId = id;
        SetId(-1);
        OnTileClick(oldId);
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

    public void SetId(int value) {
        id = value;
    }

    //void ValidClick() {
    //    OnTileClick(true, id);
    //}

    //void InvalidClick() {
    //    OnTileClick(false, id);
    //}
}
