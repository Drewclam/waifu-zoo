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

    public bool AnyTileGroupWithId(int groupId) {
        return grid.Any((Tile[] row) => {
            return row.Any((Tile tile) => {
                if (tile.HasWaifu()) {
                    if (groupId != -1 && tile.GetId() == groupId) {
                        return true;
                    };
                }
                return false;
            });
        });
    }

    public int GetTileCountById(int groupId) {
        int tileCount = 0;
        foreach (Tile[] row in grid) {
            foreach (Tile tile in row) {
                if (tile.GetId() == groupId) {
                    tileCount++;
                }
            }
        }
        return tileCount;
    }

    public Tile[][] GetGrid() {
        return grid;
    }

    public Tile GetTile(int row, int col) {
        return grid[row][col];
    }
}
