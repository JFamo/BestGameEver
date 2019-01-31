using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Quest{

	public string name;
	public string subtitle;
	public bool complete;
	public int myid;
	public int progress;
	public int progressStages;
	public int[] c_links;

	public Quest(int myid, string name, string subtitle, int progressStages, int[] clinks){
		this.myid = myid;
		this.name = name;
		this.subtitle = subtitle;
		complete = false;
		c_links = clinks;
		progress = 0;
		this.progressStages = progressStages;
	}

	public void Advance(){
		Debug.Log ("Advancing quest index : " + myid + " to progress " + progress);
		progress = progress + 1;
		if (progress == progressStages) {
			Complete ();
		}
	}

	public void Complete(){
		complete = true;
		ShowCompleteMessage ();
	}

	public void ShowCompleteMessage(){
		GameObject.Find ("Controller").GetComponent<cs2_controller> ().questTag.GetComponent<Text> ().text = "QUEST COMPLETED";
		GameObject.Find ("Controller").GetComponent<cs2_controller> ().questName.GetComponent<Text> ().text = name;
		GameObject.Find ("Controller").GetComponent<cs2_controller> ().questSubtitle.GetComponent<Text> ().text = subtitle;
		GameObject.Find ("Controller").GetComponent<cs2_controller> ().questBackground.SetActive (true);
		GameObject.Find ("Controller").GetComponent<cs2_controller> ().HideQuestMessage ();
	}

}