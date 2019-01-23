using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlighter : MonoBehaviour {

	private float length;
	private GameObject hitObj;
	private List<GameObject> hitObjects;

	public Material timeObjHighlighted;

	void Start () {
		length = gameObject.GetComponentInChildren<GunController> ().getRange ();
		hitObjects = new List<GameObject> ();
	}

	void Update () {
		Ray ray = new Ray (transform.position, transform.TransformDirection (Vector3.forward));
		RaycastHit hit;
		Vector3 endPos = transform.position + (length * transform.TransformDirection (Vector3.forward));

		if(Physics.Raycast(ray, out hit, length)){
			hitObj = hit.collider.gameObject;
		}

		if (hitObj != null) {
			if (hitObj.GetComponentInChildren<TimeObject> () != null && !hitObjects.Contains (hitObj)) {
				hitObjects.Add (hitObj);
				Renderer[] hitRenderers = hitObj.GetComponentsInChildren<Renderer> ();
				for (int i = 0; i < hitRenderers.Length; i++) {
					hitRenderers [i].material = timeObjHighlighted;
				}
			}
		}

		if(hitObjects.Count > 1){
			foreach (GameObject gmobj in hitObjects) {
				if(gmobj != hitObj){
					Renderer[] objRenderers = gmobj.GetComponentsInChildren<Renderer> ();
					for(int i = 0; i < objRenderers.Length; i ++){
						objRenderers[i].material = gmobj.GetComponent<TimeObject>().originalMaterials[i];
					}
					hitObjects.Remove (gmobj);
				}
			}
		}
	}
}
