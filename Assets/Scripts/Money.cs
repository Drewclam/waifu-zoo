using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Money : MonoBehaviour {
    public Text moneyText;

    public void SetMoney(float value) {
        moneyText.text = "Money:    " + value.ToString();
    }
}
