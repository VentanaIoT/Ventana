using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class persistentGameObjectScript : MonoBehaviour {

    // determines whether previous world anchors should be loaded when opening Ventana
    public bool loadWorldAnchors;

	// Use this for initialization
	void Start () {

        // ensures this gameobject persists between Unity scenes and next scene can access loadWorldAnchors
        DontDestroyOnLoad(this);
	}
	
	// Update is called once per frame
	void Update () {
	}
}
