using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class game1 : MonoBehaviour
{
    public GameObject Panel;
	public Text npctextbox;
	public string[] dialogue;
    private int index;
    public float wordSpeed;
	public GameObject Continue;

	[SerializeField] GameObject GameController;

    // Update is called once per frame


	void Start()
    {
 

        Panel.SetActive(true);
		StartCoroutine(Typing());
			
                
    }
    void Update()
    {
		if (Input.GetKeyDown(KeyCode.Space))
        {
         // Call the NextLine function when spacebar is pressed
            NextLine();
        }
        
    }
	
	public void NextLine(){
	
	
		if(index< dialogue.Length -1)
		{
			index++;
			npctextbox.text ="";
			StartCoroutine(Typing());
		}
		else{
			zeroText();
		}
	}
	
	public void zeroText(){
		npctextbox.text ="";
		index = 0;
		Panel.SetActive(false);
		Continue.SetActive(false);
		GameController.SetActive(true);
	}
	
	IEnumerator Typing(){
		foreach(char letter in dialogue[index]. ToCharArray())
		{
			npctextbox.text += letter;
            yield return new WaitForSeconds(wordSpeed);
		}
	}
	

	
	
	
}
