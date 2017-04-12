using System;
using HoloToolkit.Unity.InputModule;
using UnityEngine;

public class scalingHandler : MonoBehaviour, IManipulationHandler, IFocusable
{
    public Material highlightButtonMaterial;
    public Material normalButtonMaterial;
    public Transform parentObject;
    public bool leftToRight;
    [Tooltip("Speed at which the object is resized.")]
    [SerializeField]
    float ResizeSpeedFactor = 1.0f;

    [SerializeField]
    float ResizeScaleFactor = 0.75f;

    [Tooltip("Minimum resize scale allowed.")]
    [SerializeField]
    float MinScale = 0.0f;

    [Tooltip("Maximum resize scale allowed.")]
    [SerializeField]
    float MaxScale = 0.7f;
    private HandDraggable handDraggable;

    private Vector3 lastScale;

    private bool scalingDisabled = false;


    [SerializeField]
    bool resizingEnabled = true;
    void Start()
    {
        Collider collider = GetComponentInChildren<Collider>();
        if (collider == null)
        {
            gameObject.AddComponent<BoxCollider>();
        }

        handDraggable = gameObject.GetComponentInParent<HandDraggable>();
        if ( handDraggable ) {
            handDraggable.StartedDragging += HandDraggable_StartedDragging;
            handDraggable.StoppedDragging += HandDraggable_StoppedDragging;
        }
    }

    private void HandDraggable_StoppedDragging() {
        scalingDisabled = false;
    }

    private void HandDraggable_StartedDragging() {
        scalingDisabled = true;
    }

    public void SetResizing(bool enabled)
    {
        resizingEnabled = enabled;
    }

    public void OnManipulationStarted(ManipulationEventData eventData)
    {
        if ( !scalingDisabled ) {
            gameObject.SendMessageUpwards("scaleStarted");
            lastScale = parentObject.localScale;
            InputManager.Instance.PushModalInputHandler(gameObject);
        }
        
    }

    public void OnManipulationUpdated(ManipulationEventData eventData)
    {
        if ( resizingEnabled && !scalingDisabled ) {
            Resize(eventData.CumulativeDelta);
        }
    }

    public void OnManipulationCompleted(ManipulationEventData eventData)
    {
        if ( !scalingDisabled ) {
            gameObject.SendMessageUpwards("scaleEnded");
            InputManager.Instance.PopModalInputHandler();
        }
    }

    public void OnManipulationCanceled(ManipulationEventData eventData)
    {
        InputManager.Instance.PopModalInputHandler();
    }
    void Resize(Vector3 newScale)
    {
        
        Vector3 camHandDelta = Camera.main.transform.InverseTransformDirection(newScale);
        if ( leftToRight ) {
            camHandDelta.Scale(new Vector3(-1, 1, 1));
        }
        // send data to EditModeController.cs once the manipulation gesture is updated
        //gameObject.SendMessageUpwards("scaleButtonClicked", newScale);
        float resizeX, resizeY, resizeZ;
        //if we are warping, honor axis delta, else take the x

        resizeX = resizeY = resizeZ = camHandDelta.x * ResizeScaleFactor;
        resizeX = Mathf.Clamp(lastScale.x + resizeX, MinScale, MaxScale);
        resizeY = Mathf.Clamp(lastScale.y + resizeY, MinScale, MaxScale);
        resizeZ = Mathf.Clamp(lastScale.z + resizeZ, MinScale, MaxScale);
        parentObject.localScale = Vector3.Lerp(parentObject.localScale,
            new Vector3(resizeX, resizeY, resizeZ),
            ResizeSpeedFactor);

    }


    public void OnFocusEnter() {
        if( !scalingDisabled ) {
            gameObject.SendMessageUpwards("DisableHandDraggable");
        }
        gameObject.GetComponent<Renderer>().material = highlightButtonMaterial;
    }

    public void OnFocusExit() {
        if ( !scalingDisabled ) {
            gameObject.SendMessageUpwards("EnableHandDraggable");
        }
        gameObject.GetComponent<Renderer>().material = normalButtonMaterial;
    }
}
