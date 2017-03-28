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
        InputManager.Instance.PushModalInputHandler(gameObject);
        gameObject.SendMessageUpwards("scaleStarted");
    }

    public void OnManipulationUpdated(ManipulationEventData eventData)
    {
        if (resizingEnabled)
            Resize(eventData.CumulativeDelta);
    }

    public void OnManipulationCompleted(ManipulationEventData eventData)
    {
        InputManager.Instance.PopModalInputHandler();
    }

    public void OnManipulationCanceled(ManipulationEventData eventData)
    {
        InputManager.Instance.PopModalInputHandler();
    }
    void Resize(Vector3 newScale)
    {
        // send data to EditModeController.cs once the manipulation gesture is updated
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
