using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConversationManager : MonoBehaviour {

	public bool isShowing;

	public GameObject panel;
	public GameObject name;
	public GameObject text;
	public GameObject option1;
	public GameObject option2;
	public GameObject option3;
	public GameObject optionSelector;
	public GameObject tutorial;

	private Conversation currentConversation;

	private int selectedOption;

	void Start () {
		isShowing = false;
	}

	public void OpenConversation(Conversation convs){
		//Adjust my variables
		this.currentConversation = convs;
		isShowing = true;
		selectedOption = 0;

		//Set values
		name.GetComponent<Text>().text = currentConversation.name;
		text.GetComponent<Text>().text = currentConversation.text;
		if (currentConversation.includesTutorialText) {
			tutorial.SetActive (true);
		} else {
			tutorial.SetActive (false);
		}
		option1.GetComponent<Text>().text = currentConversation.options[0];
		if (currentConversation.numberOfOptions > 1) {
			option2.GetComponent<Text>().text = currentConversation.options[1];
			option2.SetActive (true);
		} else {
			option2.SetActive (false);
		}
		if (currentConversation.numberOfOptions > 2) {
			option3.GetComponent<Text> ().text = currentConversation.options [2];
			option3.SetActive (true);
		} else {
			option3.SetActive (false);
		}

		//Reset selector
		optionSelector.transform.position = new Vector3(optionSelector.transform.position.x, -267.2f, optionSelector.transform.position.z);

		//Activate Panel
		panel.SetActive(true);
	}

	public void CloseConversation(){
		isShowing = false;
		panel.SetActive(false);
	}
}
