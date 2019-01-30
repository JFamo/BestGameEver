using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationSet {

	private List<Conversation> conversations;
	public int currentIndex;

	public ConversationSet(){
		conversations = new List<Conversation> ();
		currentIndex = 0;
	}

	public void AddConversation(Conversation c){
		conversations.Add(c);
	}

	public Conversation GetFirst(){
		if (conversations [0] != null) {
			return conversations [0];
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
}
