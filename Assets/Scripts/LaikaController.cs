using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaikaController : MonoBehaviour {

	public float speed;
	public GameObject myProjectile;
	public float cooldownTime;

	private bool canShoot;
	private GameObject player;
	private Vector3 targetPosition;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("FPSController");
		targetPosition = transform.position;
		canShoot = true;
		cooldownTime = 2.0f;
		myProjectile.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		if(Vector3.Distance(transform.position, player.transform.position) < 10.0f){
			transform.LookAt (player.transform.position);
			if (canShoot) {
				gameObject.GetComponent<AudioSource> ().Play ();
				GameObject newProjectile = GameObject.Instantiate (myProjectile, transform.position, transform.rotation);
				newProjectile.transform.LookAt (player.transform.position);
				newProjectile.SetActive (true);
				canShoot = false;
				StartCoroutine (EndCooldown (cooldownTime));
			}
		}
		if(Vector3.Distance(transform.position, targetPosition) > 0.5f){
			transform.position = Vector3.MoveTowards (transform.position, targetPosition, speed * Time.deltaTime);
		}
		if(Random.Range(0,240) < 2 && Vector3.Distance(transform.position, player.transform.position) < 10.0f){
			targetPosition = (Random.insideUnitSphere * 5) + transform.position;
		}
	}

	void LateUpdate(){
		transform.position = new Vector3 (transform.position.x, 35, transform.position.z);
	}

	IEnumerator EndCooldown(float delay){
		yield return new WaitForSeconds (delay);
		canShoot = true;
	}
}
