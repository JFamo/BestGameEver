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
		if(Input.GetKeyDown(KeyCode.E)){
			Debug.Log ("Campos" + playerCamera.transform.position.x + " y : " + playerCamera.transform.position.y + " z : " + playerCamera.transform.position.z);
		}
	}

	public void BeginFade(){

	}

	public void StartGame(){
		player.SetActive (true);
		canvas.SetActive (false);
		starterCamera.SetActive (false);
	}
		
}
