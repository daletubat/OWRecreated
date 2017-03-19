using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selectedMat : MonoBehaviour {
    public Material selecMat;
    Material[] savedMats;
 
    // Use this for initialization
    void Start () {
        savedMats = new Material[100];
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void changeSelMaterial()
    {
  
        //Store renderer before color swap
        Renderer[] rends = GetComponentsInChildren<Renderer>();


        //Swap materials
        for (int i = 0; i < rends.Length; i++)
        {
            Material[] mats = rends[i].materials;
            
            for (int j = 0; j < mats.Length; j++)
            {
                savedMats[j] = mats[j];
                mats[j] = selecMat;
            }
            //set materials
            rends[i].materials = mats;
        }
       

    }


    public void revertMaterial()
    {
        //Grab the renderers you need to change back
        Renderer[] rendToRevert = GetComponentsInChildren<Renderer>();
        //Find index of the target's renderer array 


        for(int i = 0; i < rendToRevert.Length; i++)
        {
            Material[] mats = rendToRevert[i].materials;

            for (int j = 0; j < mats.Length; j++)
            {
                mats[j] = savedMats[j];
            }

            rendToRevert[i].materials = mats;
        }
    }
}
