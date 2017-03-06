using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitonAnimation : MonoBehaviour {

    //InGameObjects
    Transform shell;
    GameObject center;
    

    //Public Settings
    static public float dur = .25f;

    //Data
    float time;
    Vector3 origin = new Vector3(3.0f, 3.0f, 3.0f);
    Vector3 origSize;
    bool run;

	// Use this for initialization
	void Start () {
        run = false;
        shell = transform.GetChild(0);
        origSize = shell.localScale;
        StartCoroutine(AnimateShell());
	}
	
	// Update is called once per frame
	void Update () {
        if (!run)
        {
            StartCoroutine(AnimateShell());
        }
    }

    IEnumerator AnimateShell()
    {
        run = true;
        for(float j = 0; j < dur; j+= Time.deltaTime)
        {
            Vector3 newScale = Vector3.Lerp(origSize, origin, j / dur);
            shell.localScale = newScale;
            yield return null;
        }
        run = false;
    }
}

