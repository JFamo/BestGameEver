using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankController : MonoBehaviour {

	public float speed;
	public float myYpos;
	public float myRange;
	private GameObject player;
	private Vector3 targetPosition;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("FPSController");
		targetPosition = transform.position;
	}

	// Update is called once per frame
	void Update () {
		transform.LookAt (2 * transform.position - new Vector3(targetPosition.x,transform.position.y,targetPosition.z));
		if(Vector3.Distance(transform.position, targetPosition) > 0.5f){
			transform.position = Vector3.MoveTowards (transform.position, targetPosition, speed * Time.deltaTime);
		}
		if(Random.Range(0,240) < 2 && Vector3.Distance(transform.position, player.transform.position) < myRange){
			targetPosition = (Random.insideUnitSphere * 10) + transform.position;
			targetPosition.y = transform.position.y;
		}
	}

	void LateUpdate(){
		transform.position = new Vector3 (transform.position.x, myYpos, transform.position.z);
	}
}
