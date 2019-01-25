using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunController : MonoBehaviour {

	private GameObject currentTarget;
	private ParticleSystem myParticleSystem;
	private Highlighter myHighlighter;
	private AudioSource myAudioSource;

	private float range = 7.5f; //Range in Unity units at which I can succ
	private float absorbTime; //Raw time at which current absorbtion action began, used for opacity
	private float prevScroll; //Previous frame mousewheel scroll
	private int coroutineCounter; //Hack to stop coroutine with args
	private InventoryItem selectedObject; //Item currently selected from inventory

	//Sounds
	public AudioClip succComplete;
	public AudioClip succRay;

	//GUI
	public Transform inventoryInterface;

	//Define class for inventory items
	public class InventoryItem{
		public string myName;
		public int timeOfOrigin;
		public float length;

		public InventoryItem(TimeObject t){
			this.myName = t.myName;
			this.timeOfOrigin = t.timeOfOrigin;
		}
	}

	//My Inventory
	private List<InventoryItem> myInventory;

	void Start () {
		currentTarget = null;
		selectedObject = null;
		myInventory = new List<InventoryItem>();
		myAudioSource = GetComponent<AudioSource> ();
		myParticleSystem = GetComponentInChildren<ParticleSystem> ();
		myHighlighter = GetComponentInParent<Highlighter> ();
		absorbTime = -1f;
		prevScroll = 0.0f;

		GenerateInventoryUI ();
	}

	void Update () {
		//Get inventory scrolling
		if (Input.GetAxis ("Mouse ScrollWheel") != prevScroll) {
			prevScroll = Input.GetAxis ("Mouse ScrollWheel");
			GenerateInventoryUI ();
			inventoryInterface.gameObject.SetActive (true);
			StartCoroutine (DelayHideInventory (3.0f));
		}
	}

	void LateUpdate () {
		//Check absorbtion
		if (Input.GetButton ("Fire1")) {
			if (!myParticleSystem.gameObject.activeSelf) {	
				myParticleSystem.gameObject.SetActive(true);	//Activate vacuum effect
			}
			if (!myAudioSource.isPlaying || !myAudioSource.clip == succRay) {
				myAudioSource.clip = succRay;	//Succ ray sound effect
				myAudioSource.volume = 0.6f;
				myAudioSource.Play ();
			}
			if (myHighlighter.getChangedObject () != null && myHighlighter.getChangedObject () != currentTarget) {	//If we have a highlighted timeobject
				currentTarget = myHighlighter.getChangedObject ();
				coroutineCounter++;
				absorbTime = Time.time;
				StartCoroutine (AbsorbObject (currentTarget.GetComponent<TimeObject>().length, coroutineCounter));	//begin absorb with delay of TimeObject length
			}
		} else {
			myParticleSystem.gameObject.SetActive(false);	//Deactivate vacuum effect
			if (myAudioSource.isPlaying && myAudioSource.clip == succRay) {
				myAudioSource.Stop ();	//Stop sound effect
			}
			if (currentTarget != null) {
				SetObjectAlpha (currentTarget, 0.6f);
				currentTarget = null;
				absorbTime = -1f;
				coroutineCounter++;
			}
		}

		//Check timeobject fade
		if (absorbTime > 0) {
			if(currentTarget != null) 
				SetObjectAlpha (currentTarget, 1 - ((Time.time - absorbTime) * 0.6f / currentTarget.GetComponent<TimeObject>().length));
		}
			
	}

	public float getRange(){
		return range;
	}

	//function to remove an item from the scene and add it to inventory
	IEnumerator AbsorbObject(float delay, int myNumber){
		yield return new WaitForSeconds (delay);
		if(currentTarget != null && coroutineCounter == myNumber){	//Ensure we are still targeting
			myInventory.Add(new InventoryItem(currentTarget.GetComponent<TimeObject>()));
			absorbTime = -1f;
			myHighlighter.DestroyChangedObject ();
			Destroy (currentTarget);
			myAudioSource.clip = succComplete;
			myAudioSource.volume = 1.0f;
			myAudioSource.Play ();
		}
	}

	public void SetObjectAlpha(GameObject go, float alpha){
		Renderer[] rs = go.GetComponentsInChildren<MeshRenderer> ();
		foreach (Renderer r in rs) {
			foreach (Material mat in r.materials) {
				if (alpha == 1) {
					StandardShaderUtils.ChangeRenderMode (mat, StandardShaderUtils.BlendMode.Opaque);
				} else {
					StandardShaderUtils.ChangeRenderMode (mat, StandardShaderUtils.BlendMode.Transparent);
				}
				Color myColor = mat.color;
				myColor.a = alpha;
				mat.color = myColor;
			}

		}
	}

	IEnumerator DelayHideInventory(float delay){
		yield return new WaitForSeconds (delay);
		inventoryInterface.gameObject.SetActive (false);
	}

	public void GenerateInventoryUI(){
		GameObject sampleText = inventoryInterface.Find ("SampleText").gameObject;
		sampleText.GetComponent<Text> ().enabled = true;
		GameObject thisText;
		for (int i = 0; i < myInventory.Count; i++) {
			thisText = GameObject.Instantiate (sampleText);
			thisText.transform.SetParent (sampleText.transform.parent);
			thisText.transform.localScale = sampleText.GetComponent<RectTransform>().localScale;
			thisText.transform.localPosition = new Vector3 (sampleText.GetComponent<RectTransform>().localPosition.x, sampleText.GetComponent<RectTransform>().localPosition.y - (21.3f * i), sampleText.GetComponent<RectTransform>().localPosition.z);
			thisText.GetComponentInChildren<Text> ().text = myInventory[i].myName + "\n" + myInventory[i].timeOfOrigin;
		}
		sampleText.GetComponent<Text> ().enabled = false;
	}
}
