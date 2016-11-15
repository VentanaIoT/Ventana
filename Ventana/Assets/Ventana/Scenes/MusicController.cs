using UnityEngine;
using System.Collections;
using Vuforia;
using System;

public class MusicController : MonoBehaviour  {
    public Transform playButton;
    public Transform pauseButton;
    public bool isMusicPlaying = false;
    private string requestURL;
    private bool modelIsShowing = false;



    // Use this for initialization
    void Start() {
        playButton = gameObject.transform.Find("play");
        pauseButton = gameObject.transform.Find("pause");
        Debug.Log("In Music Controller");
        requestURL = GetComponent<RequestScript>().url;
    }

    IEnumerator WaitForRequest(WWW www) {
        yield return www;

        if ( www.error == null ) {
            //Debug.Log("THE SERVER");
            //this class exists i promise
            Debug.Log(www.text);
        } else {
            //Debug.Log("Not changing texture");
        }
    }

    // Update is called once per frame
    void Update() {
        Renderer playRenderer = playButton.GetComponent<Renderer>();
        Renderer pauseRenderer = pauseButton.GetComponent<Renderer>();
        BoxCollider playCollider = playButton.GetComponent<BoxCollider>();
        BoxCollider pauseCollider = pauseButton.GetComponent<BoxCollider>();
        if ( modelIsShowing ) {
            if ( !isMusicPlaying ) {
                playRenderer.enabled = true;
                playCollider.enabled = true;
                pauseRenderer.enabled = false;
                pauseCollider.enabled = false;
            } else {
                playRenderer.enabled = false;
                playCollider.enabled = false;
                pauseRenderer.enabled = true;
                pauseCollider.enabled = true;

            }
        }

    }
    public void OnURLSent(VentanaInteractable ventana) {
        SonosInfo info = ventana as SonosInfo;
        isMusicPlaying = !info.isPaused;
    }

    void ObjectFound() {
        modelIsShowing = true;
    }

    void ObjectLost() {
        modelIsShowing = false;
    }
    

}

