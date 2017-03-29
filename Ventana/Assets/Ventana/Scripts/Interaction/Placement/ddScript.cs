using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;

public class ddScript : MonoBehaviour, IInputClickHandler, IFocusable {
    public AudioClip clickSound;
    private AudioSource source;
    public Material highlightButtonMaterial;
    public Material normalButtonMaterial;

    // Use this for initialization
    void Start () {
        source = GetComponent<AudioSource>();
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
        source.PlayOneShot(clickSound, 1F);
    }

    public void OnFocusEnter() {
        gameObject.SendMessageUpwards("DisableHandDraggable");
        gameObject.GetComponent<Renderer>().material = highlightButtonMaterial;
    }

    public void OnFocusExit() {
        gameObject.SendMessageUpwards("EnableHandDraggable");
        gameObject.GetComponent<Renderer>().material = normalButtonMaterial;
    }
}
