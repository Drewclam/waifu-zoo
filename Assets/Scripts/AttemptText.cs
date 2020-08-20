using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttemptText : MonoBehaviour {
    public Text text;

    public void SetAttempts(int value) {
        text.text = "Attempts: " + value.ToString();
    }
}
