using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualHand : MonoBehaviour {
    bool holding;
    GameObject objToHold;
    selectedMat selecMat;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (holding)
        {
            if (OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, OVRInput.Controller.RTouch) == 0.0f)
            {
                holding = false;
                objToHold.GetComponent<Rigidbody>().isKinematic = false;
                objToHold.transform.parent = null;
                objToHold = null;

            }
        }
        else
        {
            if (OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, OVRInput.Controller.RTouch) < 0.0f)
            {
                
            }
        }
	}

    private void OnTriggerStay(Collider other)
    {
        if (OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, OVRInput.Controller.RTouch) > 0.0f)
        {
            if (objToHold == null)
            {
                if (other.gameObject.GetComponent<IsStatic>() == false)
                {
                    holding = true;

                    objToHold = other.gameObject;
                    objToHold.GetComponent<Rigidbody>().isKinematic = true;
                    objToHold.transform.parent = transform;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //selecMat.changeSelMaterial(objToHold);
    }

}
