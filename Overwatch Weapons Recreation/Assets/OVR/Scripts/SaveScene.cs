using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveScene : MonoBehaviour {

    private StreamWriter file;
    
    public GameObject spawnDesk;
    public GameObject spawnLocker;
    public GameObject spawnTV;

	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () { 
	}

   public  void RecordData()
    {
        file = new StreamWriter("data.txt");
        file.WriteLine("TIME | OBJECT NAME | POSX | POSY | POSZ | ROTX | ROTY | ROTZ | ROTW");
        file.WriteLine("Desks");
        GameObject[] allDesks = UnityEngine.GameObject.FindGameObjectsWithTag("SaveDesks");
        foreach (GameObject test in allDesks)
            if (test.activeInHierarchy)
                file.WriteLine("Desk " + test.transform.position.x + " " + test.transform.position.y + " " + test.transform.position.z + " "
                               + test.transform.rotation.x + " " + test.transform.rotation.y + " " + test.transform.rotation.z + " " + test.transform.rotation.w + " " + test.name); //add scale

        file.WriteLine("Lockers");
        GameObject[] allLockers = UnityEngine.GameObject.FindGameObjectsWithTag("SaveLockers");
        foreach (GameObject test in allLockers)
            if (test.activeInHierarchy)
                file.WriteLine("Locker " + test.transform.position.x + " " + test.transform.position.y + " " + test.transform.position.z + " "
                               + test.transform.rotation.x + " " + test.transform.rotation.y + " " + test.transform.rotation.z + " " + test.transform.rotation.w + " " + test.name); //add scale

        file.WriteLine("TVs");
        GameObject[] allTVss = UnityEngine.GameObject.FindGameObjectsWithTag("SaveTVs");
        foreach (GameObject test in allTVss)
            if (test.activeInHierarchy)
                file.WriteLine("TV " + test.transform.position.x + " " + test.transform.position.y + " " + test.transform.position.z + " "
                               + test.transform.rotation.x + " " + test.transform.rotation.y + " " + test.transform.rotation.z + " " + test.transform.rotation.w + " " + test.name); //add scale

        file.WriteLine("end");
        file.Close();
    }

    public void LoadData()
    {
        ////Destroy current gameObjects

        GameObject[] desObj = GameObject.FindGameObjectsWithTag("SaveDesks");
        foreach(GameObject obj in desObj)
        {
            Debug.Log(obj.name + " destroyed.");
            Destroy(obj);
        }

        GameObject[] desTV = GameObject.FindGameObjectsWithTag("SaveTVs");
        foreach (GameObject obj in desTV)
        {
            Destroy(obj);
        }

        GameObject[] desLock = GameObject.FindGameObjectsWithTag("SaveLockers");
        foreach (GameObject obj in desLock)
        {
            Destroy(obj);
        }


        //while (GameObject.FindGameObjectWithTag("SaveLockers"))
        //{
        //    Destroy(GameObject.FindGameObjectWithTag("SaveLockers"));
        //}

        //while (GameObject.FindGameObjectWithTag("SaveTVs"))
        //{
        //    Destroy(GameObject.FindGameObjectWithTag("SaveTVs"));
        //}

        string[] savedScene = File.ReadAllLines("data.txt");
        foreach (string line in savedScene)
        {
            string[] token = line.Split(" "[0]);
            
            if(token[0] == "Desk")
            {
                Vector3 pos = new Vector3(float.Parse(token[1]), float.Parse(token[2]), float.Parse(token[3]));
                Quaternion rot = new Quaternion(float.Parse(token[4]), float.Parse(token[5]), float.Parse(token[6]), float.Parse(token[7]));
                Instantiate(spawnDesk, pos, rot);

            }

            else if (token[0] == "Locker")
            {
                Vector3 pos = new Vector3(float.Parse(token[1]), float.Parse(token[2]), float.Parse(token[3]));
                Quaternion rot = new Quaternion(float.Parse(token[4]), float.Parse(token[5]), float.Parse(token[6]), float.Parse(token[7]));
                Instantiate(spawnLocker, pos, rot);

            }

            else if (token[0] == "TV")
            {
                Vector3 pos = new Vector3(float.Parse(token[1]), float.Parse(token[2]), float.Parse(token[3]));
                Quaternion rot = new Quaternion(float.Parse(token[4]), float.Parse(token[5]), float.Parse(token[6]), float.Parse(token[7]));
                Instantiate(spawnTV, pos, rot);

            }

        }
        file.Close();

    }
}
