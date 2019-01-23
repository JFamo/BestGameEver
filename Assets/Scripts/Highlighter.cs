using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlighter : MonoBehaviour {

	private float length;
	private Renderer hitRenderer;

	public Material timeObjHighlighted;

	class ChangedObject {
		public Renderer renderer;
		public Material originalMaterial;

		public ChangedObject (Renderer renderer, Material material) {
			this.renderer = renderer;
			originalMaterial = renderer.sharedMaterial;
			renderer.material = material;
		}

	}

	ChangedObject changedObject;

	void Start () {
		length = gameObject.GetComponentInChildren<GunController> ().getRange ();
	}

	void Update () {
		Ray ray = new Ray (transform.position, transform.TransformDirection (Vector3.forward));
		RaycastHit hit;
		Vector3 endPos = transform.position + (length * transform.TransformDirection (Vector3.forward));

		if (Physics.Raycast (ray, out hit, length)) {
			hitRenderer = hit.transform.GetComponentInChildren<Renderer>();
			if (hitRenderer) {
				if (hitRenderer.GetComponentInParent<TimeObject> () != null) {
					if (changedObject != null)
					if (changedObject.renderer == hitRenderer) {
						return;
					} else {
						changedObject.renderer.material = changedObject.originalMaterial;
					}
					changedObject = new ChangedObject (hitRenderer, timeObjHighlighted);
				} else {
					if (changedObject != null) {
						changedObject.renderer.material = changedObject.originalMaterial;
					}
				}
			}
		} else if (changedObject != null) {
			changedObject.renderer.material = changedObject.originalMaterial;
			changedObject = null;
		}
	}
}
