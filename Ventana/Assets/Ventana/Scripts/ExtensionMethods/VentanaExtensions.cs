using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VentanaExtensions {
    public static void DestroyChildren(this Transform root) {
        int childCount = root.childCount;
        for ( int i = 0; i < childCount; i++ ) {
            GameObject.Destroy(root.GetChild(0).gameObject);
        }
    }
}

