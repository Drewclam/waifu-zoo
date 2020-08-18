using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PuzzleManager : MonoBehaviour {
    int MAX_ATTEMPTS = 10;
    int attempts;

    private void Start() {
        attempts = MAX_ATTEMPTS;
        Tile.OnEmptyTileClick += DecrementAttempts;
    }

    public void DecrementAttempts() {
        attempts--;

        if (attempts < 1) {
            ExitPuzzle();
        }
    }

    void ExitPuzzle() {
        SceneManager.LoadScene("Room");
    }
}
