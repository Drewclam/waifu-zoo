using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuessProgress : MonoBehaviour {
    public Text waifuName;
    public Text tilesRemaining;
    public Image image;

    public void SetName(string name) {
        waifuName.text = name;
    }

    public void SetTilesRemaining(int value) {
        tilesRemaining.text = value.ToString();
    }

    public void SetSprite(Sprite sprite) {
        image.sprite = sprite;
    }
}
