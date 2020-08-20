using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleStatusItem : MonoBehaviour {
    public Text waifuName;
    public Text tilesRemaining;
    public Image image;
    public int groupId;

    public void SetName(string name) {
        waifuName.text = name;
    }

    public void SetTilesRemaining(int value) {
        tilesRemaining.text = value.ToString();
    }

    public void SetSprite(Sprite sprite) {
        image.sprite = sprite;
    }

    public void SetGroupId(int value) {
        groupId = value;
    }

    public int GetGroupId() {
        return groupId;
    }
}
