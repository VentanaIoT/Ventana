using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;
using System;
using HoloToolkit.Unity.SpatialMapping;

public class SpawnBehaviourScript : MonoBehaviour, IHoldHandler {
    #region PUBLIC_MEMBERS
    public GameObject prefabObject;
    public bool shouldSpawn = false;
    public Vector3 scaleMultiplier;
    public Vector3 placementPosition;
    public int ControllerID;
    public string prefabName;

    #endregion //PUBLIC_MEMBERS

    #region PRIVATE_MEMBERS

    private const float DOUBLE_TAP_MAX_DELAY = 0.5f;
    //seconds
    private float mTimeSinceLastTap = 0;

    #endregion //PRIVATE_MEMBERS


    #region PROTECTED_MEMBERS

    protected int mTapCount = 0;
    protected int spawnCount = 0;

    #endregion //PROTECTED_MEMBERS


    #region MONOBEHAVIOUR_METHODS

    void Start()
    {
        mTapCount = 0;
        mTimeSinceLastTap = 0;
    }

    void Update()
    {
        //Debug.Log("sb update reached");
    }

    #endregion //MONOBEHAVIOUR_METHODS


    #region PRIVATE_METHODS

    private void HandleTap()
    {
        if (mTapCount == 1)
        {
            mTimeSinceLastTap += Time.deltaTime;
            if (mTimeSinceLastTap > DOUBLE_TAP_MAX_DELAY)
            {
                // too late for double tap, 
                // we confirm it was a single tap
                OnSingleTapConfirmed();

                // reset touch count and timer
                mTapCount = 0;
                mTimeSinceLastTap = 0;
            }
        }
        else if (mTapCount == 2)
        {
            // we got a double tap
            OnDoubleTap();

            // reset touch count and timer
            mTimeSinceLastTap = 0;
            mTapCount = 0;
        }

        
    }

    #endregion // PRIVATE_METHODS


    #region PROTECTED_METHODS

    /// <summary>
    /// This method can be overridden by custom (derived) TapHandler implementations,
    /// to perform special actions upon single tap.
    /// </summary>
    protected virtual void OnSingleTap()
    {
        Debug.Log("sb OST reached");
    }

    protected virtual void OnSingleTapConfirmed()
    {
        Debug.Log("sb OSTC reached");
    }

    protected virtual void OnDoubleTap()
    {
        Debug.Log("sb ODT reached");
        if ( shouldSpawn )
        {
            //Copying Controller...
            GameObject prefabObjectClone = GameObject.Instantiate(gameObject);
            Vector3 cam = Camera.main.transform.forward.normalized;
            Vector3 current = gameObject.transform.position;
            prefabObjectClone.transform.position = new Vector3(current.x + (cam.x * .05f), current.y + (cam.y * .05f), current.z + (cam.z * .05f));
            Vector3 globalScale = gameObject.transform.lossyScale;
            prefabObjectClone.transform.localScale = new Vector3(globalScale.x * 1.35f, globalScale.y * 1.35f, globalScale.z * 1.35f);
            prefabObjectClone.transform.rotation = gameObject.transform.rotation;
            EditModeController edit = prefabObjectClone.GetComponent<EditModeController>();
            edit.scaleModeTriggered = true;

            Destroy(prefabObjectClone.GetComponent<SpawnBehaviourScript>());

            HandDraggable hd = prefabObjectClone.AddComponent<HandDraggable>();
            hd.enabled = true;
            hd.RotationMode = HandDraggable.RotationModeEnum.OrientTowardUserAndKeepUpright;
            hd.IsDraggingEnabled = true;

        }
    }

    /*
    public void OnInputClicked(InputClickedEventData eventData)
    {
        Debug.Log("<color=yellow>EY BAY BAY</color>");
        mTapCount++;
        HandleTap();


    }
    */
    // replacing double tap to spawn with click and hold
    public void OnHoldStarted(HoldEventData eventData)
    {

    }

    public void OnHoldCompleted(HoldEventData eventData)
    {
        OnDoubleTap();
    }

    public void OnHoldCanceled(HoldEventData eventData)
    {

    }
    #endregion // PROTECTED_METHODS
}
