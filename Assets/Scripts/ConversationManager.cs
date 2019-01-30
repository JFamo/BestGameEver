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
	private ConversationSet currentConversationSet;
	public Animator myAnimator;

	private int selectedOption;
	private float prevScroll;

	void Start () {
		if (panel.activeInHierarchy) {
			panel.SetActive (false);
		}
		isShowing = false;
		prevScroll = 0.0f;
	}

	void Update(){
		//Check for changing dialogue option
		if (isShowing) {
			if (Input.GetAxis ("Mouse ScrollWheel") != prevScroll) {
				prevScroll = Input.GetAxis ("Mouse ScrollWheel");
		
				//check change of index
				if (prevScroll > 0) {
					if (selectedOption > 0) {
						selectedOption--;
					}
				} else if (prevScroll < 0) {
					if (selectedOption < currentConversation.numberOfOptions - 1) {
						selectedOption++;
					}
				}

				//update arrow
				ChangeSelected();
			}
		}

		//Check for choosing dialogue option
		if (Input.GetKeyDown (KeyCode.Return)) {
			Conversation newc = currentConversationSet.GetNext (selectedOption);
			if (newc != null) {
				LoadConversation (newc);
			} else {
				CloseConversation ();
			}
		}
	}

	public void OpenConversation(ConversationSet cset){
		//Adjust my variables
		isShowing = true;
		currentConversationSet = cset;

		//Load First Conversation
		LoadConversation(cset.GetFirst());

		//Activate Panel
		panel.SetActive(true);
		myAnimator.Play ("OpenConvs");
	}

	public void LoadConversation(Conversation convs){
		//Adjust my variables
		this.currentConversation = convs;
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
		optionSelector.GetComponent<RectTransform>().anchoredPosition = new Vector2(-113.95f, -267.2f);
	}

	public void CloseConversation(){
		isShowing = false;
		myAnimator.Play ("CloseConvs");
		StartCoroutine (FinishClose (2.0f));
	}

	public void ChangeSelected(){
		if (selectedOption == 0) {
			optionSelector.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (-113.95f, -267.2f);
		} else if (selectedOption == 1) {
			optionSelector.GetComponent<RectTransform>().anchoredPosition = new Vector2(-113.95f, -307.2f);
		} else if (selectedOption == 2) {
			optionSelector.GetComponent<RectTransform>().anchoredPosition = new Vector2(-113.95f, -347.2f);
		}
	}

	IEnumerator FinishClose(float delay){
		yield return new WaitForSeconds (delay);
		panel.SetActive(false);
	}
}
