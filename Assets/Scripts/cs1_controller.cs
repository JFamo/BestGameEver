using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class cs1_controller : MonoBehaviour {

	//references
	public GameObject danny;
	public AudioClip[] audioclips;
	public GameObject player;
	public GameObject starterCamera;
	public GameObject playerCamera;
	public GameObject timeGun;
	public GameObject playerGun;

	//dialogues
	public GameObject titleBackground;
	public GameObject mouseMoveDialogue;
	public GameObject playerMoveDialogue;
	public GameObject gunPickupDialogue;

	private FirstPersonController characterControllerScript;

	//progress tracking bools
	private bool hasGun;
	private bool hasStartedDannyIntroCoroutine;
	private bool hasStartedPickupCoroutine;

	//actual vars
	private float pickupDistance;

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

		pickupDistance = 3.0f;
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
			if (Vector3.Distance(player.transform.position, timeGun.transform.position) < pickupDistance) {
				StartCoroutine (PickupTutorial (0.5f));
				hasStartedPickupCoroutine = true;
			}
		}
		//Manage gun pickup dialogue appearing and disappearing with distance
		if(timeGun.activeInHierarchy && hasStartedPickupCoroutine && Vector3.Distance(player.transform.position, timeGun.transform.position) < pickupDistance && !gunPickupDialogue.activeInHierarchy){
			gunPickupDialogue.SetActive (true);
		}
		else if(timeGun.activeInHierarchy && hasStartedPickupCoroutine && Vector3.Distance(player.transform.position, timeGun.transform.position) > pickupDistance && gunPickupDialogue.activeInHierarchy){
			gunPickupDialogue.GetComponent<DialogueAnimator> ().Disappear ();
		}
		//check for gun pickup
		if (Input.GetKeyDown (KeyCode.E)) {
			if(timeGun.activeInHierarchy && hasStartedPickupCoroutine && Vector3.Distance(player.transform.position, timeGun.transform.position) < pickupDistance && gunPickupDialogue.activeInHierarchy){
				hasGun = true;
				timeGun.SetActive (false);
				playerGun.SetActive (true);
				StartCoroutine (SuckTutorial (0.5f));
			}
		}
	}

	public void BeginFade(Animator canvasAnimator){
		canvasAnimator.Play ("FadeOut");
		StartCoroutine (StartGame (0.15f));
	}

	//from the time the title fades to the time the player is able to move the camera
	IEnumerator StartGame(float delay){
		yield return new WaitForSeconds (delay);
		player.SetActive (true);
		playerGun.SetActive (false);
		titleBackground.SetActive (false);
		starterCamera.SetActive (false);
		yield return new WaitForSeconds (1.0f);
		danny.GetComponent<DannySoundController> ().PlaySound (audioclips[0]);
		yield return new WaitForSeconds (2.406f);
		mouseMoveDialogue.SetActive (true);
		characterControllerScript.canLookAround = true;
	}

	//from the time the player looks at danny to the time they are able to move around
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

	//from the time the player approaches to gun until they pick it up
	IEnumerator PickupTutorial(float delay){
		playerMoveDialogue.GetComponent<DialogueAnimator> ().Disappear ();
		yield return new WaitForSeconds (delay);
		gunPickupDialogue.SetActive (true);
	}
	//from the time the player picks up the gun until computer succ
	IEnumerator SuckTutorial(float delay){
		gunPickupDialogue.GetComponent<DialogueAnimator> ().Disappear ();
		yield return new WaitForSeconds (delay);
		characterControllerScript.ForceLookAt (danny.transform);
		danny.GetComponent<DannySoundController> ().PlaySound (audioclips[1]);
		yield return new WaitForSeconds (10.467f);
		characterControllerScript.canLookAround = true;
		characterControllerScript.canWalk = true;
	}
		
}
