using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waifu {
    static int ID_COUNTER = 0;

    public string waifuName {
        get {
            return _waifuName;
        }
    }
    public int id {
        get {
            return _id;
        }
    }
    string _waifuName;
    int _tiles;
    int _id = -1;
    Sprite _sprite;
    WaifuPatterns.WAIFU_TYPES _type;

    public Waifu() {
        //_waifuName = "Null";
        //_tiles = -1;
        //_sprite = null;
        //_type = WaifuPatterns.WAIFU_TYPES.NULL;
        //_id = -1;
        _waifuName = "Basic Waifu";
        _tiles = 4;
        _sprite = null;
        _type = WaifuPatterns.WAIFU_TYPES.BASIC;
        _id = ID_COUNTER;

        ID_COUNTER++;
    }

    public void ResetId() {
        _id = -1;
    }
}
