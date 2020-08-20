using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Room : MonoBehaviour {
    public WaifuLevel waifuLevel;
    public Money money;
    GameManager gameManager;

    private void Awake() {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Start() {
        InitRoom();
    }

    private void OnEnable() {
        GameManager.OnAddMoney += UpdateMoney;
    }

    private void OnDisable() {
        GameManager.OnAddMoney -= UpdateMoney;
    }

    public void LoadPuzzle() {
        SceneManager.LoadScene("Puzzle");
    }

    void InitRoom() {
        waifuLevel.SetName("Basic Waifu");
        waifuLevel.SetLevel(gameManager.basicWaifuLevel);
        UpdateMoney(gameManager.money);
    }

    void UpdateMoney(float value) {
        money.SetMoney(value);
    }
}
