using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;
using System;

[ExecuteInEditMode]
public class KnobHandler : VentanaHandDraggable, IFocusable {
    //only allow y displacment up to a quarter of the rail's width in either direction... 
    //then just return to the position it was at. 
    [Tooltip("Where in the local Coordinate system this object will return to")]
    private Vector3 baseLocation; //location to return to.
    private Quaternion baseRotation;
    public bool allowX, allowY, allowZ;
    public GameObject containerObject;
    public Vector3 bounds;

    private float nextActionTime = 1.0f;
    public float period = 1.0f;
    private bool shouldExecute = false;

    public AudioClip clickSound;
    private AudioSource source;
    public Material highlightButtonMaterial;
    public Material normalButtonMaterial;

    override public void Start() {
        base.Start();
        gameObject.SetActive(false);
        baseLocation = gameObject.transform.localPosition;
        baseRotation = gameObject.transform.localRotation;
        gameObject.transform.localPosition = new Vector3(0, 0, 0);
        gameObject.transform.localRotation = Quaternion.identity;
        source = GetComponent<AudioSource>();
        bounds = containerObject.GetComponent<MeshFilter>().sharedMesh.bounds.extents;
        var scale = containerObject.transform.localScale;
        
        gameObject.transform.localRotation = baseRotation;
        gameObject.transform.localPosition = baseLocation;
        gameObject.SetActive(true);


    }
    
    public void OnFocusEnter() {
        base.OnFocusEnter();
        gameObject.GetComponent<Renderer>().material = highlightButtonMaterial;
    }

    public void OnFocusExit() {
        base.OnFocusExit();
        gameObject.GetComponent<Renderer>().material = normalButtonMaterial;
    }

    public override void Update() {
        base.Update();
        
        //Debug.Log(gameObject.transform.parent.localScale);
        //Debug.Log(gameObject.transform.localPosition.x + gameObject.transform.parent.localPosition.x);
        //we know we want to keep the x and y position at a certain place, only want the y offset. so lets constantly keep putting this thing there
        if ( isDragging ) {
            {
                var position = gameObject.transform.localPosition;

                Vector3 origin = baseLocation;
                
                //gives x y z values for the size. we want to go .5 times the axis of freedom max.
                float[] allowedThreshold = new float[3];

                allowedThreshold[0] = bounds.x;
                allowedThreshold[1] = bounds.y;
                allowedThreshold[2] = bounds.z;
                
                if ( !allowX ) {
                    //change x to be the origin 
                    position.x = origin.x;

                } else { //allowX
                    if ( position.x > allowedThreshold[0] + origin.x ) { //positive offset
                        //just set the position of x to be the max....
                        position.x = allowedThreshold[0];

                    } else if ( position.x < origin.x - allowedThreshold[0] ) {
                        position.x = -allowedThreshold[0];
                    }
                }

                if ( !allowY ) {
                    //change y to be the origin
                    position.y = origin.y;
                } else { //allowY
                    if ( position.y > allowedThreshold[1] + origin.y ) { //positive offset
                        //just set the position of x to be the max....
                        position.y = allowedThreshold[1];

                    } else if ( position.y < origin.y - allowedThreshold[1] ) {
                        position.y = -allowedThreshold[1];
                    }
                }

                if ( !allowZ ) {
                    position.z = origin.z;
                } else { //allowZ
                    if ( position.z > allowedThreshold[2] + origin.z ) { //positive offset
                        //just set the position of x to be the max....
                        position.z = allowedThreshold[2];

                    } else if ( position.z < origin.z - allowedThreshold[2] ) {
                        position.z = -allowedThreshold[2];
                    }
                }

                gameObject.transform.localPosition = position;
                gameObject.transform.localRotation = baseRotation;

                /*
                Debug.Log("POSITION: " + position.x + " " + position.y + " " + position.z);
                Debug.Log("BOUNDS: " + bounds.x + " " + bounds.y + " " + bounds.z);
                */


                //do we want to do this right here? i guess start a CoRoutine to tell sonos to turn the fuck up...
                //only do it in the x direction cause it seems to not work on others....
                //only do this calculation about each second...

                // If the next update is reached

                if ( Time.time >= nextActionTime ) {
                    //Debug.Log(Time.time + ">=" + nextActionTime);
                    // Change the next update (current second+1)
                    nextActionTime = Mathf.FloorToInt(Time.time) + period;
                    // Call your fonction
                    if ( shouldExecute ) { // this is where I perform calculations
                        performLevelCalculations();
                    } else {
                        shouldExecute = true;
                    }
                }
            }



    } else {
            //stuff to do when not dragging...
            baseLocation = gameObject.transform.localPosition;
            baseRotation = gameObject.transform.localRotation;
        }
    }


    override public void StopDragging() {
        base.StopDragging();
        gameObject.transform.localRotation = baseRotation;
        gameObject.transform.localPosition = baseLocation;
        shouldExecute = false;


        source.PlayOneShot(clickSound, 1F);


    }

    public override void StartDragging() {
        base.StartDragging();
        //wait X seconds before you start doing any calcs;
        shouldExecute = false;
        baseLocation = gameObject.transform.localPosition;
        baseRotation = gameObject.transform.localRotation;

        source.PlayOneShot(clickSound, 1F);

    }

    public void performLevelCalculations() {
        Vector3 origin = baseLocation;
        Vector3 currentLocation = gameObject.transform.localPosition;
        SliderLevels sliders = new SliderLevels();
        //gives x y z values for the size. we want to go .5 times the axis of freedom max.
        float[] allowedThreshold = new float[3];
        allowedThreshold[0] = bounds.x;
        allowedThreshold[1] = bounds.y;
        allowedThreshold[2] = bounds.z;

        if ( allowX ) {
            //calculate what % of the allowed direction im at.
            //0-30% +1pt 31-60% +2pts 61-100% +3pts if to the right
            //0-30% -1pt 31-60% -2pts 61-100% -3pts if to the left 

            if (currentLocation.x < origin.x ) { //left side of origin
                var delta = Mathf.Abs(currentLocation.x - origin.x);
                if ( delta > (0.61f * allowedThreshold[0]) ) {
                    Debug.Log("LEVEL 3 DECREASE");
                    sliders.XAxisLevel = -3;
                } else if ( delta > (0.31f * allowedThreshold[0]) && delta < (0.60f * allowedThreshold[0]) ) {
                    Debug.Log("LEVEL 2 DECREASE");
                    sliders.XAxisLevel = -2;
                } else if (delta > (0.01f * allowedThreshold[0]) && delta < (0.30f * allowedThreshold[0])) {
                    Debug.Log("LEVEL 1 DECREASE");
                    sliders.XAxisLevel = -1;
                } else {
                    //do nothing weird numbers....
                    sliders.XAxisLevel = 0;
                }

            } else if (currentLocation.x >= origin.x  ) { //right side of origin
                var delta = Mathf.Abs(currentLocation.x - origin.x);
                if ( delta > (0.61f * allowedThreshold[0]) ) {
                    Debug.Log("LEVEL 3 INCREASE");
                    sliders.XAxisLevel = 3;
                } else if ( delta > (0.31f * allowedThreshold[0]) && delta < (0.60f * allowedThreshold[0]) ) {
                    Debug.Log("LEVEL 2 INCREASE");
                    sliders.XAxisLevel = 2;
                } else if ( delta > (0.01f * allowedThreshold[0]) && delta < (0.30f * allowedThreshold[0]) ) {
                    Debug.Log("LEVEL 1 INCREASE");
                    sliders.XAxisLevel = 1;
                } else {
                    //do nothing weird numbers....
                    sliders.XAxisLevel = 0;
                }
            }
        }

        if ( allowY ) {
            if ( currentLocation.y < origin.y ) { //left side of origin
                var delta = Mathf.Abs(currentLocation.y - origin.y);
                if ( delta > (0.61f * allowedThreshold[1]) ) {
                    Debug.Log("LEVEL 3 DECREASE");
                    sliders.YAxisLevel = -3;
                } else if ( delta > (0.31f * allowedThreshold[1]) && delta < (0.60f * allowedThreshold[1]) ) {
                    Debug.Log("LEVEL 2 DECREASE");
                    sliders.YAxisLevel = -2;
                } else if ( delta > (0.01f * allowedThreshold[1]) && delta < (0.30f * allowedThreshold[1]) ) {
                    Debug.Log("LEVEL 1 DECREASE");
                    sliders.YAxisLevel = -1;
                } else {
                    //do nothing weird numbers....
                    sliders.YAxisLevel = 0;
                }
            } else if ( currentLocation.y >= origin.y ) { //right side of origin
                var delta = Mathf.Abs(currentLocation.y - origin.y);
                if ( delta > (0.61f * allowedThreshold[1]) ) {
                    Debug.Log("LEVEL 3 INCREASE");
                    sliders.YAxisLevel = 3;
                } else if ( delta > (0.31f * allowedThreshold[1]) && delta < (0.60f * allowedThreshold[1]) ) {
                    Debug.Log("LEVEL 2 INCREASE");
                    sliders.YAxisLevel = 2;
                } else if ( delta > (0.01f * allowedThreshold[1]) && delta < (0.30f * allowedThreshold[1]) ) {
                    Debug.Log("LEVEL 1 INCREASE");
                    sliders.YAxisLevel = 1;
                } else {
                    //do nothing weird numbers....
                    sliders.YAxisLevel = 0;
                }
            }

        } else {
            //...
        }
        
        if ( allowZ ) {
            if ( currentLocation.z < origin.z ) { //left side of origin
                var delta = Mathf.Abs(currentLocation.z - origin.z);
                if ( delta > (0.61f * allowedThreshold[2]) ) {
                    Debug.Log("LEVEL 3 DECREASE");
                    sliders.ZAxisLevel = -3; 
                } else if ( delta > (0.31f * allowedThreshold[2]) && delta < (0.60f * allowedThreshold[2]) ) {
                    Debug.Log("LEVEL 2 DECREASE");
                    sliders.ZAxisLevel = -2;                                                    
                } else if ( delta > (0.01f * allowedThreshold[2]) && delta < (0.30f * allowedThreshold[2]) ) {
                    Debug.Log("LEVEL 1 DECREASE");
                    sliders.ZAxisLevel = -1;
                } else {
                    //do nothing weird numbers....
                    sliders.ZAxisLevel = 0;
                }
            } else if ( currentLocation.z >= origin.z ) { //right side of origin
                var delta = Mathf.Abs(currentLocation.z - origin.z);
                if ( delta > (0.61f * allowedThreshold[2]) ) {
                    Debug.Log("LEVEL 3 INCREASE");
                    sliders.ZAxisLevel = 3;
                } else if ( delta > (0.31f * allowedThreshold[2]) && delta < (0.60f * allowedThreshold[2]) ) {
                    Debug.Log("LEVEL 2 INCREASE");
                    sliders.ZAxisLevel = 2;                                                   
                } else if ( delta > (0.01f * allowedThreshold[2]) && delta < (0.30f * allowedThreshold[2]) ) {
                    Debug.Log("LEVEL 1 INCREASE");
                    sliders.ZAxisLevel = 1;
                } else {
                    //do nothing weird numbers....
                    sliders.ZAxisLevel = 0;
                }
            }

        } else {

        }

        if (sliders.XAxisLevel != 0 || sliders.YAxisLevel != 0 || sliders.ZAxisLevel != 0) {
            HandleSliderChangeRequest(sliders);
        }
    }

    protected void HandleSliderChangeRequest(SliderLevels levels) {
        //pls change state based on slider levels
        //for this one i'm just going to send a message to the root object...
        gameObject.SendMessageUpwards("OnSliderChangeRequest", levels);
    }

    public void DisableInteraction(bool yes) {
        if ( yes ) {
            gameObject.GetComponent<Collider>().enabled = false;
        } else {
            gameObject.GetComponent<Collider>().enabled = true;
        }
    }

    public struct SliderLevels {
        public int XAxisLevel, YAxisLevel, ZAxisLevel;
    }
}
