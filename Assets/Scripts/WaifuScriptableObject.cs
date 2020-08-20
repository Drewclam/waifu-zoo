using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Waifu", menuName = "ScriptableObjects/WaifuScriptableObject", order = 1)]
public class WaifuScriptableObject : ScriptableObject {
    public enum Type {
        NULL,
        BASIC
    }
    public string waifuName;
    public Sprite enabledSprite;
    public Sprite disabledSprite;
    public Type type;
    // TODO: grab value from pattern helper
    public int startingTileCount;
}
