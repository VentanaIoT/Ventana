using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;
<<<<<<< HEAD

public class ddScript : MonoBehaviour, IInputClickHandler {
    public AudioClip clickSound;
    private AudioSource source;

    // Use this for initialization
    void Start () {
        source = GetComponent<AudioSource>();

=======
using System;

public class ddScript : MonoBehaviour, IInputClickHandler,IFocusable {
    public Material highlightButtonMaterial;
    public Material normalButtonMaterial;

    // Use this for initialization
    void Start () {
>>>>>>> 79f16e1a5f0c61727daf817f7502a4e1fc7d7afa
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
<<<<<<< HEAD
        source.PlayOneShot(clickSound, 1F);
=======
    }

    public void OnFocusEnter() {
        gameObject.SendMessageUpwards("DisableHandDraggable");
        gameObject.GetComponent<Renderer>().material = highlightButtonMaterial;
    }

    public void OnFocusExit() {
        gameObject.SendMessageUpwards("EnableHandDraggable");
        gameObject.GetComponent<Renderer>().material = normalButtonMaterial;
>>>>>>> 79f16e1a5f0c61727daf817f7502a4e1fc7d7afa
    }
}
