using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cs1_controller : MonoBehaviour {

	private bool hasGun;
	public GameObject player;
	public GameObject canvas;
	public GameObject starterCamera;
	public GameObject playerCamera;

	// Use this for initialization
	void Start () {
		hasGun = false;
		player.SetActive (false);
		canvas.SetActive (true);
		starterCamera.SetActive (true);
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
		canvas.SetActive (false);
		starterCamera.SetActive (false);
	}
		
}
