using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleStatus : MonoBehaviour {
    public GameObject statusItemPrefab;
    //PuzzleStatusItem[] items;

    private void Awake() {
        //items = GetComponentsInChildren<PuzzleStatusItem>();
    }

    private void OnEnable() {
        PuzzleManager.OnPuzzleReady += Init;
        PuzzleManager.OnPuzzleChange += UpdateWaifus;
    }

    private void OnDisable() {
        PuzzleManager.OnPuzzleReady -= Init;
        PuzzleManager.OnPuzzleChange -= UpdateWaifus;
    }

    public void UpdateWaifus(List<PuzzleManager.PuzzleWaifu> loadedWaifus) {
        Debug.Log("Update");
        foreach (PuzzleManager.PuzzleWaifu puzzleWaifu in loadedWaifus) {
            PuzzleStatusItem[] statusItems = GetComponentsInChildren<PuzzleStatusItem>();
            foreach (PuzzleStatusItem statusItem in statusItems) {
                if (statusItem.GetGroupId() == puzzleWaifu.groupId) {
                    statusItem.SetName(puzzleWaifu.waifu.waifuName);
                    statusItem.SetTilesRemaining(puzzleWaifu.tilesLeft);
                    statusItem.SetSprite(puzzleWaifu.waifu.enabledSprite);
                }
            }
        }
    }

    public void Init(List<PuzzleManager.PuzzleWaifu> loadedWaifus) {
        foreach (GameObject statusObject in transform) {
            Destroy(statusObject);
        }
        foreach (PuzzleManager.PuzzleWaifu puzzleWaifu in loadedWaifus) {
            GameObject instance = Instantiate(statusItemPrefab, transform);
            PuzzleStatusItem guessProgress = instance.GetComponent<PuzzleStatusItem>();
            guessProgress.SetName(puzzleWaifu.waifu.waifuName);
            guessProgress.SetTilesRemaining(puzzleWaifu.tilesLeft);
            guessProgress.SetSprite(puzzleWaifu.waifu.enabledSprite);
            guessProgress.SetGroupId(puzzleWaifu.groupId);
        }
    }
}
