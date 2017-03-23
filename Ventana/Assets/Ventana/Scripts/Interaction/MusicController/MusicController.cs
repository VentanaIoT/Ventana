using UnityEngine;
using System.Collections;
using Vuforia;
using System;
[ExecuteInEditMode]
public class MusicController : BaseVentanaController  {
    public Transform playButton;
    public Transform pauseButton;
    public bool isMusicPlaying = false;
    private string requestURL = "";
    public bool isModelShowing = false;



    // Use this for initialization
    public virtual void Start() {
        requestURL = GetComponent<RequestScript>().url;
    }

    IEnumerator WaitForRequest(WWW www) {
        yield return www;

        if ( www.error == null ) {
            //Debug.Log("THE SERVER");
            //this class exists i promise
            //Debug.Log(www.text);
        } else {
            //Debug.Log("Not changing texture");
        }
    }

    // Update is called once per frame
    void Update() {

        Renderer playRenderer = playButton.GetComponent<Renderer>();
        Renderer pauseRenderer = pauseButton.GetComponent<Renderer>();
        MeshCollider playCollider = playButton.GetComponent<MeshCollider>();
        MeshCollider pauseCollider = pauseButton.GetComponent<MeshCollider>();
        if ( isModelShowing ) {
            if ( !isMusicPlaying ) {

                if ( playRenderer ) {
                    playRenderer.enabled = true;
                }
                if ( playCollider ) {
                    playCollider.enabled = true;
                }
                if ( pauseRenderer ) {
                    pauseRenderer.enabled = false;
                }
                if ( pauseCollider ) {
                    pauseCollider.enabled = false;
                }
            } else {
                if ( playRenderer ) {
                    playRenderer.enabled = false;
                }
                if ( playCollider ) {
                    playCollider.enabled = false;
                }

                if ( pauseRenderer ) {
                    pauseRenderer.enabled = true;
                }
                if ( pauseCollider ) {
                    pauseCollider.enabled = true;
                }
            }
        }
    }
    public void OnURLSent(VentanaInteractable ventana) {
        SonosInfo info = ventana as SonosInfo;
        isMusicPlaying = !info.isPaused;
    }

    public override void OnVumarkFound() {
        base.OnVumarkFound();
        isModelShowing = true;
    }

    public override void OnVumarkLost() {
        base.OnVumarkLost();
        isModelShowing = false;
    }

}
