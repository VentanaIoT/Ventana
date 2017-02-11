/*===============================================================================
Copyright (c) 2016 PTC Inc. All Rights Reserved.

Confidential and Proprietary - Protected under copyright and other laws.
Vuforia is a trademark of PTC Inc., registered in the United States and other 
countries.
===============================================================================*/

using UnityEngine;
using Vuforia;
using System;

/// <summary>
/// A custom handler that implements the ITrackableEventHandler interface.
/// </summary>
public class VuMarkEventHandler : MonoBehaviour, ITrackableEventHandler {
    #region PRIVATE_MEMBER_VARIABLES

    private VuMarkBehaviour mTrackableBehaviour; //this is exclusively for vumarks 

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
        GameObject control = null;
        control = mc.GetPrefabWithId(Convert.ToInt32(GetVuMarkString(mTrackableBehaviour.VuMarkTarget), 16)); 
        if ( control ) {
            mTrackableBehaviour.transform.DestroyChildren();
            control.transform.SetParent(mTrackableBehaviour.gameObject.transform);
            control.transform.localPosition = new Vector3(0f, 0f, 0f);
            control.transform.localRotation = Quaternion.identity * Quaternion.Euler(0, 180, 0);
            control.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
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
