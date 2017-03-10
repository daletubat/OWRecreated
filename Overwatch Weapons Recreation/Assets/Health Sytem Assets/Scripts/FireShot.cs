using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireShot : MonoBehaviour {

    //Public Settings
    public float damage;
    public int fireRate; //controls the rate at which bullets are fired
    public int magSize;
    public float bullSpread;
    public float falloffDistance; //distance at which the bullets begin to lose inaccuracy
    public BulletHandler handler;

    
    //Data
    int currFireRate;
    int currMagSize;
    bool spreadApplied;
    int fired; //shots fired
    int landed; //shots landed


    void Start () {
        currFireRate = fireRate;
        currMagSize = magSize;
        spreadApplied = false;

        fired = 0;
        landed = 0;
    }
	

	void Update () {

        if (!handler)
        {
            Debug.Log("help");
        }



        if (Input.GetMouseButton(0))
        {

            currFireRate++;
            if (currFireRate >= fireRate)
            {
                //Bullet has been fired; reset the counter
                currFireRate = 0;
                fired++;

                //Right now, the calculation ray is originating from the user, not the gun;
                //only the animation is originating from the gun
                Vector3 shotOrigin = transform.position;

                //Adjust the height of the shot to be more accurate to the center
                shotOrigin.y += 0.3f;

                //Cast the calculation ray
                Ray shotRay = new Ray(shotOrigin, transform.forward);
                RaycastHit shotHit;

                if (Physics.Raycast(shotRay, out shotHit, Mathf.Infinity))
                {
                    //Calculate the distance of the shot
                    float distance = Vector3.Distance(shotOrigin, shotHit.point);

                    Vector3 shotHitPoint = shotHit.point;

                    //Apply bullet spread inaccuracy to applied distances
                    if(distance > falloffDistance)
                    {
                        spreadApplied = true;

                        float diff = distance - falloffDistance;
                        diff *= 0.1f;
                        float spreadX = UnityEngine.Random.Range(-bullSpread, bullSpread);
                        float spreadY = UnityEngine.Random.Range(-bullSpread, bullSpread);

                        shotHitPoint.x += spreadX * diff;
                        shotHitPoint.y += spreadY * diff;
                    }

                    handler.animateShot(shotHitPoint);

                    //Because spread is applied, we need to use another raycast to check if it still hits the enemy.
                    if (spreadApplied)
                    {
                        Ray spreadRay = new Ray(shotOrigin, shotHitPoint - shotOrigin);
                        RaycastHit spreadHit;

                        if (Physics.Raycast(spreadRay, out spreadHit, Mathf.Infinity))
                        {

                            if (spreadHit.transform.GetComponent<isEnemy>())
                            {

                                GameObject enemy = spreadHit.transform.gameObject;
                                enemy.GetComponent<PlayerHealth>().decreaseHealth(damage);
                                landed++;
                            }
                        }
                        spreadApplied = false;
                    }

                    //No spread applieds
                    else
                    {
                        if (shotHit.transform.GetComponent<isEnemy>())
                        {
                            GameObject enemy = shotHit.transform.gameObject;
                            enemy.GetComponent<PlayerHealth>().decreaseHealth(damage);
                            landed++;
                        }
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
