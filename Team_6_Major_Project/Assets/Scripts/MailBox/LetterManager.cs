﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LetterManager : MonoBehaviour
{
    public Text[] dialogueText;

    public List<string> sentences;

    public Animator animator;

    public int index;

    public bool inChat = false;

    public int special1TextFile;

    public int special2TextFile;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void StartLetter(Letter letter)
    {
        animator.SetBool("IsOpen", true);
        inChat = true;
        
        for(int i = 0; i < letter.sentences.Length; i++)
        {
            sentences.Add(letter.sentences[i]);
        }

        for (int i = 0; i < sentences.Count; i++)
        {
            dialogueText[i].text = sentences[i];
        }

        inChat = false;

    }

    public void OpenLetter()
    {
        animator.SetBool("IsOpen", true);

    }

    public void EndLetter()
    {
        animator.SetBool("IsOpen", false);
       
    }

    public void FinishLetter()
    {
        for (int i = 0; i < sentences.Count; i++)
        {
            dialogueText[i].text = "";
        }
    }

}