using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightRequestScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void makeAPIRequest(string child) {

        Debug.Log(child + " Wants to make an API request");

    }

    void OnSliderChangeRequest(KnobHandler.SliderLevels levels) {

        Debug.Log("Requesting a: " + levels.XAxisLevel + (levels.XAxisLevel > 0 ? " increase" : " decrease"));

    }
}
