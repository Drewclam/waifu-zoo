using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PuzzleManager : MonoBehaviour {
    public Transform tiles;
    public delegate void NewPuzzle();
    public static event NewPuzzle OnNewPuzzle;
    Tile[][] grid = new Tile[5][];
    int MAX_ATTEMPTS = 10;
    int MAX_COL = 5;
    int MAX_ROW = 5;
    int attempts;
    int tilesLeft;
    int totalWaifuRemaining;
    List<string> waifusToSpawn;
    List<int[]> puzzleCoords;
    int waifuId = 0;

    private void Start() {
        attempts = MAX_ATTEMPTS;
        Tile.OnTileClick += HandleTileClick;
        InitGrid();
        InitPuzzle();
    }

    void HandleTileClick(int id) {
        if (id == -1) {
            Debug.Log("Wrong guess, attempts remianing: " + attempts);
            DecrementAttempts();
            return;
        }

        Debug.Log("Correct guess, waifu remaining: " + totalWaifuRemaining);

        bool waifuRemaining = grid.Any((Tile[] row) => {
            bool res = row.Any((Tile tile) => tile.id == id);
            return res;
        });


        if (!waifuRemaining) {
            totalWaifuRemaining -= 1;
        }

        if (totalWaifuRemaining < 1) {
            SceneManager.LoadScene("Win Room");
        }
    }

    void InitPuzzle() {
        OnNewPuzzle?.Invoke();
        PreparePuzzle();
    }

    void PreparePuzzle() {
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
            waifuPositions = WaifuPatterns.MapPatternToValidPositions(WaifuPatterns.WAIFU_TYPES.BASIC, grid);
            int randomWaifuPositionIndex = UnityEngine.Random.Range(0, waifuPositions.Count);
            List<int[]> randomWaifuPosition = waifuPositions[randomWaifuPositionIndex];
            SpawnWaifu(randomWaifuPosition);
        }
    }


    void SpawnWaifu(List<int[]> positions) {
        foreach (int[] position in positions) {
            //grid[position[0]][position[1]].SetSprite();
            grid[position[0]][position[1]].SetId(waifuId);
        }
        waifuId++;
    }

    void InitGrid() {
        int col = 0;
        int row = 0;
        Tile[] rowTiles = new Tile[5];
        foreach (Tile tile in tiles.GetComponentsInChildren<Tile>()) {
            tile.SetColumn(col);
            tile.SetRow(row);
            rowTiles[col] = tile;
            col++;
            if (col >= MAX_COL) {
                grid[row] = rowTiles;
                rowTiles = new Tile[5];
                col = 0;
                row++;
            }
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

