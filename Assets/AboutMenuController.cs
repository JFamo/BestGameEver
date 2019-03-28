using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AboutMenuController : MonoBehaviour {

	private GameObject aboutMenu;
	private GameObject titleMenu;
	private Animator aboutAnim;
	private Animator titleAnim;

	// Use this for initialization
	void Start () {
		aboutMenu = GameObject.Find ("AboutSection");
		titleMenu = GameObject.Find ("TitleBackground");
		aboutAnim = aboutMenu.GetComponent<Animator> ();
		titleAnim = titleMenu.GetComponent<Animator> ();
	}
	
	public void ShowAbout(){
		aboutAnim.Play ("AboutMenuSlideIn");
		titleAnim.Play ("AboutMenuSlideOut");
	}

	public void HideAbout(){
		aboutAnim.Play ("AboutMenuSlideOut");
		titleAnim.Play ("AboutMenuSlideIn");
	}
}
