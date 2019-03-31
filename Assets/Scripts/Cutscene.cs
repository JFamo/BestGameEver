using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene : MonoBehaviour {

	public static int scene = 0;
	public static int returnLevel = 0;
	public float length;
	public int myId;

	// Update is called once per frame
	void Start () {
		if(scene != myId){
			this.gameObject.SetActive (false);
		}
		Renderer r = GetComponent<Renderer>();
		MovieTexture movie = (MovieTexture)r.material.mainTexture;
		movie.Play();
		GetComponent<AudioSource> ().Play ();
		StartCoroutine(GotoLobby(length));
	}

	IEnumerator GotoLobby(float l)
	{
		yield return new WaitForSeconds(l);
		if (myId == 1) {
			scenecontroller.LoadScene ("Greece");
			returnLevel = 1;
		}
		if (myId == 2) {
			scenecontroller.LoadScene ("Cleveland");
			returnLevel = 2;
		}
		if (myId == 3) {
			scenecontroller.LoadScene ("Classroom");
			returnLevel = 3;
		}
		if (myId == 4) {
			if (returnLevel == 1) {
				scenecontroller.LoadScene ("Greece");
			}
			if (returnLevel == 2) {
				scenecontroller.LoadScene ("Cleveland");
			}
			if (returnLevel == 3) {
				scenecontroller.LoadScene ("Classroom");
			}
		}
	}

}
