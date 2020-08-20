using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour {
    public delegate void TileClickAction(WaifuScriptableObject waifu, int groupId);
    public static event TileClickAction OnTileClick;
    public delegate void SetWaifuAction(int groupId);
    public static event SetWaifuAction OnSetWaifu;
    public int col;
    public int row;
    bool selected = false;
    WaifuScriptableObject waifu;

    public Sprite selectedTileSprite;
    public List<Sprite> tileSprites;

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
        LoadRandomTileSprite();
    }

    public void Click() {
        if (selected) {
            return;
        }
        selected = true;

        if (bottomWaifuSprite) {
            image.sprite = bottomWaifuSprite;
        } else {
            image.sprite = selectedTileSprite;
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
        OnSetWaifu?.Invoke(id);
        if (waifu != null) {
            bottomWaifuSprite = value.disabledSprite;
        }
    }

    public bool HasWaifu() {
        return waifu == null ? false : true;
    }

    public int GetId() {
        return id;
    }

    void LoadRandomTileSprite() {
        int randomIndex = Random.Range(0, tileSprites.Count);
        image.sprite = tileSprites[randomIndex];
    }
}
