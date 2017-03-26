using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;

public class ddScript : MonoBehaviour, IInputClickHandler {

	// Use this for initialization
	void Start () {
        Collider collider = GetComponentInChildren<Collider>();
        if (collider == null)
        {
            gameObject.AddComponent<BoxCollider>();
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnInputClicked(InputClickedEventData eventData)
    {
        Debug.Log("button pressed");
        gameObject.SendMessageUpwards("ddButtonClicked", gameObject.name);
    }
}
