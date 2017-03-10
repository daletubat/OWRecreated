using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthFaceUser : MonoBehaviour {

    public Camera lookAt;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.forward = lookAt.transform.position - transform.position;
	}
}
