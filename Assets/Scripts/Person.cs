using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person : MonoBehaviour {

	public bool triggersDialogue;
	public int myID;

	private bool hasOpenDialogue;
	private GameObject talkDialogue;
	private float range;

	void Start () {
		range = 5.0f;
		hasOpenDialogue = false;
		//Get the dialogue via the controller
		talkDialogue = GameObject.Find ("Controller").GetComponent<cs2_controller> ().talkDialogue;
	}
	
	// Update is called once per frame
	void Update () {
		//Check for "E TO TALK" Dialogue Triggering
		if (triggersDialogue) {
			if (Vector3.Distance (GameObject.Find ("FPSController").transform.position, gameObject.transform.position) <= range && !hasOpenDialogue) {
				talkDialogue.SetActive (true);
			} else if (talkDialogue.activeInHierarchy) {	//This means only one NPC can trigger dialogue
				talkDialogue.GetComponent<DialogueAnimator> ().Disappear ();
			}
		}
		//Open or close dialogue with E
		if (Input.GetKeyDown (KeyCode.E)) {
			if (!hasOpenDialogue) {
				if (Vector3.Distance (GameObject.Find ("FPSController").transform.position, gameObject.transform.position) <= range) {
					OpenDialogue ();
				}
			} else {
				CloseDialogue ();
			}
		}
		//Close dialogue with distance
		if(hasOpenDialogue){
			if (Vector3.Distance (GameObject.Find ("FPSController").transform.position, gameObject.transform.position) > range) {
				CloseDialogue ();
			}
		}
	}

	private void OpenDialogue(){
		hasOpenDialogue = true;
		if (talkDialogue.activeInHierarchy) {
			talkDialogue.GetComponent<DialogueAnimator> ().Disappear ();
		}
		Conversation c = GenerateConversation ();
		GameObject.Find ("Controller").GetComponent<ConversationManager> ().OpenConversation(c);
	}

	private void CloseDialogue(){
		hasOpenDialogue = false;
		if (talkDialogue.activeInHierarchy) {
			talkDialogue.GetComponent<DialogueAnimator> ().Disappear ();
		}
		GameObject.Find ("Controller").GetComponent<ConversationManager> ().CloseConversation();
	}

	private Conversation GenerateConversation(){
		if(myID == 1){
			return new Conversation ("Danny Sanchez", 2, true, "Hey kid. I'm going to say the something.", "DANNY NO!", ". fuck .");
		}

		//default case
		return new Conversation ("ERROR", 1, true, "Found no conversations for this person.", "Okay");
	}
}
