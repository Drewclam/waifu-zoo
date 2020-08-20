using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridManager : MonoBehaviour {
    public Transform tiles;
    Tile[][] grid = new Tile[5][];

    int MAX_ROW = 5;
    int MAX_COL = 5;

    private void OnEnable() {
        PuzzleManager.OnPreparePuzzle += InitGrid;
    }

    private void OnDisable() {
        PuzzleManager.OnPreparePuzzle -= InitGrid;
    }

    public void InitGrid() {
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

    public bool AnyTileWithWaifuId(int id) {
        return grid.Any((Tile[] row) => {
            bool res = row.Any((Tile tile) => {
                if (tile.HasWaifu()) {
                    if (tile.GetId() == id) {
                        return true;
                    };
                }
                return false;
            });
            return res;
        });
    }

    public Tile[][] GetGrid() {
        return grid;
    }

    public Tile GetTile(int row, int col) {
        return grid[row][col];
    }
}
