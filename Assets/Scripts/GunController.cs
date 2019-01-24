using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour {

	private GameObject currentTarget;
	private ParticleSystem myParticleSystem;
	private Highlighter myHighlighter;
	private float range = 7.5f;
	private bool brokenTarget;

	//My Inventory
	private List<TimeObject> myInventory;

	void Start () {
		currentTarget = null;
		brokenTarget = true;
		myInventory = new List<TimeObject>();
		myParticleSystem = GetComponentInChildren<ParticleSystem> ();
		myHighlighter = GetComponentInParent<Highlighter> ();
	}

	void LateUpdate () {
		if (Input.GetButton ("Fire1")) {
			myParticleSystem.gameObject.SetActive(true);	//Activate vacuum effect
			if (myHighlighter.getChangedObject () != null) {	//If we have a highlighted timeobject
				currentTarget = myHighlighter.getChangedObject ();
				StartCoroutine (AbsorbObject (currentTarget.GetComponent<TimeObject>().length));	//begin absorb with delay of TimeObject length
			}
		} else {
			myParticleSystem.gameObject.SetActive(false);	//Deactivate vacuum effect
			currentTarget = null;
			brokenTarget = true;
		}
	}

	public float getRange(){
		return range;
	}

	//function to remove an item from the scene and add it to inventory
	IEnumerator AbsorbObject(float delay){
		yield return new WaitForSeconds (delay);
		if(currentTarget != null){	//If we are still targeting

		}
	}
}
