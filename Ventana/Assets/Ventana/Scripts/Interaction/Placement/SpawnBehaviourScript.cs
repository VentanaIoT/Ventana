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
            Debug.Log("Creating new control copy");
            GameObject prefabObjectClone = GameObject.Instantiate(gameObject);
            prefabObjectClone.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
            prefabObjectClone.transform.localScale = new Vector3( scaleMultiplier.x, scaleMultiplier.y, scaleMultiplier.y);
            prefabObjectClone.transform.rotation = gameObject.transform.rotation;
            TapToPlace ttp = prefabObjectClone.AddComponent<TapToPlace>();
            //Debug.Log(DateTime.Now.Subtract(DateTime.MinValue.AddYears(1969)).TotalMilliseconds.ToString());
            //ttp.SavedAnchorFriendlyName = DateTime.Now.Subtract(DateTime.MinValue.AddYears(1969)).TotalMilliseconds.ToString();
            ttp.layerMask = SpatialMappingManager.Instance.LayerMask;
            prefabName = prefabObjectClone.ToString();
            char[] delimiterChars = { ' ', '(' };
            string[] prefabCloneName = prefabName.Split(delimiterChars);
            Debug.Log("<color=yellow>Name: </color>" + prefabCloneName[0]);
            //ttp.SavedAnchorFriendlyName = DateTime.Now.Subtract(DateTime.MinValue.AddYears(1969)).TotalMilliseconds.ToString();
            // Saved Anchor Friendly Name should save all the properties of asset (besides location) each delimited by a space
            ttp.SavedAnchorFriendlyName = prefabCloneName[0] + ' ' + DateTime.Now.Subtract(DateTime.MinValue.AddYears(1969)).TotalMilliseconds.ToString();
            Debug.Log("<color=yellow>Anchor Name: </color>" + ttp.SavedAnchorFriendlyName);
            /*
            string[] ugh = ttp.SavedAnchorFriendlyName.Split(delimiterChars);
            if (ugh[0].Equals("MusicController"))
                Debug.Log("<color=yellow>U=MC</color>");
            else
                Debug.Log("color=red>NOPE</color");
                */
            var spb =  prefabObjectClone.GetComponent<SpawnBehaviourScript>();
            spb.shouldSpawn = false;
            shouldSpawn = false;
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
