﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseVentanaController : MonoBehaviour, IVentanaVumarkEventHandler {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public virtual void OnVumarkFound() {

    }

    public virtual void OnVumarkLost() {

    }
}
