using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleStatus : MonoBehaviour {
    GuessProgress[] items;

    private void Awake() {
        items = GetComponentsInChildren<GuessProgress>();
    }

    private void Start() {
        foreach (GuessProgress item in items) {
            item.SetName("blah");
            item.SetTilesRemaining(5);
        }
    }
    private void OnEnable() {
        Tile.OnTileClick += UpdateState;
    }

    public void LoadWaifus() {

    }

    public void UpdateState(Waifu waifu) {

    }
}
