using HoloToolkit.Unity.InputModule;
using UnityEngine;
using System;

public class MusicButtonHandler : MonoBehaviour, IInputClickHandler {
    public void OnInputClicked(InputEventData eventData) {
        Debug.Log("Clicked " + gameObject.name);
        gameObject.SendMessageUpwards("makeAPIRequest", gameObject.name);
    }




    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
		
	}

}
