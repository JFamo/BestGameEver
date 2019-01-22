using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlighter : MonoBehaviour {

	private float length;
	private GameObject hitObj;

	public Material timeObjHighlighted;

	void Start () {
		length = gameObject.GetComponentInChildren<GunController> ().getRange ();
	}

	void Update () {
		Ray ray = new Ray (transform.position, transform.TransformDirection (Vector3.forward));
		RaycastHit hit;
		Vector3 endPos = transform.position + (length * transform.TransformDirection (Vector3.forward));

		if(Physics.Raycast(ray, out hit, length)){
			hitObj = hit.collider.gameObject;
		}

		if (hitObj.GetComponentInChildren<TimeObject> () != null) {
			Renderer[] hitRenderers = hitObj.GetComponentsInChildren<Renderer> ();
			Material[] originalMaterials = new Material[hitRenderers.Length];
			for(int i = 0; i < hitRenderers.Length; i ++){
				originalMaterials = hitRenderers [i].material;
				hitRenderers[i].material = timeObjHighlighted;
			}
		}
	}
}
