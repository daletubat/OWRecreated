﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchControllers : MonoBehaviour {

    public OVRInput.Controller controller;

	// Update is called once per frame
	void Update () {
        transform.localPosition = OVRInput.GetLocalControllerPosition(controller);
        transform.localRotation = OVRInput.GetLocalControllerRotation(controller);
    }
}
