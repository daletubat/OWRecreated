using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLocomotion : MonoBehaviour {

    //Public Settings
    public Transform destination;
    public float speed;

    //Data
    public bool active;
    float startTime;
    float journeyDistance;
    Vector3 startPos;

	void Start () {
        active = true;
        startTime = Time.time;
        startPos = transform.position;
        journeyDistance = Vector3.Distance(destination.position, startPos);
	}

	void Update () { 
        if (active)
        {
            //Using Lerp; Isn't accurate when using graviton
            //float disCovered = (Time.time - startTime) * speed;
            //float fracJourney = disCovered / journeyDistance;
            //transform.position = Vector3.Lerp(transform.position, destination.position, fracJourney);

            Vector3 direction = destination.position - transform.position;
            transform.forward = direction;
            transform.position += transform.forward * (speed * 0.05f);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.GetComponent<isDestination>())
        {
            Destroy(gameObject);
        }
    }
}
