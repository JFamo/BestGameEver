using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person : MonoBehaviour {

	public bool triggersDialogue;
	public int myID;

	private bool hasOpenDialogue;
	private GameObject talkDialogue;
	private ConversationSet myConversationSet;
	private QuestTracker questTracker;
	private float range;

	void Start () {
		range = 5.0f;
		hasOpenDialogue = false;
		myConversationSet = null;
		//Get the dialogue via the controller
		if (GameObject.Find ("Controller").GetComponent<cs2_controller> () != null) {
			talkDialogue = GameObject.Find ("Controller").GetComponent<cs2_controller> ().talkDialogue;
		} else {
			talkDialogue = null;
		}
		questTracker = GameObject.Find ("Controller").GetComponent<QuestTracker> ();
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
		if (myConversationSet == null) {
			myConversationSet = GenerateConversation ();
		}
		GameObject.Find ("Controller").GetComponent<ConversationManager> ().OpenConversation(myConversationSet);
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
		Quest myQuest;

		if (myID == 1) {
			//0 
			c.AddConversation (new Conversation ("Ctesibius", 1, true, "Ποιος είσαι? Και γιατί έχετε πάρει το χρόνο να μεταφράσετε!", "Huh?", 1));
			c.AddConversation (new Conversation ("Ctesibius", 2, true, "Sorry, who are you?", "I should be asking you.", "Honestly I'm not really sure.", 2, 3));
			c.AddConversation (new Conversation ("Ctesibius", 1, true, "I am Ctesibius! Inventor! Father of Pneumatics!", "Cool! Where is... anyone else?", 5));
			c.AddConversation (new Conversation ("Ctesibius", 2, true, "A question one would ask Plato or Aristotle.", "Do you know them?", "Are they around here somewhere?", 4, 4));
			c.AddConversation (new Conversation ("Ctesibius", 1, true, "Oh no, Plato died 200 years ago!", "Ah. Where is everyone else?", 5));
			c.AddConversation (new Conversation ("Ctesibius", 1, true, "The rest of the town is still asleep.", "Asleep? It's the middle of the day!", 6));
			c.AddConversation (new Conversation ("Ctesibius", 1, true, "Well usually Tromázo wakes everyone up, but he's still asleep.", "So if he doesn't wake up, the town doesn't do anything?", 7));
			c.AddConversation (new Conversation ("Ctesibius", 2, true, "I suppose so. It's rather inefficient... You know, I invented a form of water clock. I wonder if I could use it to herald the arrival of Hellios.", "Like, an alarm clock?", "Who?", 8, 9));
			c.AddConversation (new Conversation ("Ctesibius", 1, true, "A... a what? What foreign land do you journey from, stranger? What is this tongue?", "I'm from... some other time. Er, place.", 10));
			c.AddConversation (new Conversation ("Ctesibius", 1, true, "Apollo Helios! God of the sun! You're not from around here, are you?", "No, I'm from... some other time. Er, place.", 10));
			c.AddConversation (new Conversation ("Ctesibius", 3, true, "I like the idea of this a-la-rum clock. If only I had some viable way of linking my clock to a loud noise.", "What about a gong?", "What about falling rocks?", "What about a trumpet?", 11, 13, 12));
			c.AddConversation (new Conversation ("Ctesibius", 1, true, "Perhaps, but something will need to strike it.", "Well maybe something else then.", 10));
			c.AddConversation (new Conversation ("Ctesibius", 1, true, "Ochi, it takes too much air to blow a trumpet.", "Well maybe something else then.", 10));
			c.AddConversation (new Conversation ("Ctesibius", 1, true, "Naí! That's it! Could you get me some pebbles from the pond near town?", "Sure!", -1, true));
			//14
			c.AddConversation (new Conversation ("Ctesibius", 1, true, "Have you gotten pebbles from the pond near town yet?", "I'm still working on it.", -1));
			//15
			c.AddConversation (new Conversation ("Ctesibius", 3, true, "You got the pebbles! Excellent! Now we just need something for the pebbles to fall on...", "What about a gong?", "What about a drum?", "What if they fall on you to wake you up?", 18, 16, 17));
			c.AddConversation (new Conversation ("Ctesibius", 1, true, "I don't think we have any drums around here.", "Maybe something else then.", 15));
			c.AddConversation (new Conversation ("Ctesibius", 1, true, "I don't think anyone would use that.", "Maybe something else then.", 15));
			c.AddConversation (new Conversation ("Ctesibius", 1, true, "Yes! That will work!", "I'll get you a gong.", -1, true));
			//19
			c.AddConversation (new Conversation ("Ctesibius", 1, true, "Have you found a gong yet?", "I'm still working on it.", -1));
			//20
			c.AddConversation (new Conversation ("Ctesibius", 1, true, "You got a gong! Great! Just give me one minute to piece this together...", "...", 21));
			c.AddConversation (new Conversation ("Ctesibius", 1, true, "εύρηκα! I've done it! Can you take it to the center of town and put it in the marketplace for everyone to see?", "I will.", -1, true));
			//22
			c.AddConversation (new Conversation ("Ctesibius", 1, true, "Thanks for your help. Go place the alarm clock in the marketplace for everyone to see.", "I will.", -1));

			myQuest = new Quest (1, "An Alarming Realization", "Help Ctesibius Invent the Alarm Clock", 6, new int[] {
				0,
				14,
				15,
				19,
				20,
				22
			}, new string[]{
				"Talk to Ctesibius",
				"Collect pebbles from the pond for Ctesibius",
				"Return the pebbles to Ctesibius",
				"Get a gong for Ctesibius",
				"Return the gong to Ctesibius",
				"Place the alarm clock in the market"
			});
			questTracker.AddQuest (myQuest);
			c.SetQuest (myQuest);
		} else if (myID == 2) {
			c.AddConversation (new Conversation ("Bastillus", 3, true, "Γεια σου ξένε!", "Who are you?", "What are you doing?", "Why are you awake?", 1, 2, 3));
			c.AddConversation (new Conversation ("Bastillus", 2, true, "I am Bastillus, the weaponsmith and armorer of our proud civilization.", "What are you doing?", "Cool.", 2, 0));
			c.AddConversation (new Conversation ("Bastillus", 1, true, "I was working on some of my latest weapons, but then... something happened. They all just scattered!", "That may have been my advisor's fault.", 4));
			c.AddConversation (new Conversation ("Bastillus", 2, true, "My anvil fell over earlier this morning, waking me up. It appears Tromázo has yet to wake the rest of the town.", "What are you doing?", "Makes Sense.", 2, 0));
			c.AddConversation (new Conversation ("Bastillus", 2, true, "I do not know what that means, but if this is your fault, I need my weapons back. They are the proudest creations of Greece!", "What exactly did you lose?", "No Way!", 5, -1));
			c.AddConversation (new Conversation ("Bastillus", 1, true, "My bronze shield, my helmet, my xiphos shortsword, and my ballista! All gone!", "I will find your weapons for you.", -1, true));
		
			//6
			c.AddConversation (new Conversation ("Bastillus", 1, true, "Have you found my bronze shield, my helmet, my xiphos shortsword, and my ballist yet?", "I'm still working on it.", -1));

			//7
			c.AddConversation (new Conversation ("Bastillus", 1, true, "You got them! Thank you so much! The might of Greece will forever be on your side!", "No problem!", -1, true));

			//8
			c.AddConversation (new Conversation ("Bastillus", 1, true, "Thanks again for your help.", "Of course.", -1));

			myQuest = new Quest (2, "Weaponsmith Woes", "Return Bastillus's inventions in weaponry", 4, new int[]{ 0, 6, 7, 8 }, new string[]{
				"Talk to Bastillus",
				"Collect a shield, helmet, shortsword, and ballista",
				"Return the weapons to Bastillus",
				"Quest Complete"
			});
			questTracker.AddQuest (myQuest);
			c.SetQuest (myQuest);
		} else if (myID == 3) {
			c.AddConversation (new Conversation ("Zeus", 2, true, "Alright, stop.", "Are you... Zeus?", "Stop what?", 1, 2));
			c.AddConversation (new Conversation ("Zeus", 1, true, "That is what the conversation UI says.", "What are you doing?", "Cool.", 2, 0));
			c.AddConversation (new Conversation ("Bastillus", 1, true, "I was working on some of my latest weapons, but then... something happened. They all just scattered!", "That may have been my advisor's fault.", 4));
			c.AddConversation (new Conversation ("Bastillus", 2, true, "My anvil fell over earlier this morning, waking me up. It appears Tromázo has yet to wake the rest of the town.", "What are you doing?", "Makes Sense.", 2, 0));
			c.AddConversation (new Conversation ("Bastillus", 2, true, "I do not know what that means, but if this is your fault, I need my weapons back. They are the proudest creations of Greece!", "What exactly did you lose?", "No Way!", 5, -1));
			c.AddConversation (new Conversation ("Bastillus", 1, true, "My bronze shield, my helmet, my xiphos shortsword, and my ballista! All gone!", "I will find your weapons for you.", -1, true));

			//6
			c.AddConversation (new Conversation ("Bastillus", 1, true, "Have you found my bronze shield, my helmet, my xiphos shortsword, and my ballist yet?", "I'm still working on it.", -1));

			//7
			c.AddConversation (new Conversation ("Bastillus", 1, true, "You got them! Thank you so much! The might of Greece will forever be on your side!", "No problem!", -1, true));

			//8
			c.AddConversation (new Conversation ("Bastillus", 1, true, "Thanks again for your help.", "Of course.", -1));

			myQuest = new Quest (2, "Weaponsmith Woes", "Return Bastillus's inventions in weaponry", 4, new int[]{ 0, 6, 7, 8 }, new string[]{
				"Talk to x",
				"Collect x",
				"Return x",
				"Quest Complete"
			});
			questTracker.AddQuest (myQuest);
			c.SetQuest (myQuest);
		} else if (myID == 5) {
			c.AddConversation (new Conversation ("Kobe", 2, true, "Hello. I am Kobe.", "What's going on, Kobe?", "Hey Kobe - I'm looking for inventions.", 1, 2));
			c.AddConversation (new Conversation ("Kobe", 2, true, "I am standing and talking to you. So not much.", "Uh... Okay", "Goodbye.", 0, -1));
			c.AddConversation (new Conversation ("Kobe", 1, true, "There is a very neat invention up ahead.", "What's that?", 3));
			c.AddConversation (new Conversation ("Kobe", 1, true, "Lunar Lander! The one used by Buzz Aldrin and Neil Armstrong. Don't know how it got here, but I like it.", "Of Course! Thanks Kobe!", -1, true));

			//4
			c.AddConversation (new Conversation ("Kobe", 1, true, "Have fun with lunar lander.", "Uh, yeah", -1));

			myQuest = new Quest(3, "Moon Shot", "See if the Lunar Lander invented the future", 2, new int[]{0, 4}, new string[]{
				"Talk to Kobe",
				"See if the Lunar Lander invented the future"
			});
			questTracker.AddQuest (myQuest);
			c.SetQuest (myQuest);
		} else if (myID == 6) {
			c.AddConversation (new Conversation ("Kobe", 2, true, "Hello. I am Kobe.", "Wait, wasn't the last guy named Kobe?", "Do I... know you?", 1, 2));
			c.AddConversation (new Conversation ("Kobe", 1, true, "Yes! Everyone in the future is named Kobe!", "Ah, Alright", 3));
			c.AddConversation (new Conversation ("Kobe", 1, true, "I do not think so.", "But wasn't the last guy named Kobe?", 1));
			c.AddConversation (new Conversation ("Kobe", 1, true, "You must be the one looking for inventions! You will love the atomic bomb up ahead!", "Yes! The Atomic Bomb made the future!", -1, true));

			//4
			c.AddConversation (new Conversation ("Kobe", 1, true, "Please do not have too much fun with atomic bomb.", "I'll be careful.", -1));

			myQuest = new Quest(4, "You're The Bomb", "See if the Atomic Bomb invented the future", 2, new int[]{0, 4}, new string[]{
				"Talk to Kobe",
				"See if the Atomic Bomb invented the future"
			});
			questTracker.AddQuest (myQuest);
			c.SetQuest (myQuest);
		}
		else if (myID == 7) {
			c.AddConversation (new Conversation ("Kobe", 1, true, "Hello. I am Kobe.", "Yeah, yeah, I know", 1));
			c.AddConversation (new Conversation ("Kobe", 1, true, "You are the one who wants inventions! You will absolutely adore the microtransistor!", "The what?", 2));
			c.AddConversation (new Conversation ("Kobe", 1, true, "Small electric switch; allows computers to work!", "That... makes a lot of sense!", -1, true));
	
			//3
			c.AddConversation (new Conversation ("Kobe", 1, true, "I think you will like the microtransistor.", "I think this may be it.", -1));

			myQuest = new Quest(5, "This Ko-be The One", "See if the Microtransistor invented the future", 2, new int[]{0, 3}, new string[]{
				"Talk to Kobe",
				"See if the Microtransistor invented the future"
			});
			questTracker.AddQuest (myQuest);
			c.SetQuest (myQuest);
		}

		return c;
	}
}
