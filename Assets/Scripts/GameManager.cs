using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager> {
    public delegate void AddMoney(float money);
    public static event AddMoney OnAddMoney;
    public bool developmentMode = true;
    public int basicWaifuLevel = 0;
    public float moneyRate = 1;
    public float money = 0;

    private void Update() {
        money += (moneyRate * Time.deltaTime);
        OnAddMoney?.Invoke(Mathf.Round(money));
    }

    public void LevelUpBasicWaifu() {
        basicWaifuLevel++;
        moneyRate += 0.5f;
    }
}
