using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//FireGravitonSurge.cs
//Fires the graviton shot from the player into the world. 
//Does NOT handle any environmental changes (pull characters or props closer to the epicenter).


public class FireGravitonSurge : MonoBehaviour {
    ///Prefabs
    public GameObject gravitonBulletPrefab;

    //Settings
    public int velocityMultiplier;
    public float gravityMultiplier;

    //InGame Objects
    GameObject gravShot;

	void Update () {
        if (Input.GetMouseButtonDown(1))
        {
            //Play Sound Effect
            GetComponent<AudioSource>().Play();

            //Apply origin of shot
            Vector3 gravOrigin = transform.position;
            gravOrigin += transform.forward * 2;
            gravShot = Instantiate(gravitonBulletPrefab, gravOrigin, Quaternion.identity);
            
            //Apply upward angle and velocity of shot
            Vector3 gravShotAngle = transform.forward;
            gravShotAngle.y += 0.2f;
            gravShot.GetComponent<Rigidbody>().velocity += gravShotAngle * velocityMultiplier;
            
        }

        if (gravShot)
        {
            //Apply downward gravitational force
            gravShot.GetComponent<Rigidbody>().AddForce(new Vector3(0, -1, 0) * gravityMultiplier);
        }
	}
}
