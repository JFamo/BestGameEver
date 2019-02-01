using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class cs3_controller : MonoBehaviour {

	public GameObject player;
	private FirstPersonController characterControllerScript;
	public GameObject clevelandIntroDialogue;
	public GameObject eyeTop;
	public GameObject eyeBottom;
	public GameObject questBackground;
	public GameObject questTag;
	public GameObject questName;
	public GameObject questSubtitle;

	public AudioSource dannyDialogue;
	public AudioClip[] dannyDialogues;

	void Start () {
		characterControllerScript = player.GetComponent<FirstPersonController> ();
		characterControllerScript.canLookAround = false;
		characterControllerScript.canWalk = false;

		clevelandIntroDialogue.SetActive (false);

		StartCoroutine(StartGame (2.30f));
	}

	void Update(){
		
	}

	public void HideQuestMessage(){
		StartCoroutine (HideMessage (questBackground, 3.0f));
	}

	//from the time the level loads to the time the player is given control
	IEnumerator StartGame(float delay){
		yield return new WaitForSeconds (delay);
		dannyDialogue.clip = dannyDialogues [0];
		dannyDialogue.Play ();
		characterControllerScript.canLookAround = true;
		characterControllerScript.canWalk = true;
		eyeTop.SetActive (false);
		eyeBottom.SetActive (false);
		clevelandIntroDialogue.SetActive (true);
		StartCoroutine (HideMessage (clevelandIntroDialogue, 5.0f));
	}

	IEnumerator HideMessage(GameObject go, float delay){
		yield return new WaitForSeconds (delay);
		go.GetComponent<DialogueAnimator> ().Disappear ();
	}
}
