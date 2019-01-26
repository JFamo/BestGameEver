using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class cs1_controller : MonoBehaviour {

	//references
	public GameObject danny;
	private GameObject clawbot;
	public AudioClip[] audioclips;
	public GameObject player;
	public GameObject starterCamera;
	public GameObject timeGun;
	public GameObject playerGun;

	//dialogues
	public GameObject titleBackground;
	public GameObject mouseMoveDialogue;
	public GameObject playerMoveDialogue;
	public GameObject gunPickupDialogue;
	public GameObject lmbDialogue;
	public GameObject rmbDialogue;
	public GameObject scrollDialogue;
	public GameObject inventoryInterface;

	private FirstPersonController characterControllerScript;

	//progress tracking bools
	private bool hasGun;
	private bool hasStartedDannyIntroCoroutine;
	private bool hasStartedPickupCoroutine;
	private bool hasStartedSuccCoroutine;

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
		lmbDialogue.SetActive (false);
		rmbDialogue.SetActive (false);
		scrollDialogue.SetActive (false);
		starterCamera.SetActive (true);
		characterControllerScript = player.GetComponent<FirstPersonController> ();
		clawbot = GameObject.Find ("Clawbot");

		characterControllerScript.canLookAround = false;
		characterControllerScript.canWalk = false;

		hasStartedDannyIntroCoroutine = false;
		hasStartedPickupCoroutine = false;
		hasStartedSuccCoroutine = false;

		pickupDistance = 3.0f;
	}
	
	// Update is called once per frame
	void Update () {

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
		//check for clawbot pickup
		if (lmbDialogue.activeInHierarchy && !clawbot.activeInHierarchy) {
			StartCoroutine (PlaceTutorial (0.5f));
		}
		//check for inventory opening
		if (scrollDialogue.activeInHierarchy && inventoryInterface.activeInHierarchy) {
			StartCoroutine (DinosaurTutorial(0.5f));
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

	//from the time the player picks up the gun until clawbot succ
	IEnumerator SuckTutorial(float delay){
		gunPickupDialogue.GetComponent<DialogueAnimator> ().Disappear ();
		yield return new WaitForSeconds (delay);
		characterControllerScript.ForceLookAt (danny.transform);
		danny.GetComponent<DannySoundController> ().PlaySound (audioclips[2]);
		yield return new WaitForSeconds (5.500f);
		characterControllerScript.ForceLookAt (clawbot.transform);
		yield return new WaitForSeconds (2.789f);
		characterControllerScript.canLookAround = true;
		characterControllerScript.canWalk = true;
		lmbDialogue.SetActive (true);
	}

	//from the time the player picks up the clawbot until they select the dinosaur
	IEnumerator PlaceTutorial(float delay){
		lmbDialogue.GetComponent<DialogueAnimator> ().Disappear ();
		yield return new WaitForSeconds (delay);
		characterControllerScript.ForceLookAt (danny.transform);
		danny.GetComponent<DannySoundController> ().PlaySound (audioclips[3]);
		yield return new WaitForSeconds (7.367f);
		danny.GetComponent<DannySoundController> ().PlaySound (audioclips[4]);
		yield return new WaitForSeconds (9.474f);
		danny.GetComponent<DannySoundController> ().PlaySound (audioclips[5]);
		yield return new WaitForSeconds (15.504f);
		characterControllerScript.canLookAround = true;
		characterControllerScript.canWalk = true;
		scrollDialogue.SetActive (true);
	}

	//from the time the player opens their inventory until they place the dinosaur
	IEnumerator DinosaurTutorial(float delay){
		scrollDialogue.GetComponent<DialogueAnimator> ().Disappear ();
		yield return new WaitForSeconds (delay);
		rmbDialogue.SetActive (true);
	}

	//from the time the player places the dinosaur to the time they succ it up
	IEnumerator AttackTutorial(float delay){
		rmbDialogue.GetComponent<DialogueAnimator> ().Disappear ();
		yield return new WaitForSeconds (delay);
		characterControllerScript.ForceLookAt (danny.transform);
		danny.GetComponent<DannySoundController> ().PlaySound (audioclips[6]);
		yield return new WaitForSeconds (5.093f);
		characterControllerScript.canLookAround = true;
		characterControllerScript.canWalk = true;
	}
		
}
