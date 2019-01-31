using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conversation {

	public string name;
	public int numberOfOptions;
	public bool includesTutorialText;
	public string text;
	public string[] options;
	public int[] optionLinks;
	public bool advancesQuest;

	public Conversation(string name, int numberOfOptions, bool includesTutorialText, string text, string option1, int option1link){
		this.name = name;
		this.numberOfOptions = numberOfOptions;
		this.includesTutorialText = includesTutorialText;
		this.text = text;
		this.options = new string[1];
		this.optionLinks = new int[1];
		this.advancesQuest = false;
		options [0] = option1;
		optionLinks [0] = option1link;
	}

	public Conversation(string name, int numberOfOptions, bool includesTutorialText, string text, string option1, int option1link, bool advancesQuest){
		this.name = name;
		this.numberOfOptions = numberOfOptions;
		this.includesTutorialText = includesTutorialText;
		this.text = text;
		this.options = new string[1];
		this.optionLinks = new int[1];
		this.advancesQuest = advancesQuest;
		options [0] = option1;
		optionLinks [0] = option1link;
	}

	public Conversation(string name, int numberOfOptions, bool includesTutorialText, string text, string option1, string option2, int option1link, int option2link){
		this.name = name;
		this.numberOfOptions = numberOfOptions;
		this.includesTutorialText = includesTutorialText;
		this.text = text;
		this.options = new string[2];
		this.optionLinks = new int[2];
		this.advancesQuest = false;
		options [0] = option1;
		options [1] = option2;
		optionLinks [0] = option1link;
		optionLinks [1] = option2link;
	}

	public Conversation(string name, int numberOfOptions, bool includesTutorialText, string text, string option1, string option2, string option3, int option1link, int option2link, int option3link){
		this.name = name;
		this.numberOfOptions = numberOfOptions;
		this.includesTutorialText = includesTutorialText;
		this.text = text;
		this.options = new string[3];
		this.optionLinks = new int[3];
		this.advancesQuest = false;
		options [0] = option1;
		options [1] = option2;
		options [2] = option3;
		optionLinks [0] = option1link;
		optionLinks [1] = option2link;
		optionLinks [2] = option3link;
	}

}
