using UnityEngine;
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

    public string ActiveNpcName
    {
        get { return npcNameText.text; }
        set { npcNameText.text = value; }
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
        if (conversation.Count == 0)
            SetVisible(true);
      
        conversation.Add(text);
        DisplayLine(convIndex);
    }

    private void DisplayLine(int index)
    {
        var conv = conversation[index];
        StartCoroutine(DisplayLetterByLetter(conv));
    }

    public void NextButtonClicked()
    {
        convIndex++;

        if (convIndex >= conversation.Count)
        {
            SetVisible(false);
            conversation.Clear();
            convIndex = 0;
            return;
        }

        DisplayLine(convIndex);
    }

    public void PrevButtonClicked()
    {
        convIndex--;

        if (convIndex < 0)
        {
            convIndex = 0;
            return;
        }

        DisplayLine(convIndex);
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
