using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelThreeEnder : MonoBehaviour {

	public GameObject endLevelText;

	private GameObject player;
	private QuestTracker questTracker;
	private Quest myQuest;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("FPSController");
		questTracker = GameObject.Find ("Controller").GetComponent<QuestTracker>();
		endLevelText.SetActive (false);
	}

	// Update is called once per frame
	void Update () {
			if (Vector3.Distance (transform.position, player.transform.position) < 12.0f) {
			if (GetMyQuest(7).progress == 2) {
					endLevelText.SetActive (true);
				}
			} else if (endLevelText.activeInHierarchy) {
				endLevelText.SetActive (false);
			}

			if (Input.GetKeyDown (KeyCode.E) && endLevelText.activeInHierarchy) {
				EndLevel ();
			}
	}

	public void EndLevel(){
		Cutscene.scene = 3;
		scenecontroller.LoadScene ("CutsceneOne");
	}

	public Quest GetMyQuest(int qid){
		for (int i = 0; i < questTracker.quests.Count; i++) {
			if (questTracker.quests [i].myid == qid) {
				return questTracker.quests [i];
			}
		}
		return null;
	}
}
