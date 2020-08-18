using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PuzzleManager : MonoBehaviour {
    public delegate void NewPuzzle();
    public static event NewPuzzle OnNewPuzzle;
    public GridManager gridManager;
    int MAX_ATTEMPTS = 10;
    int attempts;
    int totalWaifuRemaining;
    List<string> waifusToSpawn;
    int waifuId = 0;

    private void Start() {
        InitPuzzle();
    }

    private void OnEnable() {
        Tile.OnTileClick += HandleTileClick;
    }

    private void OnDisable() {
        Tile.OnTileClick -= HandleTileClick;
    }

    void HandleTileClick(int id) {
        if (id == -1) {
            Debug.Log("Wrong guess, attempts remianing: " + attempts);
            DecrementAttempts();
            return;
        }

        Debug.Log("Correct guess, waifu remaining: " + totalWaifuRemaining);

        if (!gridManager.HasTileWithId(id)) {
            totalWaifuRemaining -= 1;
        }

        if (totalWaifuRemaining < 1) {
            SceneManager.LoadScene("Win Room");
        }
    }

    void InitPuzzle() {
        OnNewPuzzle?.Invoke();
        attempts = MAX_ATTEMPTS;
        LoadWaifusToSpawn();
        PrepareWaifus();
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
            Debug.Log("Hello" + randomWaifuPositionIndex + " " + waifuPositions.Count);
            List<int[]> randomWaifuPosition = waifuPositions[randomWaifuPositionIndex];
            SpawnWaifu(randomWaifuPosition);
        }
    }


    void SpawnWaifu(List<int[]> positions) {
        foreach (int[] position in positions) {
            gridManager.SetTileId(position[0], position[1], waifuId);
        }
        waifuId++;
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

