using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTracker : MonoBehaviour {

	public List<Quest> quests;

	void Start () {
		quests = new List<Quest> ();
	}

	public void AddQuest(Quest q){
		quests.Add (q);
	}

	public void AdvanceQuest(int id){
		Debug.Log ("Got questtracker advance " + id);
		for (int i = 0; i < quests.Count; i++) {
			if (quests [i].myid == id) {
				quests [i].Advance ();
			}
		}
	}

	public int GetProgress(int id){
		for (int i = 0; i < quests.Count; i++) {
			if (quests [i].myid == id) {
				return quests [i].progress;
			}
		}
		return -1;
	}

}
