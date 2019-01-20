using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueAnimator : MonoBehaviour {

	void OnEnable(){
		this.GetComponent<Animator> ().Play ("FadeIn");
	}

	public void Disappear(){
		this.GetComponent<Animator> ().Play ("FadeOut");
		StartCoroutine (StartGame(0.15f));
	}

	IEnumerator StartGame(float delay){
		yield return new WaitForSeconds (delay);
		gameObject.SetActive (false);
	}
}
