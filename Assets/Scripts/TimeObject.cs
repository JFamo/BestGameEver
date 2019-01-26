using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeObject : MonoBehaviour {

	public string myName;
	public string timeOfOrigin;
	public float length;

	void Start () {
		if (length <= 0) {
			length = 2.0f;
		}
	}

}
