using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WaifuPatterns : MonoBehaviour {
    public enum WAIFU_TYPES {
        BASIC
    }
    public static WaifuPatterns Instance;
    static Dictionary<WAIFU_TYPES, PatternDelegate> waifuDictionary;
    delegate List<List<int[]>> PatternDelegate(Tile[][] grid, int row, int col);
    PatternDelegate myDelegate;

    private void Awake() {
        Instance = this;
    }

    WaifuPatterns() {
        waifuDictionary = new Dictionary<WAIFU_TYPES, PatternDelegate>();
        waifuDictionary.Add(WAIFU_TYPES.BASIC, Basic);
    }

    public static List<List<int[]>> MapPatternToValidPositions(WAIFU_TYPES type, Tile[][] grid, int row, int col) {
        return waifuDictionary[type](grid, row, col);
    }

    List<List<int[]>> Basic(Tile[][] grid, int rowIndex, int colIndex) {
        Tile[] row = grid[rowIndex];
        List<List<int[]>> validPositions = new List<List<int[]>>();
        bool invalidPositions = false;

        foreach (Tile tile in row) {
            invalidPositions = false;
            for (int i = colIndex; i <= colIndex + 3; i++) {
                if (row.ElementAtOrDefault(i) == null) {
                    invalidPositions = true;
                    break;
                }
            }

            if (invalidPositions) {
                continue;
            }

            List<int[]> validPosition = new List<int[]>();
            for (int i = colIndex; i <= colIndex + 3; i++) {
                validPosition.Add(new int[] { rowIndex, i });
            }
        }
        return validPositions;
    }
}
