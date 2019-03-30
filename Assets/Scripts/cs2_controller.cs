using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class cs2_controller : MonoBehaviour {

	public GameObject player;
	private FirstPersonController characterControllerScript;
	public GameObject greekIntroDialogue;
	public GameObject sprintDialogue;
	public GameObject jumpDialogue;
	public GameObject talkDialogue;
	public GameObject eyeTop;
	public GameObject eyeBottom;
	public GameObject questBackground;
	public GameObject questTag;
	public GameObject questName;
	public GameObject questSubtitle;

	public AudioSource dannyDialogue;
	public AudioClip[] dannyDialogues;

	private bool hasShownSprintMsg;
	private bool hasShownJumpMsg;
	private bool hasShownEnergyMsg;
	private bool hasShownCombatMsg;

	public GameObject greek_helmet;
	public GameObject greek_shield;
	public GameObject greek_sword;
	public GameObject greek_ballista;

	void Start () {
		characterControllerScript = player.GetComponent<FirstPersonController> ();
		characterControllerScript.canLookAround = false;
		characterControllerScript.canWalk = false;

		hasShownSprintMsg = false;
		hasShownEnergyMsg = false;
		hasShownCombatMsg = false;
		hasShownJumpMsg = false;

		//Manage Quest Items
		GameObject.Find ("Gong").GetComponent<TimeObject> ().isInactive = true;
		GameObject.Find ("Pebbles").GetComponent<TimeObject> ().isInactive = true;

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
		if (!hasShownJumpMsg) {
			if (player.transform.position.x > 95) {
				if(sprintDialogue.activeInHierarchy){
					StartCoroutine (HideMessage (sprintDialogue, 0.1f));
				}
				dannyDialogue.clip = dannyDialogues [1];
				dannyDialogue.Play ();
				jumpDialogue.SetActive (true);
				hasShownJumpMsg = true;
				StartCoroutine (HideMessage (jumpDialogue, 4.0f));
			}
		}
		if (!hasShownEnergyMsg) {
			if (player.transform.position.z < 169) {
				dannyDialogue.clip = dannyDialogues [4];
				dannyDialogue.Play ();
				hasShownEnergyMsg = true;
			}
		}
		if (!hasShownCombatMsg) {
			if (player.transform.position.y > 29) {
				dannyDialogue.clip = dannyDialogues [5];
				dannyDialogue.Play ();
				hasShownCombatMsg = true;
			}
		}
		if (gameObject.GetComponent<QuestTracker>().quests.Count > 1) {
			if (gameObject.GetComponent<QuestTracker>().GetProgress (2) == 1) {
				if(!greek_helmet.activeInHierarchy && !greek_sword.activeInHierarchy && !greek_shield.activeInHierarchy && !greek_ballista.activeInHierarchy){
					gameObject.GetComponent<QuestTracker> ().AdvanceQuest (2);
				}
			}
		}
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
		greekIntroDialogue.SetActive (true);
		StartCoroutine (HideMessage (greekIntroDialogue, 5.0f));
	}

	IEnumerator HideMessage(GameObject go, float delay){
		yield return new WaitForSeconds (delay);
		go.GetComponent<DialogueAnimator> ().Disappear ();
		if (go.name == "SpaceBackground") {
			dannyDialogue.clip = dannyDialogues [2];
			dannyDialogue.Play ();
			StartCoroutine(PlayFindInventors (11.50f));
		}
	}

	IEnumerator PlayFindInventors(float delay){
		yield return new WaitForSeconds (delay);
		dannyDialogue.clip = dannyDialogues [3];
		dannyDialogue.Play ();
	}
}
