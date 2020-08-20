using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager> {
    public bool developmentMode = true;
    public int basicWaifuLevel = 0;

    public void LevelUpBasicWaifu() {
        basicWaifuLevel++;
    }
}
