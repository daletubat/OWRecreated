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
    float bulletDur = .06f;
    float startTime;

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
        shotVisual.enabled = true;
        StartCoroutine(animateBullet(shotPoint, shotVisual));
        //shotVisual.enabled = false;
    }

    IEnumerator animateBullet(Vector3 shotPoint, LineRenderer shotVisual) {
        startTime = Time.time;

        Vector3 bulletTrajectory = shotPoint - bulletOrigin;
        Vector3 bulletScale = Vector3.Normalize(bulletTrajectory);
        

        for (float i = 0; i < bulletDur; i += Time.deltaTime)
        {
            float disCovered = (Time.time - startTime) * 3;
            float ratio = bulletDur;
            shotVisual.SetPosition(0, bulletOrigin + (bulletTrajectory * disCovered) - (bulletScale*bulletSize));
            shotVisual.SetPosition(1, bulletOrigin + (bulletTrajectory*disCovered));
            yield return null;
        }


    }

}
