using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Room : MonoBehaviour {
    public WaifuLevel waifuLevel;
    GameManager gameManager;

    private void Awake() {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Start() {
        InitRoom();
    }

    public void LoadPuzzle() {
        SceneManager.LoadScene("Puzzle");
    }

    void InitRoom() {
        waifuLevel.SetName("Basic Waifu");
        waifuLevel.SetLevel(gameManager.basicWaifuLevel);
    }
}
