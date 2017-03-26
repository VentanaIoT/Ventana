using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;
using HoloToolkit.Unity.SpatialMapping;

public class ttpMode : MonoBehaviour {

    public Transform moreButtons;
    public Transform deleteDone;
    public Transform scaleHandles;
    public bool scaleEnabled = false;
    public Vector3 lastScale;

    [Tooltip("Speed at which the object is resized.")]
    [SerializeField]
    float ResizeSpeedFactor = 1.0f;

    [SerializeField]
    float ResizeScaleFactor = 0.75f;

    [Tooltip("When warp is checked, we allow resizing of all three scale axes - otherwise we scale each axis by the same amount.")]
    [SerializeField]
    bool AllowResizeWarp = false;

    [Tooltip("Minimum resize scale allowed.")]
    [SerializeField]
    float MinScale = 0.1f;

    [Tooltip("Maximum resize scale allowed.")]
    [SerializeField]
    float MaxScale = 0.3f;

    // Use this for initialization
    void Start () {

        // initialize to regular mode, with tap to place controls inactive
        moreButtons.gameObject.SetActive(true);
        deleteDone.gameObject.SetActive(false);
        scaleHandles.gameObject.SetActive(false);

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void MoreButtonClicked()
    {
        //Debug.Log("More button clicked");
        // More button clicked so tap to place mode should be active
        moreButtons.gameObject.SetActive(false);
        deleteDone.gameObject.SetActive(true);
        scaleHandles.gameObject.SetActive(true);
    }

    void ddButtonClicked(string child)
    {
        if (child.Equals("Done Button"))
        {
           // Debug.Log("Done button clicked");
           // Done button clicked so regular mode should be active
            moreButtons.gameObject.SetActive(true);
            deleteDone.gameObject.SetActive(false);
            scaleHandles.gameObject.SetActive(false);
        }

        if (child.Equals("Delete Button"))
        {
            //Debug.Log("Delete button clicked");
            // gonna have to remove world anchor here
            TapToPlace ttp = gameObject.GetComponent<TapToPlace>();
            if (ttp != null)
            {
                // had to make anchorManager public instead of protected in ttp
                ttp.anchorManager.RemoveAnchor(gameObject);
            }
            Destroy(gameObject);
        }
    }

    void scaleStarted()
    {
        // manipulation gesture started so get the current scale
        lastScale = gameObject.GetComponent<Transform>().localScale;
    }
    void scaleButtonClicked(Vector3 newScale)
    {
        // manipulation gesture ended, calculate and set the new scale 
        /* https://www.billmccrary.com/holotoolkit-simple-dragresizerotate/ modified from HandResize.cs */

        float resizeX, resizeY, resizeZ;
        //if we are warping, honor axis delta, else take the x
        if (AllowResizeWarp)
        {
            resizeX = newScale.x * ResizeScaleFactor;
            resizeY = newScale.y * ResizeScaleFactor;
            resizeZ = newScale.z * ResizeScaleFactor;
        }
        else
        {
            resizeX = resizeY = resizeZ = newScale.x * ResizeScaleFactor;
        }

        resizeX = Mathf.Clamp(lastScale.x + resizeX, MinScale, MaxScale);
        resizeY = Mathf.Clamp(lastScale.y + resizeY, MinScale, MaxScale);
        resizeZ = Mathf.Clamp(lastScale.z + resizeZ, MinScale, MaxScale);

        Vector3 newTransform = new Vector3(resizeX, resizeY, resizeZ);

        Transform currentTransform = gameObject.GetComponent<Transform>();
        //Debug.Log("before scale" + currentTransform);
        currentTransform.localScale = Vector3.Lerp(transform.localScale, newTransform, ResizeSpeedFactor);
        //Debug.Log("scale:" + currentTransform.localScale);
    }
}
