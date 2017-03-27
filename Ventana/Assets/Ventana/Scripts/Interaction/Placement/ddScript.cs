using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;

public class ddScript : MonoBehaviour, IInputClickHandler {
    public AudioClip clickSound;
    private AudioSource source;

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
}
