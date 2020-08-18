using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WaifuPatterns : MonoBehaviour {
    public enum WAIFU_TYPES {
        BASIC
    }
    public static GridManager gridManager;
    public static WaifuPatterns Instance;
    static Dictionary<WAIFU_TYPES, PatternDelegate> waifuDictionary;
    delegate List<List<int[]>> PatternDelegate(Tile[][] grid);
    PatternDelegate myDelegate;

    private void Awake() {
        Instance = this;
    }

    static WaifuPatterns() {
        waifuDictionary = new Dictionary<WAIFU_TYPES, PatternDelegate>();
        waifuDictionary.Add(WAIFU_TYPES.BASIC, Basic);
    }

    public static List<List<int[]>> MapPatternToValidPositions(WAIFU_TYPES type) {
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
            bool validPosition = position.All((int[] coord) => grid[coord[0]][coord[1]].id == -1);
            if (validPosition) {
                result.Add(position);
            }
        }
        return result;
    }
}
