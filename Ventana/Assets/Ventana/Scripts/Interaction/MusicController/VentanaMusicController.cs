using UnityEngine;
using System.Collections;
using Vuforia;
using System;
[ExecuteInEditMode]
public class VentanaMusicController : BaseVentanaController  {
    public GameObject playButton;
    public GameObject pauseButton;
    public bool isMusicPlaying = false;
    public bool isModelShowing = false;

    public int volumeMultiplier = 2;

    private string playCommand = "playtoggle";
    private string statusCommand = "status";
    private string nextCommand = "forward";
    private string previousCommand = "reverse";


    // Use this for initialization
    void Start() {
        base.Start();
        InvokeRepeating("requestAlbum", 1.0f, 6.0f);
    }

    // Use this for initialization
    void makeAPIRequest(string child) {
        VentanaRequestFactory requestFactory = VentanaRequestFactory.Instance;

        switch ( child ) {
            case "play":
            Debug.Log("Bubbled play");
            StartCoroutine(requestFactory.GetFromMusicAPIEndpoint(playCommand, VentanaID, null));
            break;
            case "pause":
            Debug.Log("Bubbled pause");
            StartCoroutine(requestFactory.GetFromMusicAPIEndpoint(playCommand, VentanaID, null));
            break;
            case "next":
            Debug.Log("Bubbled next");
            StartCoroutine(requestFactory.GetFromMusicAPIEndpoint(nextCommand, VentanaID, null));
            break;
            case "previous":
            Debug.Log("Bubbled previous");
            StartCoroutine(requestFactory.GetFromMusicAPIEndpoint(previousCommand, VentanaID, null));
            break;
            case "status":
            //Debug.Log("Bubbled albumArt");
            StartCoroutine(requestFactory.GetFromMusicAPIEndpoint(statusCommand, this.VentanaID, GetRequestCompleted));
            break;
            default:
            Debug.Log("No good bubble called");
            break;
        }

    }
    void requestAlbum() {
        makeAPIRequest("status");
    }

    // Update is called once per frame
    void Update() { 
        if ( isModelShowing ) {
            if ( !isMusicPlaying ) {
                playButton.SetActive(true);
                pauseButton.SetActive(false);
            } else {
                playButton.SetActive(false);
                pauseButton.SetActive(true);
            }
        }
    }
    public void GetRequestCompleted(VentanaInteractable ventana) {
        SonosInfo info = ventana as SonosInfo;
        isMusicPlaying = !info.isPaused;
        gameObject.BroadcastMessage("OnURLSent", ventana);
    }

    void OnSliderChangeRequest(KnobHandler.SliderLevels levels) {
        VentanaRequestFactory requestFactory = VentanaRequestFactory.Instance;
        Debug.Log("Requesting a: " + levels.XAxisLevel + (levels.XAxisLevel > 0 ? " increase" : " decrease"));
        int baseLevel = levels.XAxisLevel * volumeMultiplier;
        StartCoroutine(requestFactory.PostToMusicAPIEndpoint("volume", VentanaID, (levels.XAxisLevel > 0 ? "+" : "") + baseLevel.ToString()));

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
