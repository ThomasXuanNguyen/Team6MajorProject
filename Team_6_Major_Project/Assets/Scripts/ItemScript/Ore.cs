﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ore : MonoBehaviour
{
    public enum OreMaterial { iron, steel, bronze, coal };

    public OreMaterial material;

    public Smorge smorge;

    public float timeToDestroy;
    public float timeDecrease;

    public Material[] textures;


    // Start is called before the first frame update
    void Start()
    {
        TextureChange();
    }

    // Update is called once per frame
    void Update()
    {      
        if(material == OreMaterial.coal)
        {
            if(timeToDestroy > 0)
            {
                timeToDestroy -= timeDecrease * Time.deltaTime;
                if(timeToDestroy <= 0.01)
                {
                    Destroy(this.gameObject);
                }
            }
        }
    }
    
    void TextureChange()
    {
        if(material == OreMaterial.iron)
        {
            this.gameObject.GetComponent<MeshRenderer>().material = textures[0];
        }
        else if(material == OreMaterial.steel)
        {
            this.gameObject.GetComponent<MeshRenderer>().material = textures[1];
        }
        else if (material == OreMaterial.bronze)
        {
            this.gameObject.GetComponent<MeshRenderer>().material = textures[2];
        }
        else if (material == OreMaterial.coal)
        {
            this.gameObject.GetComponent<MeshRenderer>().material = textures[3];
        }
    }
}