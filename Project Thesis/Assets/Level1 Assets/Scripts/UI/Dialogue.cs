using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public string[] dialogue;
    private int index;
    public float textSpeed;

    void Start()
    {
        dialogueText.text = string.Empty;
        StartDialogue();
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(dialogueText.text == dialogue[index])
            {
                NextDialogue(); 
            }
            else
            {
                StopAllCoroutines();
                dialogueText.text = dialogue[index];
            }
        }
    }

    public void OpenDialogue()
    {
        dialoguePanel.SetActive(true);
    }

     public void CloseDialogue()
    {
        dialoguePanel.SetActive(false);
    }

    void StartDialogue()
    {
        index = 0;
        StartCoroutine(Typing());
    }

    IEnumerator Typing()
    {
        foreach(char letter in dialogue[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextDialogue()
    {
        if(index < dialogue.Length - 1)
        {
            index++;
            dialogueText.text = string.Empty;
            StartCoroutine(Typing());
        }
        else
        {
            CloseDialogue();
        }
    }
}
