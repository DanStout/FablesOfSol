﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Text;
using System.Threading;
using System.Collections.Generic;

public class Dialog : MonoBehaviour
{
    public float letterDelay = 0.001f;

    [SerializeField]
    private Text npcNameText;

    [SerializeField]
    private Text dialogText;

    private GameObject dialogChild;

    private List<string> conversation;
    private int convIndex;

    private Coroutine lastCoroutine;

    public string ActiveNpcName
    {
        get { return npcNameText.text; }
        set { npcNameText.text = value; }
    }

    public void CloseDialog()
    {
        SetVisible(false);
        conversation.Clear();
        convIndex = 0;
        StopCoroutine(lastCoroutine);
    }

    private void SetVisible(bool isVisible)
    {
        dialogChild.SetActive(isVisible);   
    }

    void Start()
    {
        conversation = new List<string>();
        dialogChild = transform.FindChild("DialogChild").gameObject;
        SetVisible(false);
    }

    public void AddLine(string text)
    {
        conversation.Add(text);

        if (conversation.Count == 1)
        {
            SetVisible(true);
            DisplayLine();
        }
      
    }

    private void DisplayLine()
    {
        var conv = conversation[convIndex];
        lastCoroutine = StartCoroutine(DisplayLetterByLetter(conv));
    }

    public void NextButtonClicked()
    {
        convIndex++;

        if (convIndex >= conversation.Count)
        {
            CloseDialog();
            return;
        }

        DisplayLine();
    }

    public void PrevButtonClicked()
    {
        convIndex--;

        if (convIndex < 0)
        {
            convIndex = 0;
            return;
        }

        DisplayLine();
    }

    private IEnumerator DisplayLetterByLetter(string text)
    {
        var builder = new StringBuilder();
        foreach (var letter in text)
        {
            builder.Append(letter);
            dialogText.text = builder.ToString();
            yield return new WaitForSeconds(letterDelay);
        }
    }

}
