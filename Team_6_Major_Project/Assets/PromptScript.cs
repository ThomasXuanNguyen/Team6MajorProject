﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PromptScript : MonoBehaviour
{
    public GameObject player;

    public GameObject prompt;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        prompt.transform.LookAt(player.transform);

    }

    private void OnMouseOver()
    {
        float dist = Vector3.Distance(this.gameObject.transform.position, player.transform.position);
        if (dist < 3)
        {
            prompt.SetActive(true);
            prompt.transform.localScale = new Vector3(0.0003f * dist, 0.0003f * dist, 0.0003f * dist);
        }
        else prompt.SetActive(false);
    }
    private void OnMouseExit()
    {
        prompt.SetActive(false);

    }

}