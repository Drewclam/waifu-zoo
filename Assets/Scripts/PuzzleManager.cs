using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PuzzleManager : MonoBehaviour {
    static int WAIFU_GROUP_ID = 0;

    public struct PuzzleWaifu {
        public WaifuScriptableObject waifu;
        public int groupId;
        public List<int[]> positions;
        public int tilesLeft;
        public PuzzleWaifu(WaifuScriptableObject _waifu, List<int[]> _positions, GridManager gridManager) {
            waifu = _waifu;
            groupId = WAIFU_GROUP_ID;
            positions = _positions;
            tilesLeft = gridManager.GetTileCountById(WAIFU_GROUP_ID);

            WAIFU_GROUP_ID++;
        }

        public void SetTilesLeft(int value) {
            tilesLeft = value;
        }
    }

    public WaifuScriptableObject basicWaifu;
    public GridManager gridManager;

    public delegate void PreparePuzzle();
    public static event PreparePuzzle OnPreparePuzzle;

    public delegate void PuzzleReady(List<PuzzleWaifu> waifus);
    public static event PuzzleReady OnPuzzleReady;

    public delegate void PuzzleChange(List<PuzzleWaifu> loadedWaifus);
    public static event PuzzleChange OnPuzzleChange;

    public List<PuzzleWaifu> waifusToSpawn;
    List<WaifuScriptableObject> HARD_CODED_WAIFUS_TO_SPAWN;
    int MAX_ATTEMPTS = 10;
    int attempts;
    int totalWaifuRemaining;

    private void Start() {
        InitPuzzle();
    }

    private void OnEnable() {
        Tile.OnTileClick += HandleTileClick;
    }

    private void OnDisable() {
        Tile.OnTileClick -= HandleTileClick;
    }

    void UpdatePuzzleCount() {
        foreach (PuzzleWaifu waifu in waifusToSpawn) {
            int tilesLeft = gridManager.GetTileCountById(waifu.groupId);
            waifu.SetTilesLeft(tilesLeft);
        }
    }

    void HandleTileClick(WaifuScriptableObject waifu, int groupId) {
        if (waifu == null) {
            Debug.Log("Wrong guess, attempts remianing: " + attempts);
            DecrementAttempts();
            return;
        }

        Debug.Log("Correct guess, waifu remaining: " + totalWaifuRemaining);
        int tilesLeft = gridManager.GetTileCountById(groupId);
        Debug.Log("Tiles left: " + tilesLeft);
        if (tilesLeft < 1) {
            totalWaifuRemaining -= 1;
        }
        Debug.Log("Waifu remaining: " + totalWaifuRemaining);

        OnPuzzleChange?.Invoke(waifusToSpawn);

        if (totalWaifuRemaining < 1) {
            SceneManager.LoadScene("Win Room");
        }
    }

    void InitPuzzle() {
        OnPreparePuzzle?.Invoke();
        attempts = MAX_ATTEMPTS;
        LoadWaifusToSpawn();
        PrepareWaifus(HARD_CODED_WAIFUS_TO_SPAWN);
        //SpawnWaifus();
        OnPuzzleReady?.Invoke(waifusToSpawn);
    }

    void LoadWaifusToSpawn() {
        HARD_CODED_WAIFUS_TO_SPAWN = new List<WaifuScriptableObject>();
        HARD_CODED_WAIFUS_TO_SPAWN.Add(basicWaifu);
        HARD_CODED_WAIFUS_TO_SPAWN.Add(basicWaifu);
        //HARD_CODED_WAIFUS_TO_SPAWN.Add(basicWaifu);
        //HARD_CODED_WAIFUS_TO_SPAWN.Add(basicWaifu);
        //HARD_CODED_WAIFUS_TO_SPAWN.Add(basicWaifu);
        //HARD_CODED_WAIFUS_TO_SPAWN.Add(basicWaifu);
    }

    void PrepareWaifus(List<WaifuScriptableObject> waifus) {
        waifusToSpawn = new List<PuzzleWaifu>();
        foreach (WaifuScriptableObject waifu in waifus) {
            List<List<int[]>> waifuPositions = new List<List<int[]>>();
            waifuPositions = WaifuPatterns.MapTypeToAllPositions(waifu.type);
            int randomWaifuPositionIndex = UnityEngine.Random.Range(0, waifuPositions.Count);
            List<int[]> randomWaifuPosition = waifuPositions[randomWaifuPositionIndex];
            PuzzleWaifu puzzleWaifu = new PuzzleWaifu(waifu, randomWaifuPosition, gridManager);
            waifusToSpawn.Add(puzzleWaifu);
            SpawnWaifu(puzzleWaifu);
        }
        totalWaifuRemaining = waifusToSpawn.Count;
    }

    void SpawnWaifu(PuzzleWaifu puzzleWaifu) {
        foreach (int[] position in puzzleWaifu.positions) {
            gridManager.GetTile(position[0], position[1]).SetWaifu(puzzleWaifu.waifu, puzzleWaifu.groupId);
        }
    }

    //void SpawnWaifus() {
    //    foreach (PuzzleWaifu puzzleWaifu in waifusToSpawn) {
    //        foreach (int[] position in puzzleWaifu.positions) {
    //            gridManager.GetTile(position[0], position[1]).SetWaifu(puzzleWaifu.waifu, puzzleWaifu.groupId);
    //        }
    //    }
    //}


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

