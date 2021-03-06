﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VentanaLightController : BaseVentanaController {

    private string poweredCommand = "change_power";
    private string brightCommand = "change_brightness";
    private string colorCommand = "color";
    private string statusCommand = "status";

    public int brightnessMultipler = 2;
    // Use this for initialization
    protected new void Start() {
        base.Start();
    }

    // Update is called once per frame
    protected new void Update() {
        base.Update();
    }

    public override void OnVumarkFound() {
        base.OnVumarkFound();
    }

    public override void OnVumarkLost() {
        base.OnVumarkLost();
    }
    void makeAPIRequest(string child) {
        VentanaRequestFactory requestFactory = VentanaRequestFactory.Instance;
        switch ( child ) {
            case "light":
            StartCoroutine(requestFactory.PostToLightAPIEndpoint(poweredCommand, VentanaID, ""));
            break;
            default:
            break;
        }
    }

    void OnSliderChangeRequest(KnobHandler.SliderLevels levels) {
        VentanaRequestFactory requestFactory = VentanaRequestFactory.Instance;
        Debug.Log("Requesting a: " + levels.XAxisLevel + (levels.XAxisLevel > 0 ? " increase" : " decrease"));
        int baseLevel = levels.XAxisLevel * brightnessMultipler;
        StartCoroutine(requestFactory.PostToLightAPIEndpoint(brightCommand, VentanaID, baseLevel.ToString()));

    }
}
