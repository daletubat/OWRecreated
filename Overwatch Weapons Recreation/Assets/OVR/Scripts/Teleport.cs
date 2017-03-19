using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public GameObject player;

    public OVRInput.Controller LController;
    public OVRInput.Controller RController;


    public GameObject rHand;
    public GameObject lHand;
    public GameObject rayObject;


    public selectedMat selecMat;
    public Spawn spawner;


    GameObject target;
    public GameObject groupParent;

    public SaveScene saver;

    GameObject currBoard;
    bool whiteboardMode;
    string savetag;
    GameObject tempParent;
    bool hasParent;


    // Use this for initialization
    void Start()
    {
        groupParent = new GameObject();
        whiteboardMode = false;
        hasParent = false;
    }

    // Update is called once per frame
    void Update()
    {

        /////////////RIGHT HAND////////////////
        if (!whiteboardMode)
        {
            Ray gazeRight = new Ray(rHand.transform.position, rHand.transform.forward);
            Debug.DrawRay(rHand.transform.position, rHand.transform.forward * 10, Color.green);
            RaycastHit hitRight;

            if (Physics.Raycast(gazeRight, out hitRight, Mathf.Infinity))
            {
                LineRenderer ray = rHand.GetComponent<LineRenderer>();
                ray.SetPosition(0, rHand.transform.position);
                ray.SetPosition(1, hitRight.point);
                ////////////A BUTTON: TELEPORT////////////
                if (OVRInput.GetDown(OVRInput.Button.One))
                {
                    Vector3 rayRot = rHand.transform.forward;
                }

                else if (OVRInput.GetUp(OVRInput.Button.One))
                {
                    if (hitRight.transform.GetComponent<IsTerrain>())
                    {
                        Vector3 newPos = hitRight.point;
                        transform.parent.transform.position = newPos;
                    }

                    //save and load
                    else if (hitRight.transform.GetComponent<IsSaver>())
                    {
                        saver.RecordData();
                    }
                    else if (hitRight.transform.GetComponent<IsLoader>())
                    {
                        saver.LoadData();
                    }
                }

                ////////////B BUTTON: SELECT AND MOVE////////////
                else if (OVRInput.GetDown(OVRInput.Button.Two))
                {

                    target = hitRight.transform.gameObject;
                    if (target.GetComponent<IsWhiteboard>() == true)
                    {
                        Debug.Log("WHITEBOARD");
                        target.GetComponent<Rigidbody>().isKinematic = true;
                        target.transform.parent = rHand.transform;
                        currBoard = target;
                        whiteboardMode = true;
                    }

                    else if (target.GetComponent<IsStatic>() == false)
                    {
                        savetag = target.tag;
                        target.GetComponent<Rigidbody>().isKinematic = true;
                        if (target.transform.parent)
                        {
                            hasParent = true;
                            tempParent = target.transform.parent.gameObject;
                        }
                        target.transform.parent = rHand.transform;
                        

                    }
    


                    else
                    {
                        if (target.GetComponent<IsPanel>() == true)
                        {
                            spawner.setPlane(target.GetComponent<IsPanel>().objInt);
                        }
                    }
                }
                else if (OVRInput.GetUp(OVRInput.Button.Two))
                {

                    //target.AddComponent<Rigidbody>();
                    if (target.GetComponent<IsStatic>() == false)
                    {
                        Vector3 vel = target.GetComponent<Rigidbody>().velocity;
                        target.GetComponent<Rigidbody>().isKinematic = false;
                        if (hasParent)
                        {
                            target.transform.parent = tempParent.transform;
                            hasParent = false;
                        }
                        else
                        {
                            target.transform.parent = null;
                        }
                        target.tag = savetag;
                    }
                }
            }

        }
        else
        {

            Ray gazeRight = new Ray(rHand.transform.position, rHand.transform.forward);
            Debug.DrawRay(rHand.transform.position, rHand.transform.forward * 10, Color.green);
            RaycastHit[] rayHit = Physics.RaycastAll(gazeRight, Mathf.Infinity);

            LineRenderer ray = rHand.GetComponent<LineRenderer>();
            ray.SetPosition(0, rHand.transform.position);
            ray.SetPosition(1, rayHit[0].point);

            if (OVRInput.GetDown(OVRInput.Button.Two))
            {
                for (int i = 0; i < rayHit.Length; i++)
                {
                    GameObject check = rayHit[i].transform.gameObject;
                    Debug.Log(check.tag);
                    if(check.tag == "Wall")
                    {
                        currBoard.transform.parent = null;

                        int num = check.gameObject.GetComponent<isWall>().num;
                        Vector3 rotate = new Vector3(0,0,0);
                        Debug.Log("Wall Number" + num);
                        //don't look at this please
                        switch (num)
                        {
                            case 0:
                                Debug.Log("CASE 0");
                            rotate = new Vector3 (-90, 0, 90);
                            break;

                            case 1:
                                rotate = new Vector3(-90, 90, 90);
                                break;

                            case 2:
                                rotate = new Vector3(-90, 188, 0);
                                break;

                            case 3:
                                rotate = new Vector3(-90, 0, -95);
                                break;

                            case 4:
                                rotate = new Vector3(-90, -10, 0);
                                break;
                            case 5:
                                rotate = new Vector3(-90, 0, 0);
                                break;
                        }

                        currBoard.transform.position = rayHit[i].point;
                        Vector3 fuck = currBoard.transform.localPosition;
                        //fuck.z += 1;
                        currBoard.transform.position = fuck;
                        currBoard.transform.rotation = Quaternion.Euler(rotate);
                        currBoard.GetComponent<Rigidbody>().isKinematic = true;
                        
                        target = null;
                        whiteboardMode = false;
                    }
                }
               
            }

            else if (OVRInput.GetUp(OVRInput.Button.Two)) {

            }
            
        }





        /////////////LEFT HAND////////////////
        Ray gazeLeft = new Ray(lHand.transform.position, lHand.transform.forward);
        Debug.DrawRay(lHand.transform.position, lHand.transform.forward * 10, Color.red);
        RaycastHit hitLeft;

        if (Physics.Raycast(gazeLeft, out hitLeft, Mathf.Infinity))
        {
            LineRenderer lray = lHand.GetComponent<LineRenderer>();
            lray.SetPosition(0, lHand.transform.position);
            lray.SetPosition(1, hitLeft.point);

            ////////////X BUTTON: SELECT TO GROUP////////////
            if (OVRInput.GetUp(OVRInput.Button.Three))
            {

                target = hitLeft.transform.gameObject;
                if (target.GetComponent<IsStatic>() == false)
                {

                    //Adding to group                
                                     
                    if (target.transform.parent == null || target.transform.parent.transform != groupParent.transform)
                    {
                        target.GetComponent<selectedMat>().changeSelMaterial();
                        Debug.Log(target.transform.name + " added to Group.");
                        target.transform.parent = groupParent.transform;
                    }

                    //Removing from group
                    else
                    {
                        target.GetComponent<selectedMat>().revertMaterial();
                        Debug.Log(target.transform.name + " removed from Group.");
                        target.transform.parent = null;
                    }
                }


            }

            else if (OVRInput.GetUp(OVRInput.Button.Three))
            {

            }

            ////////////Y BUTTON: SELECT AND MOVE GROUP////////////
            else if (OVRInput.GetDown(OVRInput.Button.Four))
            {

                foreach (Transform child in groupParent.transform)
                {
                    child.GetComponent<Rigidbody>().isKinematic = true;
                }
                groupParent.transform.parent = lHand.transform;
            }


            else if (OVRInput.GetUp(OVRInput.Button.Four))
            {

                foreach (Transform child in groupParent.transform)
                {
                    child.GetComponent<Rigidbody>().isKinematic = false;
                }
                groupParent.transform.parent = null;
            }
        }

    }



}
