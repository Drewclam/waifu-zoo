using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PuzzleManager : MonoBehaviour {
    static int WAIFU_GROUP_ID = 0;

    public WaifuScriptableObject basicWaifu;
    public delegate void PreparePuzzle();
    public static event PreparePuzzle OnPreparePuzzle;
    public delegate void PuzzleReady(List<WaifuScriptableObject.Type> List);
    public static event PuzzleReady OnPuzzleReady;
    public GridManager gridManager;
    int MAX_ATTEMPTS = 10;
    int attempts;
    int totalWaifuRemaining;
    List<WaifuScriptableObject.Type> waifusToSpawn;

    private void Start() {
        InitPuzzle();
    }

    private void OnEnable() {
        Tile.OnTileClick += HandleTileClick;
    }

    private void OnDisable() {
        Tile.OnTileClick -= HandleTileClick;
    }

    void HandleTileClick(WaifuScriptableObject waifu) {
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
        OnPuzzleReady?.Invoke(waifusToSpawn);
    }

    void LoadWaifusToSpawn() {
        waifusToSpawn = new List<WaifuScriptableObject.Type>();
        waifusToSpawn.Add(WaifuScriptableObject.Type.BASIC);
        waifusToSpawn.Add(WaifuScriptableObject.Type.BASIC);
        totalWaifuRemaining = waifusToSpawn.Count;
    }

    void PrepareWaifus() {
        foreach (WaifuScriptableObject.Type type in waifusToSpawn) {
            List<List<int[]>> waifuPositions = new List<List<int[]>>();
            waifuPositions = WaifuPatterns.MapTypeToAllPositions(type);
            int randomWaifuPositionIndex = UnityEngine.Random.Range(0, waifuPositions.Count);
            Debug.Log("Positions: " + waifuPositions.Count);
            List<int[]> randomWaifuPosition = waifuPositions[randomWaifuPositionIndex];
            SpawnBasicWaifu(randomWaifuPosition);
        }
    }


    void SpawnBasicWaifu(List<int[]> positions) {
        foreach (int[] position in positions) {
            gridManager.GetTile(position[0], position[1]).SetWaifu(basicWaifu, WAIFU_GROUP_ID);
        }
        WAIFU_GROUP_ID++;
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

