using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour {
    public delegate void TileClickAction(Waifu waifu);
    public static event TileClickAction OnTileClick;
    public int col;
    public int row;
    Waifu waifu;

    public Sprite flipPlaceholder;

    Image image;

    private void Awake() {
        image = GetComponent<Image>();
    }

    private void Start() {
        PuzzleManager.OnPreparePuzzle += ResetId;
    }

    public void Click() {
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

    void ResetId() {
        if (waifu != null) {
            waifu.ResetId();
            GetComponent<DebugId>().SetId(waifu.id);
        }
    }
}
