using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person : MonoBehaviour {

	public bool triggersDialogue;

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
		if (triggersDialogue) {
			if (Vector3.Distance (GameObject.Find ("FPSController").transform.position, gameObject.transform.position) <= range && !hasOpenDialogue) {
				talkDialogue.SetActive (true);
			} else if (talkDialogue.activeInHierarchy) {	//This means only one NPC can trigger dialogue
				talkDialogue.GetComponent<DialogueAnimator> ().Disappear ();
			}
		}
		if (Input.GetKeyDown (KeyCode.E)) {
			if (!hasOpenDialogue) {
				if (Vector3.Distance (GameObject.Find ("FPSController").transform.position, gameObject.transform.position) <= range) {
					OpenDialogue ();
				}
			} else {
				CloseDialogue ();
			}
		}
	}

	private void OpenDialogue(){
		if (talkDialogue.activeInHierarchy) {
			talkDialogue.GetComponent<DialogueAnimator> ().Disappear ();
		}
	}

	private void CloseDialogue(){
		if (talkDialogue.activeInHierarchy) {
			talkDialogue.GetComponent<DialogueAnimator> ().Disappear ();
		}
	}
}
