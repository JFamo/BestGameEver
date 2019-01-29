using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlighter : MonoBehaviour {

	private float length;
	private GameObject hitObject;

	public Material timeObjHighlighted;
	public Material enemyHighlighted;

	class ChangedObject {
		public GameObject myself;
		public List<ChangedRenderer> renderers;

		public ChangedObject (GameObject thisobj, Material material) {
			renderers = new List<ChangedRenderer>();
			this.myself = thisobj;
			foreach(Renderer renderer in thisobj.GetComponentsInChildren<Renderer>()){
				renderers.Add(new ChangedRenderer(renderer, material));
			}
		}

		public void ResetObject(){
			foreach (ChangedRenderer cr in renderers) {
				cr.ResetMaterials ();
			}
		}

	}

	class ChangedRenderer {
		public List<Material> originalMaterials;
		public Renderer renderer;

		public ChangedRenderer(Renderer renderer, Material newMaterial){
			originalMaterials = new List<Material>();
			this.renderer = renderer;
			foreach(Material mat in renderer.materials){
				Color makeOpaqueColor = mat.color;
				makeOpaqueColor.a = 1.0f;
				mat.color = makeOpaqueColor;
				originalMaterials.Add(mat);
			}
			Material[] newMaterials = new Material[renderer.materials.Length];
			for(int i = 0; i < newMaterials.Length; i ++){
				newMaterials[i] = newMaterial;
			}
			renderer.materials = newMaterials;
		}

		public void ResetMaterials(){
			foreach (Material mat in originalMaterials) {
				StandardShaderUtils.ChangeRenderMode (mat, StandardShaderUtils.BlendMode.Opaque);
			}
			renderer.materials = originalMaterials.ToArray ();
		}
	}

	ChangedObject changedObject;

	void Start () {
		if(GameObject.Find("TutorialController") != null){
			length = 5.0f;
		}else{
			length = gameObject.GetComponentInChildren<GunController> ().getRange ();
		}
	}

	void Update () {
		Ray ray = new Ray (transform.position, transform.TransformDirection (Vector3.forward));
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit, length)) {
			hitObject = hit.transform.root.gameObject;
			if (hitObject) {
				if (hitObject.GetComponent<TimeObject> () != null) {
					if (changedObject != null)
					if (changedObject.myself == hitObject) {
						return;
					} else {
						RevertChangedObject ();
					}
					changedObject = new ChangedObject (hitObject, timeObjHighlighted);
				} else if (hitObject.GetComponent<Enemy> () != null) { 
					if (changedObject != null)
					if (changedObject.myself == hitObject) {
						return;
					} else {
						RevertChangedObject ();
					}
					changedObject = new ChangedObject (hitObject, enemyHighlighted);
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
		changedObject.ResetObject ();
		changedObject = null;
		this.gameObject.GetComponentInChildren<GunController> ().ClearCurrentTarget ();
	}

	public void AbsorbChangedObject(){
		changedObject.ResetObject ();
		changedObject = null;
	}

	public GameObject getChangedObject(){
		if (changedObject != null) {
			return changedObject.myself;
		}
		return null;
	}
		
}