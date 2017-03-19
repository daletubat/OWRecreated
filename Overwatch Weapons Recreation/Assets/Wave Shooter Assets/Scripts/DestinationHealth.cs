using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestinationHealth : MonoBehaviour {

    //Public Settings

    public float health;



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.GetComponent<isEnemy>())
        {
            health -= 10;
            Debug.Log("Health: " + health);
        }

        if (health <= 0){
        //Insert Game Over Here
        }
    }
}
