﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smorge : MonoBehaviour
{
    public Furnace furance;
    public Furnace furance2;
    public Smelter smelter;
    public static string oreplace1 = "empty";
    public static string oreplace2 = "empty";
    public static string oreplace3 = "empty";
    public static string oreplace4 = "empty";
    public static string oreplace5 = "empty";
    public static string oreplace6 = "empty";
    public int place;
    public Transform[] oreplace;
    public float time;
    public float timeDecrease;
    public float timeIncrease;
    public string objectName;
    public bool smorgeOn = false;

    // Start is called before the first frame update
    void Start()
    {
        time = 0;
        objectName = this.gameObject.name;
    }

    // Update is called once per frame
    void Update()
    {
        if(time > 0)
        {
            smorgeOn = true;
            furance.smorgeOn = smorgeOn;
            furance2.smorgeOn = smorgeOn;

            smelter.smorgeOn = smorgeOn;
            if (smorgeOn == true)
            {
                this.gameObject.name = objectName + " (Ready)";
            }
            time -= timeDecrease * Time.deltaTime;
        }
        else
        {
            smorgeOn = false;
            furance.smorgeOn = smorgeOn;
            furance2.smorgeOn = smorgeOn;

            smelter.smorgeOn = smorgeOn;
            if (smorgeOn == false)
            {
                this.gameObject.name = objectName + " (Not Ready)";
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Iron Ore")
        {
            if(other.gameObject.GetComponent<Ore>().material == Ore.OreMaterial.coal)
            {
                other.gameObject.GetComponent<Ore>().orePickup.isHolding = false;
                other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                other.gameObject.GetComponent<Ore>().smorge = this;
                other.gameObject.GetComponent<Ore>().timeToDestroy = timeIncrease;
                other.gameObject.GetComponent<Ore>().timeDecrease = timeDecrease;
                time += timeIncrease;
                if (oreplace1 == "empty")
                {
                    other.transform.position = oreplace[0].transform.position;
                    other.gameObject.GetComponent<Ore>().place = 1;
                    oreplace1 = "full";
                }
                else if (oreplace2 == "empty")
                {
                    other.transform.position = oreplace[1].transform.position;
                    other.gameObject.GetComponent<Ore>().place = 2;
                    oreplace2 = "full";
                }
                else if (oreplace3 == "empty")
                {
                    other.transform.position = oreplace[2].transform.position;
                    other.gameObject.GetComponent<Ore>().place = 3;
                    oreplace3 = "full";
                }
                else if (oreplace4 == "empty")
                {
                    other.transform.position = oreplace[3].transform.position;
                    other.gameObject.GetComponent<Ore>().place = 4;
                    oreplace4 = "full";
                }
                else if (oreplace5 == "empty")
                {
                    other.transform.position = oreplace[4].transform.position;
                    other.gameObject.GetComponent<Ore>().place = 5;
                    oreplace5 = "full";
                }
                else if (oreplace6 == "empty")
                {
                    other.transform.position = oreplace[5].transform.position;
                    other.gameObject.GetComponent<Ore>().place = 6;
                    oreplace6 = "full";
                }
            }
            else
            {
                return;
            }
        }
        else
        {
            return;
        }
    }

    public void Empty()
    {
        if(place == 1)
        {
            oreplace1 = "empty";
        }
        else if (place == 2)
        {
            oreplace2 = "empty";
        }
        else if (place == 3)
        {
            oreplace3 = "empty";
        }
        else if (place == 4)
        {
            oreplace4 = "empty";
        }
        else if (place == 5)
        {
            oreplace5 = "empty";
        }
        else if (place == 6)
        {
            oreplace6 = "empty";
        }
    }
}
