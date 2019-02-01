using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	public float speed;

	void Start(){
		speed = 30.0f;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = transform.position + transform.forward * speed * Time.deltaTime;
		if (transform.position.x > 1000 || transform.position.x < -1000 || transform.position.z > 1000 || transform.position.z < -1000 || transform.position.y > 1000 || transform.position.y < -1000) {
			Destroy (gameObject);
		}
	}

		void OnTriggerEnter(){
			Debug.Log ("Collision with Player Detected!");
			GameObject.Find ("FPSController").GetComponentInChildren<PlayerHealth> ().TakeDamage (5.0f);
			Destroy (gameObject);
		}
}
