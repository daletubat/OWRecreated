using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour {



    public Transform spawnPoint;

    public GameObject chairObj;
    public GameObject lockerObj;
    public GameObject deskObj;
    public GameObject boardObj;
    public GameObject TVObj;
    public GameObject cabinetObj;

   public Plane chairPlane;
    public Plane lockerPlane;
    public Plane deskPlane;
    public Plane boardPlane;
    public Plane TVPlane;
   

    Plane currPlane;
    GameObject currObj;
    // Use this for initialization
    void Start () {
        currPlane = chairPlane;
        currObj = chairObj;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void spawnItem(){
        Debug.Log("SPAWNING");
        Instantiate(currObj, spawnPoint.position, Quaternion.identity);
    }

    public void setPlane(int obj) {
        switch(obj) 
        {
            case 1:
                currPlane = chairPlane;
                currObj = chairObj;
                break;
            case 2:
                currPlane = lockerPlane;
                currObj = lockerObj;
                break;
            case 3:
                currPlane = deskPlane;
                currObj = deskObj;
                break;
            case 4:
                currPlane = boardPlane;
                currObj = boardObj;
                break;
            case 5:
                currPlane = boardPlane;
                currObj = TVObj;
                break;
            case 6:
                currPlane = chairPlane;
                currObj = cabinetObj;
                break;
        }
        Debug.Log("PANEL SET TO: " + obj);
        spawnItem();
    }
}
