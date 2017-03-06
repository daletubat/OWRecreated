using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireShot : MonoBehaviour {

    public int fireRate;
    int currFireRate;
    // Use this for initialization
    void Start () {
        currFireRate = fireRate;
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
                    animateShot(shotOrigin, shotHit.point);

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

    void animateShot(Vector3 shotOrigin, Vector3 shotPoint)
    {
        LineRenderer shotVisual = GetComponent<LineRenderer>();

        shotVisual.SetPosition(0, shotOrigin);
        shotVisual.SetPosition(1, shotPoint);
    }


}
