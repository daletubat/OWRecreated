using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireShot : MonoBehaviour {

    //Public Settings
    public int fireRate;
    public int magSize;
    public BulletHandler handler;
 
    
    //Data
    int currFireRate;
    int currMagSize;


    // Use this for initialization
    void Start () {
        currFireRate = fireRate;
        currMagSize = magSize;
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButton(0))
        {
            currFireRate++;
            
            if (currFireRate >= fireRate)
            {
                currFireRate = 0;
                Vector3 shotOrigin = transform.position;
                shotOrigin.y += 0.2f;
                Ray shotRay = new Ray(shotOrigin, transform.forward);
                RaycastHit shotHit;

                if (Physics.Raycast(shotRay, out shotHit, Mathf.Infinity))
                {
                    
                    handler.animateShot(shotHit.point);

                    if (shotHit.transform.GetComponent<isEnemy>())
                    {
                        GameObject enemy = shotHit.transform.gameObject;
                        enemy.GetComponent<PlayerHealth>().decreaseHealth(10);
                    }
                }
            }

        }

        if (Input.GetMouseButtonUp(0))
        {
            currFireRate = fireRate;
        }
		
	}

}
