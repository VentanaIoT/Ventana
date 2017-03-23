/*===============================================================================
Copyright (c) 2016 PTC Inc. All Rights Reserved.

Confidential and Proprietary - Protected under copyright and other laws.
Vuforia is a trademark of PTC Inc., registered in the United States and other 
countries.
===============================================================================*/

using UnityEngine;
using Vuforia;
using System;
using HoloToolkit.Unity.InputModule;

/// <summary>
/// A custom handler that implements the ITrackableEventHandler interface.
/// </summary>
public class VuMarkEventHandler : MonoBehaviour, ITrackableEventHandler {
    #region PRIVATE_MEMBER_VARIABLES

    private VuMarkBehaviour mTrackableBehaviour; //this is exclusively for vumarks 
    private GameObject control; //the control this vumark currently has

    #endregion // PRIVATE_MEMBER_VARIABLES
    #region UNTIY_MONOBEHAVIOUR_METHODS

    void Start() {
        mTrackableBehaviour = GetComponent<VuMarkBehaviour>();
        if ( mTrackableBehaviour ) {
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
        }
    }

    #endregion // UNTIY_MONOBEHAVIOUR_METHODS

    #region PUBLIC_METHODS

    /// <summary>
    /// Implementation of the ITrackableEventHandler function called when the
    /// tracking state changes.
    /// </summary>
    public void OnTrackableStateChanged(
                                    TrackableBehaviour.Status previousStatus,
                                    TrackableBehaviour.Status newStatus) {
        if ( newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED ) {
            OnTrackingFound();
        } else {
            OnTrackingLost();
        }
    }

    #endregion // PUBLIC_METHODS


    #region PRIVATE_METHODS

    private void OnTrackingFound() {
        
        Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
        Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);
        

        // Enable rendering:
        foreach ( Renderer component in rendererComponents ) {
            component.enabled = true;
        }

        // Enable colliders:
        foreach ( Collider component in colliderComponents ) {
            component.enabled = true;
        }
        

        Debug.Log("VuMark of Type: " + GetVuMarkType( mTrackableBehaviour.VuMarkTarget) + " and Value: " + GetVuMarkString(mTrackableBehaviour.VuMarkTarget) + " found");

        ModelController mc = ModelController.Instance;
        int vuMarkId = Convert.ToInt32(GetVuMarkString(mTrackableBehaviour.VuMarkTarget), 16);
        control = null;
        control = mc.GetPrefabWithId(vuMarkId); 
        if ( control ) {
            mTrackableBehaviour.transform.DestroyChildren();
            BaseVentanaController bvc = control.GetComponent<BaseVentanaController>();
            if ( bvc ) {
                bvc.OnVumarkFound();
                bvc.VentanaID = vuMarkId;
                
            }
            control.transform.SetParent(mTrackableBehaviour.gameObject.transform);
            control.transform.position = gameObject.transform.position;
            

            control.layer = 9;

            control.transform.localScale = new Vector3(.35f, .35f, .35f);
            SpawnBehaviourScript spb = control.gameObject.AddComponent<SpawnBehaviourScript>();
            spb.ControllerID = vuMarkId;
            spb.shouldSpawn = true;
            spb.scaleMultiplier = gameObject.transform.localScale;
        }

    }

   


    private void OnTrackingLost() {
        Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
        Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);

        // Disable rendering:
        foreach ( Renderer component in rendererComponents ) {
            component.enabled = false;
        }

        // Disable colliders:
        foreach ( Collider component in colliderComponents ) {
            component.enabled = false;
        }

        Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost");

        //delete all children
        if ( control ) {
            BaseVentanaController bvc = control.GetComponent<BaseVentanaController>();
            if ( bvc ) {
                bvc.OnVumarkLost();
            }
        }
        
        mTrackableBehaviour.transform.DestroyChildren();
    }

    private string GetVuMarkType(VuMarkTarget vumark) {
        switch ( vumark.InstanceId.DataType ) {
            case InstanceIdType.BYTES:
            return "Bytes";
            case InstanceIdType.STRING:
            return "String";
            case InstanceIdType.NUMERIC:
            return "Numeric";
        }
        return "";
    }

    private string GetVuMarkString(VuMarkTarget vumark) {
        switch ( vumark.InstanceId.DataType ) {
            case InstanceIdType.BYTES:
            return vumark.InstanceId.HexStringValue;
            case InstanceIdType.STRING:
            return vumark.InstanceId.StringValue;
            case InstanceIdType.NUMERIC:
            return vumark.InstanceId.NumericValue.ToString();
        }
        return "";
    }

    #endregion // PRIVATE_METHODS

   
}
