using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;
using UnityEngine.SceneManagement;


public class LoadButtonController : MonoBehaviour, IInputClickHandler
{

    public Transform persistentGO;

    public void OnInputClicked(InputClickedEventData eventData)
    {
        //Load the next scene
        persistentGameObjectScript pgo = persistentGO.GetComponent<persistentGameObjectScript>();
        pgo.loadWorldAnchors = true;
        SceneManager.LoadSceneAsync("Ventana", LoadSceneMode.Single);
    }

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
}
