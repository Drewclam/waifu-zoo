using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T> {
    protected static T instance = null;
    public static T Instance() {
        if (instance == null) {
            instance = FindObjectOfType<T>();
            if (instance == null) {
                Debug.LogError("Singleton instance not found.");
            }
        }
        return instance;
    }
}
