using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridManager : MonoBehaviour {
    public Transform tiles;
    Tile[][] grid = new Tile[5][];

    int MAX_ROW = 5;
    int MAX_COL = 5;

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

    public void SetTileId(int row, int col, int id) {
        grid[row][col].SetId(id);
    }

    public bool HasTileWithId(int id) {
        return grid.Any((Tile[] row) => {
            bool res = row.Any((Tile tile) => tile.id == id);
            return res;
        });
    }

    public Tile[][] GetGrid() {
        return grid;
    }
}
