using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene : MonoBehaviour {

	public static int scene = 0;
	public float length;
	public int myId;

	// Update is called once per frame
	void Start () {
		scene = 1;
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
		}
	}

}
