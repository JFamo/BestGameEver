using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour {

	public LineRenderer myLaserLineRenderer;
	private ParticleSystem myParticleSystem;
	private float laserWidth = 0.5f;
	private float range = 7.5f;

	void Start () {
		Vector3[] initLaserPositions = new Vector3[ 2 ] { Vector3.zero, Vector3.zero };
		myLaserLineRenderer.SetPositions (initLaserPositions);
		myLaserLineRenderer.startWidth = laserWidth/20;
		myLaserLineRenderer.endWidth = laserWidth;
		myParticleSystem = GetComponentInChildren<ParticleSystem> ();
	}

	void LateUpdate () {
		if (Input.GetButton ("Fire1")) {
			//ShootLaser (transform.position, transform.TransformDirection (Vector3.forward), range);
			//myLaserLineRenderer.enabled = true;
			myParticleSystem.Play();
		} else {
			//myLaserLineRenderer.enabled = false;
			myParticleSystem.Pause();
		}
	}

	/** Function for if we were using a line renderer as the vacuum projection
	void ShootLaser(Vector3 targetPos, Vector3 direction, float length){
		Ray ray = new Ray (targetPos, direction);
		RaycastHit hit;
		Vector3 endPos = targetPos + (length * direction);

		if(Physics.Raycast(ray, out hit, length)){
			endPos = hit.point;
		}

		myLaserLineRenderer.SetPosition(0 , targetPos);
		myLaserLineRenderer.SetPosition(1 , endPos);
	}
	**/


	public float getRange(){
		return range;
	}
}
