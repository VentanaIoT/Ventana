using System;
using HoloToolkit.Unity.InputModule;
using UnityEngine;

public class scalingHandler : MonoBehaviour, IManipulationHandler, IFocusable
{
    public Material highlightButtonMaterial;
    public Material normalButtonMaterial;

    [SerializeField]
    bool resizingEnabled = true;
    void Start()
    {
        Collider collider = GetComponentInChildren<Collider>();
        if (collider == null)
        {
            gameObject.AddComponent<BoxCollider>();
        }
    }

    public void SetResizing(bool enabled)
    {
        resizingEnabled = enabled;
    }

    public void OnManipulationStarted(ManipulationEventData eventData)
    {

    }

    public void OnManipulationUpdated(ManipulationEventData eventData)
    {
        if (resizingEnabled)
            Resize(eventData.CumulativeDelta);
    }

    public void OnManipulationCompleted(ManipulationEventData eventData)
    {
        
    }

    public void OnManipulationCanceled(ManipulationEventData eventData)
    {

    }
    void Resize(Vector3 newScale)
    {
        // send data to ttpMode.cs once the manipulation gesture is updated
        Debug.Log("bubbling to scalebuttonclicked" + newScale);
        gameObject.SendMessageUpwards("scaleButtonClicked", newScale);
    }

    public void OnFocusEnter() {
        gameObject.SendMessageUpwards("DisableHandDraggable");
        gameObject.GetComponent<Renderer>().material = highlightButtonMaterial;
    }

    public void OnFocusExit() {
        gameObject.SendMessageUpwards("EnableHandDraggable");
        gameObject.GetComponent<Renderer>().material = normalButtonMaterial;
    }
}
