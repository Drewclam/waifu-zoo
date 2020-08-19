using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugId : MonoBehaviour {
    public Text text;

    public void SetId(int id) {
        text.text = id.ToString();
    }
}
