﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WaifuPatterns : Singleton<WaifuPatterns> {
    public static GridManager gridManager;
    public new static WaifuPatterns instance = null;
    static Dictionary<WaifuScriptableObject.Type, PatternDelegate> waifuDictionary;
    delegate List<List<int[]>> PatternDelegate(Tile[][] grid);
    PatternDelegate myDelegate;

    private void Awake() {
        gridManager = FindObjectOfType<GridManager>();
        waifuDictionary = new Dictionary<WaifuScriptableObject.Type, PatternDelegate>();
        waifuDictionary.Add(WaifuScriptableObject.Type.BASIC, Basic);
    }

    public static List<List<int[]>> MapTypeToAllPositions(WaifuScriptableObject.Type type) {
        Tile[][] grid = gridManager.GetGrid();
        return RemoveInvalidPositions(waifuDictionary[type](grid), grid);
    }

    static List<List<int[]>> Basic(Tile[][] grid) {
        List<List<int[]>> validPositions = new List<List<int[]>>();
        bool invalidPositions = false;

        foreach (Tile[] row in grid) {
            foreach (Tile tile in row) {
                invalidPositions = false;
                for (int i = tile.col; i <= tile.col + 3; i++) {
                    if (row.ElementAtOrDefault(i) == null) {
                        invalidPositions = true;
                        break;
                    }
                }
                if (invalidPositions) {
                    continue;
                }
                List<int[]> validPosition = new List<int[]>();
                for (int i = tile.col; i <= tile.col + 3; i++) {
                    validPosition.Add(new int[] { tile.row, i });
                }
                validPositions.Add(validPosition);
            }
        }
        return validPositions;
    }

    static List<List<int[]>> RemoveInvalidPositions(List<List<int[]>> waifuPositions, Tile[][] grid) {
        List<List<int[]>> result = new List<List<int[]>>();
        foreach (List<int[]> position in waifuPositions) {
            bool validPosition = position.All((int[] coord) => !grid[coord[0]][coord[1]].HasWaifu());
            if (validPosition) {
                result.Add(position);
            }
        }
        return result;
    }
}
