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
						RevertChangedObject ();
					}
				}
			} else if (changedObject != null) {
				RevertChangedObject ();
			}
		} else if (changedObject != null) {
			RevertChangedObject ();
		}
	}

	public void RevertChangedObject(){
		changedObject.renderer.material = changedObject.originalMaterial;
		changedObject = null;
	}

	public void DestroyChangedObject(){
		changedObject = null;
	}

	public GameObject getChangedObject(){
		if (changedObject.renderer.gameObject.transform.root.gameObject) {
			return changedObject.renderer.gameObject.transform.root.gameObject;
		}
		return null;
	}

	public Renderer getChangedRenderer(){
		if (changedObject.renderer) {
			return changedObject.renderer;
		}
		return null;
	}
		
}
