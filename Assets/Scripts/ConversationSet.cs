using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConversationSet {

	private List<Conversation> conversations;
	public int currentIndex;
	public Quest myQuest;

	public ConversationSet(){
		conversations = new List<Conversation> ();
		currentIndex = 0;
		myQuest = null;
	}

	public void SetQuest(Quest q){
		myQuest = q;
	}

	public void AddConversation(Conversation c){
		conversations.Add(c);
	}

	public Conversation GetFirst(){
		if (myQuest != null) {
			if (conversations [myQuest.c_links [myQuest.progress]] != null) {
				currentIndex = myQuest.c_links [myQuest.progress];
				return conversations [myQuest.c_links [myQuest.progress]];
			}
		} else {
			if (conversations [0] != null) {
				currentIndex = 0;
				return conversations [0];
			}
		}
		return new Conversation ("ERROR", 1, true, "Looked for first conversation that does not exist!", "Okay", -1);
	}

	public Conversation GetNext(int selectedOption){
		int newIndex = conversations [currentIndex].optionLinks [selectedOption];
		if (newIndex < 0) {
			return null;
		}
		currentIndex = newIndex;
		return conversations [newIndex];
	}

	public void CloseConversation(){
		Debug.Log ("Closing convs " + currentIndex);
		if (conversations [currentIndex].advancesQuest) {
			if (myQuest.progress == 0) {
				ShowQuestMessage ();
			}
			GameObject.Find ("Controller").GetComponent<QuestTracker> ().AdvanceQuest (myQuest.myid);
		}
	}

	public void ShowQuestMessage(){
		if (GameObject.Find ("Controller").GetComponent<cs2_controller> () != null) {
			GameObject.Find ("Controller").GetComponent<cs2_controller> ().questTag.GetComponent<Text> ().text = "QUEST STARTED";
			GameObject.Find ("Controller").GetComponent<cs2_controller> ().questName.GetComponent<Text> ().text = myQuest.name;
			GameObject.Find ("Controller").GetComponent<cs2_controller> ().questSubtitle.GetComponent<Text> ().text = myQuest.subtitle;
			GameObject.Find ("Controller").GetComponent<cs2_controller> ().questBackground.SetActive (true);
			GameObject.Find ("Controller").GetComponent<cs2_controller> ().HideQuestMessage ();
		} else {
			if (GameObject.Find ("Controller").GetComponent<cs3_controller> () != null) {
				GameObject.Find ("Controller").GetComponent<cs3_controller> ().questTag.GetComponent<Text> ().text = "QUEST STARTED";
				GameObject.Find ("Controller").GetComponent<cs3_controller> ().questName.GetComponent<Text> ().text = myQuest.name;
				GameObject.Find ("Controller").GetComponent<cs3_controller> ().questSubtitle.GetComponent<Text> ().text = myQuest.subtitle;
				GameObject.Find ("Controller").GetComponent<cs3_controller> ().questBackground.SetActive (true);
				GameObject.Find ("Controller").GetComponent<cs3_controller> ().HideQuestMessage ();
			}
		}
	}

}
