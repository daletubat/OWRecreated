﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHandler : MonoBehaviour {

    //Public Settings
    public BulletRenderer bullets;
    public GameObject bulletOriginObject;
    public float bulletSize;
    //Data
    Vector3 bulletOrigin;
    float bulletDur = .1f;

	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void animateShot(Vector3 shotPoint)
    {
        BulletRenderer currRenderer = bullets;
        //foreach(BulletRenderer brenderer in bullets) {
        //    if(!brenderer.activated) {
        //        currRenderer = brenderer;
        //        break;
        //    }
        //}

        LineRenderer shotVisual = currRenderer.GetComponent<LineRenderer>();

        bulletOrigin = bulletOriginObject.transform.position;

        StartCoroutine(animateBullet(shotPoint, shotVisual));

        //shotVisual.SetPosition(0, bulletOrigin);
        //shotVisual.SetPosition(1, shotPoint);

    }

    IEnumerator animateBullet(Vector3 shotPoint, LineRenderer shotVisual) {

        shotVisual.enabled = true;
        
        Vector3 bulletTrajectory = shotPoint - bulletOrigin;
        Vector3 bulletScale = Vector3.Normalize(bulletTrajectory);

        for (float i = 0; i < bulletDur; i += Time.deltaTime)
        {
            float ratio = i / bulletDur;
            shotVisual.SetPosition(0, bulletOrigin + (bulletTrajectory * ratio) - (bulletScale*bulletSize));
            shotVisual.SetPosition(1, bulletOrigin + (bulletTrajectory*ratio));
            yield return null;
        }

        shotVisual.enabled = false;
    }

}
