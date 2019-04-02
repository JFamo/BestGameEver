using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankTurretController : MonoBehaviour {

	public GameObject myProjectile;
	public float cooldownTime;
	public float myRange;

	private bool canShoot;
	private GameObject player;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("FPSController");
		canShoot = true;
		cooldownTime = 3.0f;
		myProjectile.SetActive (false);
	}

	IEnumerator EndCooldown(float delay){
		yield return new WaitForSeconds (delay);
		canShoot = true;
	}

	void Update () {
		if(Vector3.Distance(transform.position, player.transform.position) < myRange){
			transform.LookAt (2 * transform.position - new Vector3(player.transform.position.x,transform.position.y,player.transform.position.z));
			if (canShoot) {
				gameObject.GetComponent<AudioSource> ().Play ();
				GameObject newProjectile = GameObject.Instantiate (myProjectile, transform.position, transform.rotation);
				newProjectile.transform.LookAt (player.transform.position);
				newProjectile.SetActive (true);
				canShoot = false;
				StartCoroutine (EndCooldown (cooldownTime));
			}
		}
	}
}
