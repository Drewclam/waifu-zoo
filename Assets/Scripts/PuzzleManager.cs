using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PuzzleManager : MonoBehaviour {
    public Transform tiles;
    Tile[][] grid = new Tile[5][];
    int MAX_ATTEMPTS = 10;
    int MAX_COL = 5;
    int MAX_ROW = 5;
    int attempts;
    int tilesLeft;
    int totalWaifuRemaining;

    private void Start() {
        attempts = MAX_ATTEMPTS;
        Tile.OnTileClick += HandleTileClick;
        InitGrid();
        InitPuzzle();
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
        List<int[]> validPos = new List<int[]>();
        foreach (Tile[] row in grid) {
            foreach (Tile tile in row) {
                if (SheepPattern(tile.col)) {
                    validPos.Add(new int[] { tile.row, tile.col });
                }
            }
        }

        int spots = 2;
        int waifuId = 0;
        totalWaifuRemaining = spots;

        for (int i = 0; i < spots; i++) {
            int randomIndex = UnityEngine.Random.Range(0, validPos.Count);
            int row = validPos[randomIndex][0];
            int col = validPos[randomIndex][1];
            SpawnSheep(row, col, waifuId);
            waifuId++;
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

    bool SheepPattern(int index) {
        foreach (Tile[] row in grid) {
            Tile lastTile = Array.Find(row, (Tile tile) => tile.col == (index + 3));
            if (lastTile != null) {
                return true;
            }
        }
        return false;
    }

    void SpawnSheep(int row, int col, int id) {
        for (int i = col; i < col + 4; i++) {
            grid[row][i].SetSprite();
            grid[row][i].SetId(id);
        }
    }
}
