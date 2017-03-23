using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseVentanaController : MonoBehaviour, IVentanaVumarkEventHandler {

    public int VentanaID = -1;
    public string ControllerName;
    
    // Use this for initialization
    protected void Start () {
        Debug.Log("ID: " + VentanaID);
	}
	
	// Update is called once per frame
	protected void Update () {
		
	}

    public virtual void OnVumarkFound() {

    }

    public virtual void OnVumarkLost() {

    }
}
