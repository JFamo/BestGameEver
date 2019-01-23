using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeObject : MonoBehaviour {

	public string name;
	public int timeOfOrigin;

	public Material[] originalMaterials;

	void Start () {
		Renderer[] hitRenderers = gameObject.GetComponentsInChildren<Renderer> ();
		originalMaterials = new Material[hitRenderers.Length];
		for(int i = 0; i < hitRenderers.Length; i ++){
			originalMaterials[i] = hitRenderers [i].material;
		}
	}

}
