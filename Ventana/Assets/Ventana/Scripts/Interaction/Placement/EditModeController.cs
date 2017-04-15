using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;
using HoloToolkit.Unity.SpatialMapping;
using UnityEngine.VR.WSA;
using HoloToolkit.Unity;
using System;

[ExecuteInEditMode]
public class EditModeController : MonoBehaviour {

    public Transform moreButtons;
    public Transform deleteDone;
    public Transform scaleHandles;
    public bool scaleEnabled = false;
    public bool scaleModeTriggered = false;
    public AudioClip clickSound;
    public Vector3 lastScale;
    private HandDraggable handDraggable;
    private bool isDragging = false;
    private AudioSource source;


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
    float MinScale = 0.05f;

    [Tooltip("Maximum resize scale allowed.")]
    [SerializeField]
    float MaxScale = 0.7f;

    // Use this for initialization
    void Start () {
        source = gameObject.GetComponent<AudioSource>();
        // initialize to regular mode, with tap to place controls inactive
        moreButtons.gameObject.SetActive(true);
        deleteDone.gameObject.SetActive(false);
        scaleHandles.gameObject.SetActive(false);
               
        handDraggable = gameObject.GetComponent<HandDraggable>();

        if (handDraggable != null ) {
            handDraggable.StoppedDragging += HandDraggable_StoppedDragging;
            handDraggable.StartedDragging += HandDraggable_StartedDragging;
        }

    }
    

    private void HandDraggable_StartedDragging() {
        SpatialMappingManager.Instance.DrawVisualMeshes = true;
        isDragging = true;
        Debug.Log(gameObject.name + " : Removing existing world anchor if any.");
        WorldAnchorManager.Instance.RemoveAnchor(gameObject);
    }

    private void HandDraggable_StoppedDragging() {
        SpatialMappingManager.Instance.DrawVisualMeshes = false;
        isDragging = false;
        // Add world anchor when object placement is done.
        BaseVentanaController bvc = gameObject.GetComponent<BaseVentanaController>();
        if ( bvc ) {
            string currentTime = DateTime.Now.Subtract(DateTime.MinValue.AddYears(1969)).TotalMilliseconds.ToString();
            string savedAnchorName = bvc.VentanaID + ":" + gameObject.transform.lossyScale.x.ToString()+ ":" + currentTime;
            Debug.Log("<color=yellow>Name: </color>" + savedAnchorName);

            WorldAnchorManager.Instance.AttachAnchor(gameObject, savedAnchorName);
        }
        source.PlayOneShot(clickSound, 1F);
    }



    // Update is called once per frame
    void Update () {
		if ( scaleModeTriggered ) {
            moreButtons.gameObject.SetActive(false);
            deleteDone.gameObject.SetActive(true);
            scaleHandles.gameObject.SetActive(true);
            gameObject.BroadcastMessage("DisableInteraction", true);
        } else {
            moreButtons.gameObject.SetActive(true);
            deleteDone.gameObject.SetActive(false);
            scaleHandles.gameObject.SetActive(false);
            gameObject.BroadcastMessage("DisableInteraction", false);
        }
	}

    void MoreButtonClicked()
    {
        //Debug.Log("More button clicked");
        // More button clicked so tap to place mode should be active
        moreButtons.gameObject.SetActive(false);
        deleteDone.gameObject.SetActive(true);
        scaleHandles.gameObject.SetActive(true);
        //Enable Tap To Place and Hand Dragable here.
        
        if ( handDraggable != null ) {
            handDraggable.enabled = true;
        }

        scaleModeTriggered = true;
        source.PlayOneShot(clickSound, 1F);
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
            scaleModeTriggered = false;
            
            if ( handDraggable != null ) {
                handDraggable.enabled = false;
            }

        }

        if (child.Equals("Delete Button"))
        {
            //Debug.Log("Delete button clicked");
            // gonna have to remove world anchor here
            if (handDraggable != null)
            {
                // had to make anchorManager public instead of protected in ttp
                WorldAnchor wa = gameObject.GetComponent<WorldAnchor>();
                if ( wa ) {
                    WorldAnchorManager.Instance.AnchorStore.Delete(wa.name);
                }
               
            }
            scaleModeTriggered = false;
            Destroy(gameObject);
        }

        source.PlayOneShot(clickSound, 1F);
    }

    void scaleStarted()
    {
        // manipulation gesture started so get the current scale
        //turn off draggable behaviours
        
    }

    void scaleEnded() {
        BaseVentanaController bvc = gameObject.GetComponent<BaseVentanaController>();
        if ( bvc ) {
            Debug.Log(gameObject.name + " : Removing existing world anchor if any after scaling");
            WorldAnchorManager.Instance.RemoveAnchor(gameObject);
            string currentTime = DateTime.Now.Subtract(DateTime.MinValue.AddYears(1969)).TotalMilliseconds.ToString();
            string savedAnchorName = bvc.VentanaID + ":" + gameObject.transform.lossyScale.x.ToString() + ":" + currentTime;
            Debug.Log("<color=yellow>Name: </color>" + savedAnchorName);

            WorldAnchorManager.Instance.AttachAnchor(gameObject, savedAnchorName);
        }
    }

    void EnableHandDraggable() {
        if ( handDraggable ) { handDraggable.enabled = true; }
    }

    void DisableHandDraggable() {
        if ( handDraggable ) { handDraggable.enabled = false; }
    }
    
    void scaleButtonClicked(Vector3 newScale)
    {
        // manipulation gesture ended, calculate and set the new scale 
        /* https://www.billmccrary.com/holotoolkit-simple-dragresizerotate/ modified from HandResize.cs */
        if ( !isDragging ) {
            float resizeX, resizeY, resizeZ;
            //if we are warping, honor axis delta, else take the x
            if ( AllowResizeWarp ) {
                resizeX = newScale.x * ResizeScaleFactor;
                resizeY = newScale.y * ResizeScaleFactor;
                resizeZ = newScale.z * ResizeScaleFactor;
            } else {
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
}
