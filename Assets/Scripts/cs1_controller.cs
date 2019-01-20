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
	public GameObject starterCamera;
	public GameObject playerCamera;

	private FirstPersonController characterControllerScript;

	// Use this for initialization
	void Start () {
		hasGun = false;
		player.SetActive (false);
		titleBackground.SetActive (true);
		mouseMoveDialogue.SetActive (false);
		starterCamera.SetActive (true);
		characterControllerScript = player.GetComponent<FirstPersonController> ();
	}
	
	// Update is called once per frame
	void Update () {
		//For testing, remove in build
		if(Input.GetKeyDown(KeyCode.Q)){
			Debug.Log ("Campos" + playerCamera.transform.position.x + " y : " + playerCamera.transform.position.y + " z : " + playerCamera.transform.position.z);
		}
	}

	public void BeginFade(Animator canvasAnimator){
		canvasAnimator.Play ("Fade");
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
		
}
