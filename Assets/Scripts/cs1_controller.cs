using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class cs1_controller : MonoBehaviour {

	private bool hasGun;
	public GameObject danny;
	public AudioClip[] audioclips;
	public GameObject player;
	public GameObject titleBackground;
	public GameObject mouseMoveDialogue;
	public GameObject playerMoveDialogue;
	public GameObject gunPickupDialogue;
	public GameObject starterCamera;
	public GameObject playerCamera;
	public GameObject timeGun;

	private FirstPersonController characterControllerScript;
	private bool hasStartedDannyIntroCoroutine;
	private bool hasStartedPickupCoroutine;

	// Use this for initialization
	void Start () {
		hasGun = false;
		player.SetActive (false);
		titleBackground.SetActive (true);
		mouseMoveDialogue.SetActive (false);
		playerMoveDialogue.SetActive (false);
		gunPickupDialogue.SetActive (false);
		starterCamera.SetActive (true);
		characterControllerScript = player.GetComponent<FirstPersonController> ();

		hasStartedDannyIntroCoroutine = false;
		hasStartedPickupCoroutine = false;
	}
	
	// Update is called once per frame
	void Update () {
		//For testing, remove in build
		if(Input.GetKeyDown(KeyCode.Q)){
			Debug.Log ("Campos" + playerCamera.transform.position.x + " y : " + playerCamera.transform.position.y + " z : " + playerCamera.transform.position.z);
		}

		//check for viewing danny
		if(mouseMoveDialogue.activeInHierarchy && !hasStartedDannyIntroCoroutine){
			if (danny.GetComponent<MeshRenderer> ().isVisible) {
				StartCoroutine (DannyIntro (2.0f));
				hasStartedDannyIntroCoroutine = true;
			}
		}
		//check for close to gun
		if(playerMoveDialogue.activeInHierarchy && !hasStartedPickupCoroutine){
			if (Vector3.Distance(player.transform.position, timeGun.transform.position) < 1.0f) {
				StartCoroutine (PickupTutorial (0.5f));
				hasStartedPickupCoroutine = true;
			}
		}
	}

	public void BeginFade(Animator canvasAnimator){
		canvasAnimator.Play ("FadeOut");
		StartCoroutine (StartGame (0.15f));
	}

	IEnumerator StartGame(float delay){
		yield return new WaitForSeconds (delay);
		player.SetActive (true);
		titleBackground.SetActive (false);
		starterCamera.SetActive (false);
		yield return new WaitForSeconds (1.0f);
		danny.GetComponent<DannySoundController> ().PlaySound (audioclips[0]);
		yield return new WaitForSeconds (2.406f);
		mouseMoveDialogue.SetActive (true);
		characterControllerScript.canLookAround = true;
	}

	IEnumerator DannyIntro(float delay){
		yield return new WaitForSeconds (delay);
		mouseMoveDialogue.GetComponent<DialogueAnimator> ().Disappear ();
		characterControllerScript.ForceLookAt (danny.transform);
		danny.GetComponent<DannySoundController> ().PlaySound (audioclips[1]);
		yield return new WaitForSeconds (10.467f);
		characterControllerScript.canLookAround = true;
		characterControllerScript.canWalk = true;
		playerMoveDialogue.SetActive (true);
	}

	IEnumerator PickupTutorial(float delay){
		playerMoveDialogue.GetComponent<DialogueAnimator> ().Disappear ();
		yield return new WaitForSeconds (delay);
		gunPickupDialogue.SetActive (true);
	}

		
}
