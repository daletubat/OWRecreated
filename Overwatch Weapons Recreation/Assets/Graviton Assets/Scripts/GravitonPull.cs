using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//GravitonPull.cs
//Creates the behavior of the Graviton Surge once it is fired into the world.

public class GravitonPull : MonoBehaviour {

    //Prefabs
    public GameObject GravitonEpicenterPrefab;

    //Public Settings
    public int lifeTime;
    public int pullRadius;
    public int pullForce;

    //InGame Objects
    GameObject gravEpicenter;
    

    //Object Data
    float timeActivated;
    bool activate;
    Collider[] objectsInRadius;
    Vector3 epicenter;
    

    void Start()
    {
        gravEpicenter = null;
        activate = false;
    }


	// Update is called once per frame
	void Update () {
        if (activate)
        {
            timeActivated += Time.deltaTime;

            //Re-evaluate all of the objects in the radius
            objectsInRadius = Physics.OverlapSphere(epicenter, pullRadius);

            //Apply forces to all of the objects in the radius
            foreach (Collider collider in objectsInRadius)
            {
                GameObject obj = collider.gameObject;
                if (obj.GetComponent<isKinematic>())
                {
                    PullObject(obj);
                }
            }
        }

        //Expiration Timer
        if (timeActivated > lifeTime)
        {
            activate = false;
            Destroy(gravEpicenter);
            Destroy(this.gameObject);
        }
    }

    //When bullet hits the terrain, spawns the Graviton Epicenter in the world
    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.GetComponent<isTerrain>())
        {
            //Graviton Activated in the world
            activate = true;
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<SphereCollider>().enabled = false;
            GetComponent<Rigidbody>().isKinematic = true;

            //Epicenter of the Graviton is the first point of contact
            epicenter = collision.contacts[0].point;

            //Spawn the Graviton
            gravEpicenter = Instantiate(GravitonEpicenterPrefab, epicenter, Quaternion.identity);

            //Grab the objects in the radius
            objectsInRadius = Physics.OverlapSphere(epicenter, pullRadius);

        }
    }

    //Pull objects towards the center of the graviton
    void PullObject(GameObject obj)
    {
        Debug.Log("Pulling");

        //Calculate the center of the graviton
        Vector3 pullCenter = -(obj.transform.position - epicenter);
        

        //Proportional to the distance from object to center of object;
        //As the object gets closer to the center, the force gets higher.
        float proportion = Mathf.Max(0.5f/Vector3.Magnitude(pullCenter), 1);

        //Normalize and raise the vector that pulls the objects towards the center
        pullCenter.y += 1.5f;
        pullCenter.Normalize();

        //Apply forces to the objects
        obj.GetComponent<Rigidbody>().AddForce(pullCenter * pullForce * proportion);
    }


}
