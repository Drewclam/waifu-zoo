using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour {
    public delegate void TileClickAction(Waifu waifu);
    public static event TileClickAction OnTileClick;
    public int col;
    public int row;
    bool selected = false;
    Waifu waifu;

    public Sprite flipPlaceholder;

    Image image;

    private void Awake() {
        image = GetComponent<Image>();
    }

    public void Click() {
        if (selected) {
            return;
        }
        selected = true;
        image.sprite = flipPlaceholder;
        Waifu tempWaifu = waifu;
        SetWaifu(null);
        OnTileClick(tempWaifu);
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

    public void SetWaifu(Waifu value) {
        waifu = value;
        if (waifu != null) {
            GetComponent<DebugId>().SetId(waifu.id);
        }
    }

    public bool HasWaifu() {
        return waifu == null ? false : true;
    }

    public int GetId() {
        return waifu.id;
    }
}
