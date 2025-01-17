﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderMiniGame : MonoBehaviour
{
    //[SerializeField]
    //private Text text;
    [SerializeField]
    private int maxrepeat = 5;
    [SerializeField]
    private int badQuality = 0;
    [SerializeField]
    private int goodQuality = 1;
    [SerializeField]
    private int greatQuality = 2;
    [SerializeField]
    private int perfectQuality = 3;
    [SerializeField]
    public int totalQuality = 0;
    [SerializeField]
    private Slider slider;
    [SerializeField]
    private float speed = 1.0f;
    [SerializeField]
    private float badRange = 40;
    [SerializeField]
    private float goodRange = 30;
    [SerializeField]
    private float greatRange = 20;
    [SerializeField]
    private float perfectRange = 10;
    [SerializeField]
    private bool forward = true;
    [SerializeField]
    private bool stopped = false;
    [SerializeField]
    private Anvil anvil;
    [SerializeField]
    private Grinder grinder;
    [SerializeField]
    private GameObject hammer;

    public int lastDinkAmount;

    public bool inUseGrinder = false;
    public bool inUseAnvil = false;
    public bool handle = false;
    public bool guard = false;

    public int repeat;

    public AudioClip[] hammerDink;
    public AudioSource dinkSource;
    // Start is called before the first frame update
    private void Start()
    {
        anvil = GameObject.Find("Anvil").GetComponentInChildren<Anvil>();
        SliderMovement();
    }

    // Update is called once per frame
    private void Update()
    {
        SliderDirection();
        SliderMovement();
        Rating();
        Continue();
    }
    //Functions which moves the indicator left and right
    private void SliderMovement()
    {
        if (stopped == false)
        {
            if (forward == true)
            {
                slider.value += speed;
            }
            else
            {
                slider.value -= speed;
            }
        }
        else
        {
            if (forward == true)
            {
                slider.value += speed;
            }
            else
            {
                slider.value -= speed;
            }
        }
    }
    //Functions which determines the direction of the indicator
    private void SliderDirection()
    {
        if(slider.value == slider.minValue)
        {
            forward = true;
        }
        else if( slider.value == slider.maxValue)
        {
            forward = false;
        }
                            
    }
    //Functions which runs after the hammering is done and gets the quality for the object on the anvil after hammering
    private void Rating()
    {
        if (repeat == maxrepeat)
        {
            anvil.selected = false;
            if (inUseAnvil == true)
            {
                //hammer.transform.position = new Vector3(hammer.transform.position.x - 0.25f, hammer.transform.position.y, hammer.transform.position.z);

                anvil.Quality = totalQuality;
                anvil.anvilIngot();
                anvil.anvilSheet();
                totalQuality = 0;
                this.gameObject.SetActive(false);
                inUseAnvil = false;
                return;
            }
            else if(inUseGrinder == true)
            {
                grinder.Quality = totalQuality;
                if(handle == true)
                {
                    grinder.GrinderHandle();
                    handle = false;
                }
                else if(guard == true)
                {
                    grinder.GrinderHandle();
                    guard = false;
                }
                totalQuality = 0;
                this.gameObject.SetActive(false);
                inUseGrinder = false;
                return;
                //hammer.transform.position = new Vector3(hammer.transform.position.x + 0.05f, hammer.transform.position.y, hammer.transform.position.z);
            }
        }
        else if (Input.GetMouseButtonDown(0))
        {
            hammer.GetComponent<Animator>().Play("hammerDink", -1, 0);
            lastDinkAmount = (int)slider.value;
            StartCoroutine("DinkHammer");
            if (repeat < maxrepeat + 1)
            {
                stopped = true;
                repeat++;
                if (slider.value <= badRange / 2)
                {
                    //text.text = "Bad";
                    totalQuality += badQuality;
                }
                else if (slider.value > badRange / 2 && slider.value <= ((badRange / 2) + (goodRange / 2)))
                {
                    //text.text = "Good";
                    totalQuality += goodQuality;
                }
                else if (slider.value > ((badRange / 2) + (goodRange / 2)) && slider.value <= ((badRange / 2) + (goodRange / 2) + (greatRange / 2)))
                {
                    //text.text = "Great";
                    totalQuality += greatQuality;
                }
                else if (slider.value > ((badRange / 2) + (goodRange / 2) + (greatRange / 2)) && slider.value <= ((badRange / 2) + (goodRange / 2) + (greatRange / 2) + perfectRange))
                {
                    //text.text = "Perfect";
                    totalQuality += perfectQuality;
                }
                else if (slider.value > (slider.maxValue - (badRange / 2) - (goodRange / 2) - (greatRange / 2)) && slider.value <= (slider.maxValue - (badRange / 2) - (goodRange / 2)))
                {
                    //text.text = "Great";
                    totalQuality += greatQuality;
                }
                else if (slider.value > (slider.maxValue - (badRange / 2) - (goodRange / 2)) && slider.value <= slider.maxValue - (badRange / 2))
                {
                    //text.text = "Good";
                    totalQuality += goodQuality;
                }
                else if (slider.value > slider.maxValue - (badRange / 2) && slider.value <= slider.maxValue)
                {
                    //text.text = "Bad";
                    totalQuality += badQuality;
                }
                
                if(repeat < maxrepeat)
                {
                    anvil.Invoke("AddCritPoint", 0.5f);
                }
                //hammer.transform.position = new Vector3(hammer.transform.position.x + 0.05f, hammer.transform.position.y, hammer.transform.position.z);
            }
        }
    }
    //Ienmerator which dinks the hammer
    IEnumerator DinkHammer()
    {
        yield return new WaitForSeconds(0.15f);
    }
    //Continues the movements of the indicator
    private void Continue()
    {
        if (Input.GetMouseButtonUp(0))
        {
            stopped = false;
            slider.value = 0;
        }
    }
}
