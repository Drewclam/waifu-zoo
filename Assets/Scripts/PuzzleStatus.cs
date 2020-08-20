using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleStatus : MonoBehaviour {
    public GameObject statusObject;
    GuessProgress[] items;

    private void Awake() {
        items = GetComponentsInChildren<GuessProgress>();
    }

    private void OnEnable() {
        //Tile.OnTileClick += UpdateState;
        PuzzleManager.OnPuzzleReady += Init;
    }

    private void OnDisable() {
        PuzzleManager.OnPuzzleReady -= Init;
    }

    public void LoadWaifus() {

    }

    public void Init(List<WaifuScriptableObject.Type> types) {
        foreach (GameObject statusObject in transform) {
            Destroy(statusObject);
        }
        foreach (WaifuScriptableObject.Type type in types) {
            GameObject instance = Instantiate(statusObject, transform);
            GuessProgress guessProgress = instance.GetComponent<GuessProgress>();
            //guessProgress.SetName(WaifuScriptableObject.MapTypeToName(type));
            guessProgress.SetTilesRemaining(4);
        }
    }
}
