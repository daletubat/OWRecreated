using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretSeeker : MonoBehaviour {
    //Public Settings
    public int seekRadius;
    public float damage;

    //Local Data
    Collider[] objectsInRadius;
    float lowDist;
    

	void Update () {
        objectsInRadius = Physics.OverlapSphere(transform.position, seekRadius);
        focusTarget();
	}
    void focusTarget()
    {
        lowDist = seekRadius;

        GameObject target = null;
        foreach (Collider collider in objectsInRadius)
        {
            if (collider.gameObject.GetComponent<PlayerHealth>())
            {
                float dist = Vector3.Distance(collider.gameObject.transform.position, transform.position);

                if (dist < lowDist)
                {
                    lowDist = dist;
                    target = collider.gameObject;
                }
            }
        }
        if (target)
        {
            target.GetComponent<PlayerHealth>().decreaseHealth(damage);

            LineRenderer rend = GetComponent<LineRenderer>();
            rend.SetPosition(0, transform.position);
            rend.SetPosition(1, target.transform.position);
        }

    }
}
