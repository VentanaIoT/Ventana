using UnityEngine;
using HoloToolkit.Unity.InputModule;

/// <summary>
/// The Interactible class flags a Game Object as being "Interactible".
/// Determines what happens when an Interactible is being gazed at.
/// </summary>
public class Interactible : MonoBehaviour, IInputClickHandler
{
    private Material[] defaultMaterials;  

    void Start()
    {
        defaultMaterials = GetComponent<Renderer>().materials;

        // Add a BoxCollider if the interactible does not contain one.
        Collider collider = GetComponentInChildren<Collider>();
        if (collider == null)
        {
            gameObject.AddComponent<BoxCollider>();
        }

        // Show the more buttons to start, can be initialized to false if we don't want to show them
        Renderer[] renderer = GetComponentsInChildren<Renderer>();
        foreach (Renderer child in renderer)
            child.enabled = true;
    }

    void GazeEntered()
    {
        // Debug.Log("GazeEntered");
        Renderer[] renderer = GetComponentsInChildren<Renderer>();
        foreach (Renderer child in renderer)
            child.enabled = true;
    }

    void GazeExited()
    {
       // Debug.Log("GazeExited");
 
        Renderer[] renderer = GetComponentsInChildren<Renderer>();
        foreach (Renderer child in renderer)
            child.enabled = false;
    }

    public void OnInputClicked(InputClickedEventData eventData)
    {
        //Debug.Log("<color=yellow>EY BAY BAY</color>");

        // Send a more button clicked message to EditModeController script
        gameObject.SendMessageUpwards("MoreButtonClicked");
    }
}
