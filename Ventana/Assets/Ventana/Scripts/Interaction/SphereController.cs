using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;
using System;
using Vuforia;
public class SphereController : MonoBehaviour, IInputClickHandler, IFocusable  {
    public GameObject primeImgTarget;
    public string dataSetName = "QCAR\\VentanaTargets.xml";
    public void OnFocusEnter() {
        Debug.Log("Sphere Focused");
    }

    public void OnFocusExit() {
        Debug.Log("Sphere Unfocused");
    }

    public void OnInputClicked(InputEventData eventData) {
        //code to change image target 
        ObjectTracker objectTracker = TrackerManager.Instance.GetTracker<ObjectTracker>();

        DataSet dataSet = objectTracker.CreateDataSet();

        if ( dataSet.Load(dataSetName, VuforiaUnity.StorageType.STORAGE_APPRESOURCE) ) {

            objectTracker.Stop();  // stop tracker so that we can add new dataset

            if ( !objectTracker.ActivateDataSet(dataSet) ) {
                // Note: ImageTracker cannot have more than 100 total targets activated
                Debug.Log("<color=yellow>Failed to Activate DataSet: " + dataSetName + "</color>");
            }

            if ( !objectTracker.Start() ) {
                Debug.Log("<color=yellow>Tracker Failed to Start.</color>");
            }

            int counter = 0;

            IEnumerable<TrackableBehaviour> tbs = TrackerManager.Instance.GetStateManager().GetTrackableBehaviours();
            ModelController mc = ModelController.Instance;
            foreach ( TrackableBehaviour tb in tbs ) {
                if ( tb.name == "New Game Object" ) {

                    // change generic name to include trackable name
                    tb.gameObject.name = ++counter + tb.TrackableName;
                   
                    // add additional script components for trackable
                    tb.gameObject.AddComponent<DefaultTrackableEventHandler>();
                    tb.gameObject.AddComponent<TurnOffBehaviour>();
                    GameObject control = mc.GetPrefabWithId(tb.TrackableName);
                    if ( control ) {
                        control.transform.SetParent(tb.gameObject.transform);
                    }
                }
            }
        } else {
            Debug.LogError("<color=yellow>Failed to load dataset: '" + dataSetName + "'</color>");
        }
    }

}
