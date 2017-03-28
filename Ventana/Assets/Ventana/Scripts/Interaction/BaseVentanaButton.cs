using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseVentanaButton : MonoBehaviour, IInputClickHandler, IFocusable {
    public AudioClip clickSound;
    protected AudioSource source;
    public Material highlightButtonMaterial;
    public Material normalButtonMaterial;

    public void OnFocusEnter() {
        gameObject.GetComponent<Renderer>().material = highlightButtonMaterial;
    }

    public void OnFocusExit() {
        gameObject.GetComponent<Renderer>().material = normalButtonMaterial;
    }

    public void OnInputClicked(InputClickedEventData eventData) {
        Debug.Log("Clicked " + gameObject.name);
        gameObject.SendMessageUpwards("makeAPIRequest", gameObject.name);
        source.PlayOneShot(clickSound, 1F);
    }

    public void DisableInteraction(bool yes) {
        if (yes) {
            gameObject.GetComponent<Collider>().enabled = false;
        } else {
            gameObject.GetComponent<Collider>().enabled = true;
        }
    }

    // Use this for initialization
    void Start() {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update() {

    }
}

