using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person : MonoBehaviour {

	public bool triggersDialogue;
	public int myID;

	private bool hasOpenDialogue;
	private GameObject talkDialogue;
	private float range;

	void Start () {
		range = 5.0f;
		hasOpenDialogue = false;
		//Get the dialogue via the controller
		talkDialogue = GameObject.Find ("Controller").GetComponent<cs2_controller> ().talkDialogue;
	}
	
	// Update is called once per frame
	void Update () {
		//Check for "E TO TALK" Dialogue Triggering
		if (triggersDialogue) {
			if (Vector3.Distance (GameObject.Find ("FPSController").transform.position, gameObject.transform.position) <= range && !hasOpenDialogue) {
				talkDialogue.SetActive (true);
			} else if (talkDialogue.activeInHierarchy) {	//This means only one NPC can trigger dialogue
				talkDialogue.GetComponent<DialogueAnimator> ().Disappear ();
			}
		}
		//Open or close dialogue with E
		if (Input.GetKeyDown (KeyCode.E)) {
			if (!hasOpenDialogue) {
				if (Vector3.Distance (GameObject.Find ("FPSController").transform.position, gameObject.transform.position) <= range) {
					OpenDialogue ();
				}
			} else {
				CloseDialogue ();
			}
		}
		//Close dialogue with distance
		if(hasOpenDialogue){
			if (Vector3.Distance (GameObject.Find ("FPSController").transform.position, gameObject.transform.position) > range) {
				CloseDialogue ();
			}
		}
		//Check if dialogue was closed via enter
		if (hasOpenDialogue) {
			if (!GameObject.Find ("Controller").GetComponent<ConversationManager> ().isShowing) {
				hasOpenDialogue = false;
			}
		}
	}

	private void OpenDialogue(){
		hasOpenDialogue = true;
		if (talkDialogue.activeInHierarchy) {
			talkDialogue.GetComponent<DialogueAnimator> ().Disappear ();
		}
		ConversationSet c = GenerateConversation ();
		GameObject.Find ("Controller").GetComponent<ConversationManager> ().OpenConversation(c);
	}

	private void CloseDialogue(){
		hasOpenDialogue = false;
		if (talkDialogue.activeInHierarchy) {
			talkDialogue.GetComponent<DialogueAnimator> ().Disappear ();
		}
		GameObject.Find ("Controller").GetComponent<ConversationManager> ().CloseConversation();
	}

	private ConversationSet GenerateConversation(){
		ConversationSet c = new ConversationSet();

		if(myID == 1){
			c.AddConversation( new Conversation("Ctesibius", 1, true, "Ποιος είσαι? Και γιατί έχετε πάρει το χρόνο να μεταφράσετε!", "Huh?", 1));
			c.AddConversation( new Conversation("Ctesibius", 2, true, "Sorry, who are you?", "I should be asking you.", "Honestly I'm not really sure.", 2, 3));
			c.AddConversation( new Conversation("Ctesibius", 1, true, "I am Ctesibius! Inventor! Father of Pneumatics!", "Cool! Where is... anyone else?", 5));
			c.AddConversation( new Conversation("Ctesibius", 2, true, "A question one would ask Plato or Aristotle.", "Do you know them?", "Are they around here somewhere?", 4, 4));
			c.AddConversation( new Conversation("Ctesibius", 1, true, "Oh no, Plato died 200 years ago!", "Ah. Where is everyone else?", 5));
			c.AddConversation( new Conversation("Ctesibius", 1, true, "The rest of the town is still asleep.", "Asleep? It's the middle of the day!", 6));
			c.AddConversation( new Conversation("Ctesibius", 1, true, "Well usually Tromázo wakes everyone up, but he's still asleep.", "So if he doesn't wake up, the town doesn't do anything?", 7));
			c.AddConversation( new Conversation("Ctesibius", 2, true, "I suppose so. It's rather inefficient... You know, I invented a form of water clock. I wonder if I could use it to herald the arrival of Hellios.", "Like, an alarm clock?", "Who?", 8, 9));
			c.AddConversation( new Conversation("Ctesibius", 1, true, "A... a what? What foreign land do you journey from, stranger? What is this tongue?", "I'm from... some other time. Er, place.", 10));
			c.AddConversation( new Conversation("Ctesibius", 1, true, "Apollo Helios! God of the sun! You're not from around here, are you?", "No, I'm from... some other time. Er, place.", 10));
			c.AddConversation( new Conversation("Ctesibius", 3, true, "I like the idea of this a-la-rum clock. If only I had some viable way of linking my clock to a loud noise.", "What about a gong?", "What about falling rocks?", "What about a trumpet?", 11, 13, 12));
			c.AddConversation( new Conversation("Ctesibius", 1, true, "Perhaps, but something will need to strike it.", "Well maybe something else then.", 10));
			c.AddConversation( new Conversation("Ctesibius", 1, true, "Ochi, it takes too much air to blow a trumpet.", "Well maybe something else then.", 10));
			c.AddConversation( new Conversation("Ctesibius", 1, true, "Naí! That's it! Could you get me some pebbles from the pond near town?", "Sure!", -1));
		}

		return c;
	}
}
