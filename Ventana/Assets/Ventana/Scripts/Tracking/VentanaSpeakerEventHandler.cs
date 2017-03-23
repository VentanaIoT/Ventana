using System.Collections;
using Vuforia;
using System.Collections.Generic;
using UnityEngine;

public class VentanaSpeakerEventHandler : DefaultTrackableEventHandler {
    override public void OnTrackingFound() {
        VentanaMusicController musicController = GetComponentInChildren<VentanaMusicController>();
        if ( musicController ) {
            musicController.isModelShowing = true;
        }
        base.OnTrackingFound();
    }

    public override void OnTrackingLost() {
        VentanaMusicController musicController = GetComponentInChildren<VentanaMusicController>();
        if ( musicController ) {
            musicController.isModelShowing = false;
        }
        base.OnTrackingLost();
    }
}
