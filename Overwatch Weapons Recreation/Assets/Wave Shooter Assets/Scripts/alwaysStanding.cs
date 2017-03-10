using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class alwaysStanding : MonoBehaviour {

    Vector3 upwards;

	// Use this for initialization
	void Start () {
        upwards = transform.up;
	}
	
	// Update is called once per frame
	void Update () {
        transform.up = upwards;
	}
}
