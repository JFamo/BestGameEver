using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SopwithController : MonoBehaviour {

	public float speed;
	public float range;
	public float myYpos;
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
		if(Vector3.Distance(transform.position, player.transform.position) < range){
			transform.LookAt (new Vector3(player.transform.position.x,transform.position.y,player.transform.position.z));
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
		if(Random.Range(0,240) < 2 && Vector3.Distance(transform.position, player.transform.position) < range){
			targetPosition = (Random.insideUnitSphere * 5) + transform.position;
			targetPosition.y = transform.position.y;
		}
	}

	void LateUpdate(){
		transform.position = new Vector3 (transform.position.x, myYpos, transform.position.z);
	}

	IEnumerator EndCooldown(float delay){
		yield return new WaitForSeconds (delay);
		canShoot = true;
	}
}
