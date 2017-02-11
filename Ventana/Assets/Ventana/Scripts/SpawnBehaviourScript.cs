using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;
using System;

public class SpawnBehaviourScript : MonoBehaviour, IInputClickHandler {
    #region PUBLIC_MEMBERS
    public GameObject prefabObject;

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
        if (spawnCount == 0)
        {
            GameObject prefabObjectClone = (GameObject)Instantiate(prefabObject, transform.position, transform.rotation);
            prefabObject.AddComponent<SpawnBehaviourScript>();
            spawnCount = 1;
        }
    }

    public void OnInputClicked(InputEventData eventData)
    {
        Debug.Log("<color=yellow>EY BAY BAY</color>");
        mTapCount++;
        HandleTap();


    }

    #endregion // PROTECTED_METHODS
}
