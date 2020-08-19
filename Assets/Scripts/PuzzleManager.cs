using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PuzzleManager : MonoBehaviour {
    public delegate void PreparePuzzle();
    public static event PreparePuzzle OnPreparePuzzle;
    public delegate void PuzzleReady();
    public static event PuzzleReady OnPuzzleReady;
    public GridManager gridManager;
    int MAX_ATTEMPTS = 10;
    int attempts;
    int totalWaifuRemaining;
    List<string> waifusToSpawn;

    private void Start() {
        InitPuzzle();
    }

    private void OnEnable() {
        Tile.OnTileClick += HandleTileClick;
    }

    private void OnDisable() {
        Tile.OnTileClick -= HandleTileClick;
    }

    void HandleTileClick(Waifu waifu) {
        if (waifu == null) {
            Debug.Log("Wrong guess, attempts remianing: " + attempts);
            DecrementAttempts();
            return;
        }

        Debug.Log("Correct guess, waifu remaining: " + totalWaifuRemaining);

        if (!gridManager.AnyTileWithWaifuId(waifu.id)) {
            totalWaifuRemaining -= 1;
        }
        Debug.Log("Waifu remaining: " + totalWaifuRemaining);

        if (totalWaifuRemaining < 1) {
            SceneManager.LoadScene("Win Room");
        }
    }

    void InitPuzzle() {
        OnPreparePuzzle?.Invoke();
        attempts = MAX_ATTEMPTS;
        LoadWaifusToSpawn();
        PrepareWaifus();
        OnPuzzleReady?.Invoke();
    }

    void LoadWaifusToSpawn() {
        waifusToSpawn = new List<string>();
        waifusToSpawn.Add("Basic");
        waifusToSpawn.Add("Basic");
        totalWaifuRemaining = waifusToSpawn.Count;
    }

    void PrepareWaifus() {
        foreach (string type in waifusToSpawn) {
            List<List<int[]>> waifuPositions = new List<List<int[]>>();
            waifuPositions = WaifuPatterns.MapPatternToValidPositions(WaifuPatterns.WAIFU_TYPES.BASIC);
            int randomWaifuPositionIndex = UnityEngine.Random.Range(0, waifuPositions.Count);
            List<int[]> randomWaifuPosition = waifuPositions[randomWaifuPositionIndex];
            SpawnBasicWaifu(randomWaifuPosition);
        }
    }


    void SpawnBasicWaifu(List<int[]> positions) {
        Waifu basicWaifu = new Waifu();
        foreach (int[] position in positions) {
            //gridManager.SetTileId(position[0], position[1], waifuId);
            gridManager.GetTile(position[0], position[1]).SetWaifu(basicWaifu);
        }
    }


    void DecrementAttempts() {
        attempts--;

        if (attempts < 1) {
            ExitPuzzle();
        }
    }

    void ExitPuzzle() {
        SceneManager.LoadScene("Room");
    }
}

