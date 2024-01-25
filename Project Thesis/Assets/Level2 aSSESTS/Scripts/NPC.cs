using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    public GameObject Panel;
	public Text npctextbox;
	public string[] dialogue;
    private int index;
    public float wordSpeed;
    public bool playerIsClose;
	
	public GameObject Continue;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerIsClose){
			
			if(Panel.activeInHierarchy){
				zeroText();
			}
			else{
				Panel.SetActive(true);
				StartCoroutine(Typing());
			}
		}
		if(npctextbox.text == dialogue[index]){
			Continue.SetActive(true);
			}
    }
	
	public void NextLine(){
	
		Continue.SetActive(false);
	
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
	}
	
	IEnumerator Typing(){
		foreach(char letter in dialogue[index]. ToCharArray())
		{
			npctextbox.text += letter;
            yield return new WaitForSeconds(wordSpeed);
		}
	}
	
	private void OnTriggerEnter2D(Collider2D other){
		if (other. CompareTag( "Player")){
			
			playerIsClose = true;
		}
	}
	
	private void OnTriggerExit2D(Collider2D other){
		if (other. CompareTag( "Player")){
			
			playerIsClose = false;
			zeroText();
		}
	}
	
	
	
}
