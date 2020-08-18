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
    List<WAIFU_TYPES> waifusToSpawn;
    List<int[]> puzzleCoords;
    int waifuId = 0;

    private void Start() {
        attempts = MAX_ATTEMPTS;
        Tile.OnTileClick += HandleTileClick;
        InitGrid();
        InitPuzzle();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.T)) {
            InitPuzzle();
        }
    }

    void HandleTileClick(int id) {
        if (id == -1) {
            DecrementAttempts();
            return;
        }

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
        SpawnWaifus();
    }

    void SpawnWaifus() {
    }


    void LoadWaifusToSpawn() {
        waifusToSpawn = new List<WAIFU_TYPES>();
        waifusToSpawn.Add(WAIFU_TYPES.BASIC);
        waifusToSpawn.Add(WAIFU_TYPES.BASIC);
        waifusToSpawn.Add(WAIFU_TYPES.BASIC);
    }

    void PrepareWaifus() {
        if (waifusToSpawn.Count < 1) {
            return;
        }

        WAIFU_TYPES nextWaifu = waifusToSpawn.First();
        waifusToSpawn.Remove(nextWaifu);

        PrepareWaifus();
        //foreach (WAIFU_TYPES type in waifusToSpawn) {
        //    puzzleCoords = new List<int[]>();
        //    switch (type) {
        //        case WAIFU_TYPES.BASIC:
        //            foreach (Tile[] row in grid) {
        //                foreach (Tile tile in row) {
        //                    if (SheepPattern(tile.row, tile.col)) {
        //                        puzzleCoords.Add(new int[] { tile.row, tile.col });
        //                        // store all possibel coords
        //                        // randomly spawn a basic waifu at coord
        //                        // continue to next waifu type
        //                    }
        //                }
        //            }
        //            SpawnWaifu(type);
        //            break;
        //        default:
        //            break;
        //    }
        //}

    }


    void SpawnWaifu(WAIFU_TYPES type) {
        totalWaifuRemaining = waifusToSpawn.Count;

        switch (type) {
            case WAIFU_TYPES.BASIC:
                if (puzzleCoords.Count < 1) {
                    break;
                }
                // check for new pos
                int randomIndex = UnityEngine.Random.Range(0, puzzleCoords.Count);
                int row = puzzleCoords[randomIndex][0];
                int col = puzzleCoords[randomIndex][1];
                SpawnBasicWaifu(row, col, waifuId);
                waifuId++;
                break;
            default:
                break;
        }
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

    void SpawnBasicWaifu(int row, int col, int id) {
        for (int i = col; i < col + 4; i++) {
            grid[row][i].SetSprite();
            grid[row][i].SetId(id);
        }
    }
}
