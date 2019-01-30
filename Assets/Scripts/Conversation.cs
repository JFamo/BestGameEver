using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conversation {

	public string name;
	public int numberOfOptions;
	public bool includesTutorialText;
	public string text;
	public string[] options;

	public Conversation(string name, int numberOfOptions, bool includesTutorialText, string text, string option1){
		this.name = name;
		this.numberOfOptions = numberOfOptions;
		this.includesTutorialText = includesTutorialText;
		this.text = text;
		this.options = new string[1];
		options [0] = option1;
	}

	public Conversation(string name, int numberOfOptions, bool includesTutorialText, string text, string option1, string option2){
		this.name = name;
		this.numberOfOptions = numberOfOptions;
		this.includesTutorialText = includesTutorialText;
		this.text = text;
		this.options = new string[2];
		options [0] = option1;
		options [1] = option2;
	}

	public Conversation(string name, int numberOfOptions, bool includesTutorialText, string text, string option1, string option2, string option3){
		this.name = name;
		this.numberOfOptions = numberOfOptions;
		this.includesTutorialText = includesTutorialText;
		this.text = text;
		this.options = new string[3];
		options [0] = option1;
		options [1] = option2;
		options [2] = option3;
	}

}
