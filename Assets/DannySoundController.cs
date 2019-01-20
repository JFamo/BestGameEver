using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DannySoundController : MonoBehaviour {

	public void PlaySound(AudioClip mySound){
		this.GetComponent<AudioSource> ().clip = mySound;
		this.GetComponent<AudioSource> ().Play ();
	}
}
