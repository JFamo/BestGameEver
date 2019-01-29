using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class cs2_controller : MonoBehaviour {

	public GameObject player;
	private FirstPersonController characterControllerScript;
	public GameObject greekIntroDialogue;
	public GameObject sprintDialogue;
	public GameObject eyeTop;
	public GameObject eyeBottom;

	private bool hasShownSprintMsg;

	void Start () {
		characterControllerScript = player.GetComponent<FirstPersonController> ();
		characterControllerScript.canLookAround = false;
		characterControllerScript.canWalk = false;

		hasShownSprintMsg = false;

		StartCoroutine(StartGame (2.30f));
	}

	void Update(){
		if (!hasShownSprintMsg) {
			if (player.transform.position.x > 60) {
				if(greekIntroDialogue.activeInHierarchy){
					StartCoroutine (HideMessage (greekIntroDialogue, 0.1f));
				}
				sprintDialogue.SetActive (true);
				hasShownSprintMsg = true;
				StartCoroutine (HideMessage (sprintDialogue, 4.0f));
			}
		}
	}

	//from the time the level loads to the time the player is given control
	IEnumerator StartGame(float delay){
		yield return new WaitForSeconds (delay);
		characterControllerScript.canLookAround = true;
		characterControllerScript.canWalk = true;
		eyeTop.SetActive (false);
		eyeBottom.SetActive (false);
		greekIntroDialogue.SetActive (true);
		StartCoroutine (HideMessage (greekIntroDialogue, 5.0f));
	}

	IEnumerator HideMessage(GameObject go, float delay){
		yield return new WaitForSeconds (delay);
		go.GetComponent<DialogueAnimator> ().Disappear ();
	}
}
