using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;
using System;
using Vuforia;
using HoloToolkit.Unity.SpatialMapping;

public class VentanaImageTargetController : MonoBehaviour {
    private void Start() {
        VuforiaARController vb = VuforiaARController.Instance;
        vb.RegisterVuforiaStartedCallback(LoadDataSet);
    }

    public void LoadDataSet() {
        //code to change image target 
        ObjectTracker objectTracker = TrackerManager.Instance.GetTracker<ObjectTracker>();
        string dataSetName = Args.VENTANA_DATA_SET_NAME;
        DataSet dataSet = objectTracker.CreateDataSet();

        if ( dataSet.Load(dataSetName, VuforiaUnity.StorageType.STORAGE_APPRESOURCE) ) {

            objectTracker.Stop();  // stop tracker so that we can add new dataset

            if ( !objectTracker.ActivateDataSet(dataSet) ) {
                // Note: ImageTracker cannot have more than 1000 total targets activated
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
                    tb.gameObject.AddComponent<VentanaSpeakerEventHandler>();
                    tb.gameObject.AddComponent<TurnOffBehaviour>();
                    SpawnBehaviourScript spb = tb.gameObject.AddComponent<SpawnBehaviourScript>();

                    GameObject control = mc.GetPrefabWithId(tb.TrackableName);
                    spb.prefabObject = control;

                    if ( control ) {
                        control.transform.SetParent(tb.gameObject.transform);
                        control.transform.localPosition = new Vector3(0f, 0f, 0.9f);
                        //control.transform.localRotation = Quaternion.identity * Quaternion.Euler(0, 180, 0);
                       
                        control.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
                    }
                }
            }
        } else {
            Debug.LogError("<color=yellow>Failed to load dataset: '" + dataSetName + "'</color>");
        }
    }

}
