using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour {
    public delegate void TileClickAction(WaifuScriptableObject waifu, int groupId);
    public static event TileClickAction OnTileClick;
    public int col;
    public int row;
    bool selected = false;
    WaifuScriptableObject waifu;

    public Sprite bottomBasicSprite;
    public Sprite topBasicSprite;
    public Sprite bottomAppleSprite;
    public Sprite topAppleSprite;
    public Sprite bottomCoinSprite;
    public Sprite topCoinSprite;
    public Sprite bottomSockSprite;
    public Sprite topSockSprite;

    Sprite topWaifuSprite;
    Sprite bottomWaifuSprite;
    Image image;

    int id = -1;

    private void Awake() {
        image = GetComponent<Image>();
    }

    private void OnEnable() {
        PuzzleManager.OnPreparePuzzle += Init;
    }

    private void OnDisable() {
        PuzzleManager.OnPreparePuzzle -= Init;
    }

    public void Init() {
        image.sprite = topBasicSprite;
    }

    public void Click() {
        if (selected) {
            return;
        }
        selected = true;

        if (bottomWaifuSprite) {
            image.sprite = bottomWaifuSprite;
        }
        WaifuScriptableObject tempWaifu = waifu;
        int tempGroupId = id;
        SetWaifu(null, -1);
        OnTileClick(tempWaifu, tempGroupId);
    }

    public void SetColumn(int value) {
        col = value;
    }

    public void SetRow(int value) {
        row = value;
    }

    public void SetWaifu(WaifuScriptableObject value, int tileGroupId) {
        waifu = value;
        id = tileGroupId;
        GetComponent<DebugId>().SetId(id);
        if (waifu != null) {
            topWaifuSprite = value.enabledSprite;
            bottomWaifuSprite = value.disabledSprite;
            image.sprite = topWaifuSprite;
        }
    }

    public bool HasWaifu() {
        return waifu == null ? false : true;
    }

    public int GetId() {
        return id;
    }
}
