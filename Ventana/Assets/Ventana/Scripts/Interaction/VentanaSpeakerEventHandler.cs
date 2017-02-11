using System.Collections;
using Vuforia;
using System.Collections.Generic;
using UnityEngine;

public class VentanaSpeakerEventHandler : DefaultTrackableEventHandler {
    override public void OnTrackingFound() {
        MusicController musicController = GetComponentInChildren<MusicController>();
        if ( musicController ) {
            musicController.isModelShowing = true;
        }
        base.OnTrackingFound();
    }

    public override void OnTrackingLost() {
        MusicController musicController = GetComponentInChildren<MusicController>();
        if ( musicController ) {
            musicController.isModelShowing = false;
        }
        base.OnTrackingLost();
    }
}
