﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrindstoneLogic : MonoBehaviour
{
    public bool isGrinding;
    // Start is called before the first frame update
    GameObject otherOther = null;
    public GameObject handle;
    public GameObject guard;
    public GameObject options;
    public int i;
    Vector3 initialPosition;
    public float endPosition;
    public MoveToPos MTP;
    private int quality;
    private int otherQuality;
    private bool isHandle;
    private bool isGuard;
    public bool playerInPos;
    public bool playerHere;
    void Start()
    {
        initialPosition = transform.position;
        MTP = GameObject.FindObjectOfType<MoveToPos>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerHere)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                MTP.returnToPos();
            }
        }
        if (i >= 100 && otherOther != null)
        {
            Destroy(otherOther);
            if (isHandle)
            {
                GameObject craftedHandle = Instantiate(handle, initialPosition + new Vector3(1, 0.25f, 0.21f), Quaternion.identity);
                isHandle = false;
                craftedHandle.GetComponent<Handle>().quality = (quality + otherQuality) / 2;
            }
            if (isGuard)
            {
                GameObject craftedGuard = Instantiate(guard, initialPosition + new Vector3(1, 0.25f, 0.21f), Quaternion.identity);
                craftedGuard.GetComponent<Guard>().quality = (quality + otherQuality) / 2;

                isGuard = false;

            }
            i = 0;
            playerInPos = false;
            MTP.returnToPos();
        }
        if (Input.GetKeyDown(KeyCode.Space) && playerInPos)
        {
            isGrinding = true;
        }

        if(Input.GetKeyUp(KeyCode.Space))
        {
            isGrinding = false;
        }
        if (otherOther != null)
        {
            if (Input.GetMouseButtonDown(0))
            {
               
                otherOther.GetComponent<Rigidbody>().isKinematic = false;
                otherOther.transform.position = new Vector3(0,0,0);
            }

            if (isGrinding)
            {
                {
                    transform.position = new Vector3(initialPosition.x, initialPosition.y, 4.94f );
                    otherOther.transform.position = transform.position = new Vector3(initialPosition.x, initialPosition.y, initialPosition.z + endPosition);

                    otherOther.transform.eulerAngles = new Vector3(0, 0, 0);
                    i++;
                }


            }
            if (!isGrinding)
            {
                {
                    otherOther.transform.position =  initialPosition;
                   
                    otherOther.transform.eulerAngles = new Vector3(0, 0, 0);
                }
            }
        }

    }

    void yes()
    {
        

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "gsHazard")
        {
            quality = quality - 10;
            isGrinding = false;
            
        }
        if(other.gameObject.tag == "Iron Sheet")
        {
            if (other.GetComponent<Sheet>().size == Sheet.TypeSheet.small)
            {
                other.gameObject.GetComponent<Sheet>().sheetPickup.isHolding = false;


                otherQuality = other.GetComponent<Sheet>().quality;
                quality = 100;
                //MPTP.gotoGrinder();
                

                otherOther = other.gameObject;
                other.transform.position = new Vector3(0, 0, 0);
                other.transform.parent = null;


                other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                other.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                other.transform.eulerAngles = new Vector3(0, 0, 0);
            }
        }

    }


    public void chooseHandle()
    {
        options.SetActive(false);
        isHandle = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        playerInPos = true;

    }

    public void chooseGuard()
    {
        options.SetActive(false);
        Debug.Log("Aaaaaa");

        isGuard = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        playerInPos = true;
    }

 

}