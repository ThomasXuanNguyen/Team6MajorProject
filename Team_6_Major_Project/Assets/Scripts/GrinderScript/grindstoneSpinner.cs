﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grindstoneSpinner : MonoBehaviour
{
    float degrees;
    public float speed;
    public GameObject Grindstone;
   
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        Grindstone.transform.Rotate(speed * Time.deltaTime, 0, 0);
        
    }

    private void OnMouseOver()
    {
        

    }

}
