﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDialogueTrigger : MonoBehaviour
{
    public bool inRange = false;
    public bool inDialogue = false;
    public bool dialogueDoneforDay = false;
    public bool dialogueStart = false;
    public ItemSlot[] slots;
    public ItemSlot SlotNumber;
    public Dialogue dialogue;
    public DialogueManager dialogueManager;
    public CustomerAI customerAI;
    public int CustomerNumber;
    public int gold;
    public int randomtype;
    public int randomblademat;
    public int randomguardmat;
    public int randomhandlemat;
    public PlayerStats playerStats;
    public float dist;
    public float delay = 2f;


    // Start is called before the first frame update
    void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
        customerAI =this.gameObject.GetComponent<CustomerAI>();
        playerStats = FindObjectOfType<PlayerStats>();
        slots = FindObjectsOfType<ItemSlot>();
        randomtype = Random.Range(0, 3);
        randomblademat = Random.Range(0, 3);
        randomguardmat = Random.Range(0, 3);
        randomhandlemat = Random.Range(0, 3);
        dialogue.bladeType = (Sword.SwordType)randomtype;
        dialogue.bladeMaterial = (Sword.MaterialBlade)randomblademat;
        dialogue.guardMaterial = (Sword.MaterialGuard)randomguardmat;
        dialogue.handleMaterial = (Sword.MaterialHandle)randomhandlemat;
        dialogue.sentences[1] = "I would like to order a " + dialogue.bladeType.ToString() + " " + dialogue.bladeMaterial.ToString() + " blade with " 
            + dialogue.guardMaterial.ToString() + " guard " + dialogue.handleMaterial.ToString() + " handle";
    }

    // Update is called once per frame
    void Update()
    {
        dist = Vector3.Distance(gameObject.transform.position, playerStats.gameObject.transform.position);
        if(dist <= 7)
        {
            inRange = true;
        }
        else
        {
            inRange = false;
        }

        if(inRange == true)
        {
            if (dialogueDoneforDay == false)
            {
                if (Input.GetKeyDown(KeyCode.Space) && dialogueStart == false)
                {
                    TriggerDialogue();                   
                    CustomerNumber = playerStats.CustomerOrderNumber;
                    playerStats.CustomerOrderNumber++;
                    inDialogue = dialogueManager.inChat;
                    dialogueStart = true;
                }
                else if (Input.GetKeyDown(KeyCode.Space))
                {
                    dialogueManager.DisplayNextSentence();
                    inDialogue = dialogueManager.inChat;
                    if (inDialogue == false)
                    {
                        WaypointUpdate();
                        customerAI.setSlotWayPoint();
                        dialogueDoneforDay = true;
                        dialogueStart = false;
                    }
                }
            }
            else
            {
                return;
            }
        }
    }

    public void SetItemSlot()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (CustomerNumber == slots[i].slotNumber)
            {
                SlotNumber = slots[i];
                CheckItem();
            }
        }
    }

    public void CheckItem()
    {
        if(SlotNumber.bladeType == dialogue.bladeType)
        {
            if(SlotNumber.bladeMaterial == dialogue.bladeMaterial)
            {
                if(SlotNumber.guardMaterial == dialogue.guardMaterial)
                {
                    if(SlotNumber.handleMaterial == dialogue.handleMaterial)
                    {
                        playerStats.gold += gold;
                        customerAI.waypointIndex++;
                        Destroy(SlotNumber.sword);
                    }
                    else
                    {
                        SlotNumber.sword.transform.position = SlotNumber.badlocation.position;
                    }
                }
                else
                {
                    SlotNumber.sword.transform.position = SlotNumber.badlocation.position;
                }
            }
            else
            {
                SlotNumber.sword.transform.position = SlotNumber.badlocation.position;
            }
        }
        else
        {
            SlotNumber.sword.transform.position = SlotNumber.badlocation.position;
        }
    }

    public void TriggerDialogue()
    {
         dialogueManager.StartDialogue(dialogue);
    }

    public void WaypointUpdate()
    {
        if (customerAI.waypointIndex == 3)
        {
            CheckItem();
        }
        else
        {
            customerAI.waypointIndex++;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Iron Sword")
        {
            Debug.Log(other.ToString());
            if(other.gameObject.GetComponent<Sword>().swordType == dialogue.bladeType)
            {
                if(other.gameObject.GetComponent<Sword>().materialBlade == dialogue.bladeMaterial)
                {
                    if (other.gameObject.GetComponent<Sword>().materialGuard == dialogue.guardMaterial)
                    {
                        if (other.gameObject.GetComponent<Sword>().materialHandle == dialogue.handleMaterial)
                        {
                            playerStats.gold += gold;
                            Destroy(other.gameObject);
                            WaypointUpdate();
                        }
                    }
                }
            }
        }
    }
}
