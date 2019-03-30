using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeObject : MonoBehaviour {

	public string myName;
	public string timeOfOrigin;
	public float length;
	public bool isInactive;

	void Start () {
		isInactive = false;
		if (length <= 0) {
			length = 2.0f;
		}
	}

}
