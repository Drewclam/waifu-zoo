using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaifuLevel : MonoBehaviour {
    public Text waifuName;
    public Text waifuLevel;

    public void SetName(string value) {
        waifuName.text = value;
    }

    public void SetLevel(int value) {
        waifuLevel.text = value.ToString();
    }
}
