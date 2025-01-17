﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public float throwForce = 10;
    public Vector3 scale;
    public Vector3 objectPos;
    public float distance;
    Camera cam;
    Ray ray;
    RaycastHit hit;
    private float distanceTohit = 10.0f;

    public bool canHold = true;
    public bool shopHold = false;
    public bool inHammering = false;
    public GameObject item;
    public GameObject tempParent;
    public bool isHolding = false;

    public Vector3 direction;
    public Vector3 startpoint;

    public Vector3 itemPos;

    public GameObject playerRightArm;

    private void Start()
    {
        scale = item.transform.localScale;
        tempParent = GameObject.Find("Parent");
        cam = Camera.main;
        playerRightArm = GameObject.FindGameObjectWithTag("RightArm");
    }

    private void Update()
    {
        playerRightArm = GameObject.FindGameObjectWithTag("RightArm");

        tempParent = GameObject.Find("Parent");

        ray = cam.ScreenPointToRay(Input.mousePosition);
        startpoint = ray.origin + (ray.direction * distanceTohit);
        item = this.gameObject;
        itemPos = item.transform.position;
        distance = Vector3.Distance(itemPos, tempParent.transform.position);
        if(distance >= 3f)
        {
            isHolding = false;
        }
        
        if(isHolding == true && inHammering == false)
        {
            item.GetComponent<Rigidbody>().velocity = Vector3.zero;
            item.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            item.transform.parent = tempParent.transform;
            item.transform.position = tempParent.transform.position;
            item.transform.localRotation = Quaternion.Euler(0, 0, 0);
            if (shopHold == true)
            {
                item.GetComponent<Rigidbody>().useGravity = true;
                item.GetComponent<Rigidbody>().isKinematic = false;
                shopHold = false;
            }
            if (Input.GetMouseButtonDown(1))
            {
                playerRightArm.GetComponent<Animator>().Play("ThrowingObject");
                
                RaycastObject();
                direction = (this.transform.position - startpoint).normalized;
                item.GetComponent<Rigidbody>().AddForce(-direction * throwForce);
                isHolding = false;
                canHold = true;
            }

            if (canHold == false)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    playerRightArm.GetComponent<Animator>().Play("DroppingObjec");

                    isHolding = false;
                    canHold = true;
                }
            }
            canHold = false;

        }
        else
        {
            objectPos = item.transform.position;
            item.transform.parent = null;
            if (shopHold == true)
            {
                item.GetComponent<Rigidbody>().useGravity = false;
                item.GetComponent<Rigidbody>().isKinematic = true;

            }
            else
            {
                item.GetComponent<Rigidbody>().useGravity = true;
            }
            item.transform.position = objectPos;
            canHold = true;
        }
    }

    //Makes a raycast and checks if it hits an objects
    private void RaycastObject()
    {
        if (Physics.Raycast(ray, out hit, distanceTohit))
        {
            startpoint = hit.point;
        }
    }

    //On mouse down allows player to pick up objects
    private void OnMouseDown()
    {
        if (distance <= 3f)
        {
            if (canHold == true)
            {
                playerRightArm.GetComponent<Animator>().Play("HoldingObject", -1, 0f);
                isHolding = true;
                if (shopHold == true)
                {
                    item.GetComponent<Rigidbody>().useGravity = false;
                    item.GetComponent<Rigidbody>().isKinematic = true;

                }
                else
                {
                    item.GetComponent<Rigidbody>().useGravity = true;
                }
                item.GetComponent<Rigidbody>().useGravity = false;
                item.GetComponent<Rigidbody>().detectCollisions = true;
                item.transform.localScale = scale;
            }          
        }
    }

    //When the mouse enters the hitbox highlights appears
    private void OnMouseEnter()
    {
        if (this.gameObject.transform.childCount > 0)
        {
            this.gameObject.transform.Find("Highlight").gameObject.SetActive(true);
        }
    }

    //When the mouse exits the hitbox highlight disappears
    private void OnMouseExit()
    {
        if (this.gameObject.transform.childCount > 0)
        {
            this.gameObject.transform.Find("Highlight").gameObject.SetActive(false);
        }
    }

}
