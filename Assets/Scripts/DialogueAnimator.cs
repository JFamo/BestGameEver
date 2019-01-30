using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueAnimator : MonoBehaviour {

	void OnEnable(){
		this.GetComponent<Animator> ().Play ("FadeIn");
	}

	public void Disappear(){
		if (gameObject.activeInHierarchy) {
			this.GetComponent<Animator> ().Play ("FadeOut");
			StartCoroutine (DoHide (0.15f));
		}
	}

	IEnumerator DoHide(float delay){
		yield return new WaitForSeconds (delay);
		gameObject.SetActive (false);
	}
}
