using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour {
    public Sprite flipPlaceholder;
    Image image;

    private void Awake() {
        image = GetComponent<Image>();
    }

    public void Click() {
        // flip
        image.sprite = flipPlaceholder;
        // emit event that tile has been selected
    }
}
