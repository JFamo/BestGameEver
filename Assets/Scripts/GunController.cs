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
	private int selectedIndex; //Item currently selected from inventory
	private float spawnDist = 5.0f; //Range at which I spawn items in my forward direction

	//Sounds
	public AudioClip succComplete;
	public AudioClip succRay;

	//GUI
	public Transform inventoryInterface;
	public Transform emptyInvMessage;
	float latestInvLoad;
	float latestEmptyInvMessageLoad;

	//My Inventory
	private List<TimeObject> myInventory;

	void Start () {
		currentTarget = null;
		selectedIndex = 0;
		if (myInventory == null) {
			myInventory = new List<TimeObject>();
		}
		myAudioSource = GetComponent<AudioSource> ();
		myParticleSystem = GetComponentInChildren<ParticleSystem> ();
		myHighlighter = GetComponentInParent<Highlighter> ();
		absorbTime = -1f;
		prevScroll = 0.0f;
	}

	public void GrabDinosaur(){
		//Get Dinosaur in Tutorial
		if (myInventory == null) {
			myInventory = new List<TimeObject>();
		}
		if(GameObject.Find("TutorialController") != null){
			myInventory.Add (GameObject.Find("Dinosaur").GetComponent<TimeObject>());
			GameObject.Find("Dinosaur").SetActive (false);
		}
	}

	void Update () {
		//Get inventory scrolling
		if (Input.GetAxis ("Mouse ScrollWheel") != prevScroll) {
			if (GameObject.Find ("Controller") != null) {
				if (!GameObject.Find ("Controller").GetComponent<ConversationManager> ().isShowing) {
					prevScroll = Input.GetAxis ("Mouse ScrollWheel");

					//check change of index
					if (prevScroll > 0) {
						if (selectedIndex > 0) {
							selectedIndex--;
						}
					} else if (prevScroll < 0) {
						if (selectedIndex < myInventory.Count - 1) {
							selectedIndex++;
						}
					}

					//make new UI 
					GenerateInventoryUI ();
					inventoryInterface.gameObject.SetActive (true);
					StartCoroutine (DelayHideInventory (3.0f));
				}
			} else {

				prevScroll = Input.GetAxis ("Mouse ScrollWheel");

				//check change of index
				if (prevScroll > 0) {
					if (selectedIndex > 0) {
						selectedIndex--;
					}
				} else if (prevScroll < 0) {
					if (selectedIndex < myInventory.Count - 1) {
						selectedIndex++;
					}
				}

				//make new UI 
				GenerateInventoryUI ();
				inventoryInterface.gameObject.SetActive (true);
				StartCoroutine (DelayHideInventory (3.0f));
			}
		}
	}

	void LateUpdate () {
		//Check absorbtion
		if (Input.GetButton ("Fire1")) {
			bool canSucc = false;
			if (GameObject.Find ("Controller") != null) {
				if (gameObject.GetComponentInParent<PlayerHealth> ().currentHealth > 0.025f) {
					canSucc = true;
					gameObject.GetComponentInParent<PlayerHealth> ().UseEnergy (0.025f);
				}
			} else {
				canSucc = true;
			}
			if (canSucc) {
				if (!myParticleSystem.gameObject.activeSelf) {	
					myParticleSystem.gameObject.SetActive (true);	//Activate vacuum effect
				}
				if (!myAudioSource.isPlaying || !myAudioSource.clip == succRay) {
					myAudioSource.clip = succRay;	//Succ ray sound effect
					myAudioSource.volume = 0.6f;
					myAudioSource.Play ();
				}
				if (myHighlighter.getChangedObject () != null && myHighlighter.getChangedObject () != currentTarget) {	//If we have a highlighted timeobject
					currentTarget = myHighlighter.getChangedObject ();
					coroutineCounter++;

					if (myHighlighter.getChangedType () == "timeobject") {
						absorbTime = Time.time;
						StartCoroutine (AbsorbObject (currentTarget.GetComponent<TimeObject> ().length, coroutineCounter));	//begin absorb with delay of TimeObject length
					}

				} else if (myHighlighter.getChangedObject () != null && myHighlighter.getChangedType () == "energy") {
					gameObject.GetComponentInParent<PlayerHealth> ().Heal (0.05f);
				}
			}
			} else {
				myParticleSystem.gameObject.SetActive (false);	//Deactivate vacuum effect
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
			if (currentTarget != null) {
				SetObjectAlpha (currentTarget, 1 - ((Time.time - absorbTime) * 0.6f / currentTarget.GetComponent<TimeObject> ().length));
			}
		}

		//Check Object Placement
		if (Input.GetButtonDown ("Fire2")) {
			PlaceCurrentObject ();
		}
			
	}

	public float getRange(){
		return range;
	}

	//function to remove an item from the scene and add it to inventory
	IEnumerator AbsorbObject(float delay, int myNumber){
		yield return new WaitForSeconds (delay);
		if(currentTarget != null && coroutineCounter == myNumber){	//Ensure we are still targeting
			myHighlighter.AbsorbChangedObject ();
			myInventory.Add(currentTarget.GetComponent<TimeObject>());

			//Quest items
			if (currentTarget.name == "Pebbles") {
				GameObject.Find ("Controller").GetComponent<QuestTracker> ().AdvanceQuest (1);
			}

			absorbTime = -1f;
			currentTarget.SetActive (false);
			currentTarget = null;
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
		if (latestInvLoad + delay <= Time.time + 0.5f ){
			inventoryInterface.gameObject.SetActive (false);
		}
	}

	IEnumerator DelayHideMessage(float delay){
		yield return new WaitForSeconds (delay);
		if (latestEmptyInvMessageLoad + delay <= Time.time + 0.5f ){
			emptyInvMessage.gameObject.SetActive (false);
		}
	}

	public void GenerateInventoryUI(){
		//Grab Prefabs
		latestInvLoad = Time.time;
		GameObject sampleText = inventoryInterface.Find ("SampleText").gameObject;
		sampleText.gameObject.SetActive (true);
		GameObject thisText;

		//Clear Item Texts
		inventoryInterface.gameObject.SetActive (true);
		foreach (GameObject g in GameObject.FindGameObjectsWithTag("InventoryText")) {
			if (g.name == "SampleText") {
				break;
			} else {
				Destroy (g);
			}
		}
		inventoryInterface.gameObject.SetActive (false);

		//Create New Inventory Text Items
		for (int i = 0; i < myInventory.Count; i++) {
			thisText = GameObject.Instantiate (sampleText);
			thisText.transform.SetParent (sampleText.transform.parent);
			thisText.transform.localScale = sampleText.GetComponent<RectTransform>().localScale;
			thisText.transform.localPosition = new Vector3 (sampleText.GetComponent<RectTransform>().localPosition.x, sampleText.GetComponent<RectTransform>().localPosition.y - (21.3f * i), sampleText.GetComponent<RectTransform>().localPosition.z);
			thisText.GetComponentInChildren<Text> ().text = myInventory[i].myName + "\n" + myInventory[i].timeOfOrigin;
			if (i == selectedIndex) {
				thisText.GetComponentInChildren<Text> ().color = Color.blue;
			} else {
				thisText.GetComponentInChildren<Text> ().color = Color.black;
			}
			thisText.name = "itemText";
		}

		sampleText.gameObject.SetActive (false);
	}

	public void PlaceCurrentObject(){
		if (myInventory.Count > 0) {
			GameObject placementObj = myInventory [selectedIndex].gameObject;	//assume timeObject is on root transform gameobject
			Vector3 spawnPos = myHighlighter.gameObject.transform.position + (spawnDist * myHighlighter.gameObject.transform.forward);
			if (spawnPos.y < 1.0f) {
				spawnPos.y = 1.0f;
			} else {
				spawnPos.y += 1.0f;
			}
			placementObj.transform.position = spawnPos;
			Quaternion spawnRot = myHighlighter.gameObject.transform.rotation;
			placementObj.transform.rotation = spawnRot;
			placementObj.SetActive (true);
			myInventory.RemoveAt (selectedIndex);
			selectedIndex = 0;
			GenerateInventoryUI ();
		} else {
			latestEmptyInvMessageLoad = Time.time;
			emptyInvMessage.gameObject.SetActive (true);
			StartCoroutine (DelayHideMessage (1.0f));
		}
	}

	public void ClearCurrentTarget(){
		currentTarget = null;
	}
}
