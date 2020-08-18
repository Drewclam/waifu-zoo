using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singletons : MonoBehaviour {
    static Singletons instance = null;
    private void Awake() {
        DontDestroyOnLoad(this);
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
        }
    }
}
