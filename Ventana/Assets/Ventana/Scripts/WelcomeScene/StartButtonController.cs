using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;
using System;
using UnityEngine.SceneManagement;

public class StartButtonController : MonoBehaviour, IInputClickHandler {

    public Transform persistentGO;
    public void OnInputClicked(InputClickedEventData eventData) {
        //Load the next scene
        persistentGameObjectScript pgo = persistentGO.GetComponent<persistentGameObjectScript>();
        pgo.loadWorldAnchors = false;
        SceneManager.LoadSceneAsync("Ventana", LoadSceneMode.Single);
        
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void CallOnInputClicked() {
        InputClickedEventData data = null;
        this.OnInputClicked(data);
    }
}
