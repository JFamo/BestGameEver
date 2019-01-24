using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour {

	private GameObject currentTarget;
	private ParticleSystem myParticleSystem;
	private Highlighter myHighlighter;
	private float range = 7.5f;
	private float absorbTime;
	private int coroutineCounter; //Hack to stop coroutine with args

	//My Inventory
	private List<TimeObject> myInventory;

	void Start () {
		currentTarget = null;
		myInventory = new List<TimeObject>();
		myParticleSystem = GetComponentInChildren<ParticleSystem> ();
		myHighlighter = GetComponentInParent<Highlighter> ();
		absorbTime = -1f;
	}

	void LateUpdate () {
		//Check absorbtion
		if (Input.GetButton ("Fire1")) {
			if (!myParticleSystem.gameObject.activeSelf) {	
				myParticleSystem.gameObject.SetActive(true);	//Activate vacuum effect
			}
			if (myHighlighter.getChangedObject () != null && myHighlighter.getChangedObject () != currentTarget) {	//If we have a highlighted timeobject
				currentTarget = myHighlighter.getChangedObject ();
				coroutineCounter++;
				absorbTime = Time.time;
				StartCoroutine (AbsorbObject (currentTarget.GetComponent<TimeObject>().length, coroutineCounter));	//begin absorb with delay of TimeObject length
			}
		} else {
			myParticleSystem.gameObject.SetActive(false);	//Deactivate vacuum effect
			currentTarget = null;
			absorbTime = -1f;
			coroutineCounter++;
		}

		//Check timeobject fade
		if (absorbTime > 0) {
			SetObjectAlpha (currentTarget, 1 - ((Time.time - absorbTime) / currentTarget.GetComponent<TimeObject>().length));
		}
	}

	public float getRange(){
		return range;
	}

	//function to remove an item from the scene and add it to inventory
	IEnumerator AbsorbObject(float delay, int myNumber){
		yield return new WaitForSeconds (delay);
		if(currentTarget != null && coroutineCounter == myNumber){	//Ensure we are still targeting
			myInventory.Add(currentTarget.GetComponent<TimeObject>());
			absorbTime = -1f;
			myHighlighter.DestroyChangedObject ();
			Destroy (currentTarget);
		}
	}

	public void SetObjectAlpha(GameObject go, float alpha){
		Renderer[] rs = go.GetComponentsInChildren<MeshRenderer> ();
		foreach (Renderer r in rs) {
			foreach (Material mat in r.materials) {
				Color myColor = mat.color;
				myColor.a = alpha;
				mat.color = myColor;
			}

		}
	}
}
