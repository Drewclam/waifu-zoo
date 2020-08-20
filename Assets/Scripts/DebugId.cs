using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugId : MonoBehaviour {
    public Text text;
    GameManager gameManager;

    private void Awake() {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Start() {
        if (gameManager.developmentMode) {
            gameObject.SetActive(true);
        } else {
            gameObject.SetActive(false);
        }
    }

    private void OnEnable() {
        if (gameManager.developmentMode) {
            Tile.OnSetWaifu += SetId;
        }
    }

    private void OnDisable() {
        Tile.OnSetWaifu -= SetId;
    }

    public void SetId(int id) {
        text.text = id.ToString();
    }
}
