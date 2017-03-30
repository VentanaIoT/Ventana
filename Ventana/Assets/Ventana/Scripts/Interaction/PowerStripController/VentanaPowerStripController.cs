using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VentanaPowerStripController : BaseVentanaController {

    private string poweredCommand = "change_power";
    private string statusCommand = "status";
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
            case "Toggle0":
            Debug.Log("Toggled 0");
            StartCoroutine(requestFactory.PostToLightAPIEndpoint(poweredCommand, VentanaID, "0"));
            break;
            case "Toggle1":
            Debug.Log("Toggled 1");
            StartCoroutine(requestFactory.PostToLightAPIEndpoint(poweredCommand, VentanaID, "1"));
            break;
            default:
            break;
        }
    }
}
