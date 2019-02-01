using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scenecontroller : MonoBehaviour {

	public static void LoadScene(string name){

		SceneManager.LoadScene (name,LoadSceneMode.Single);

	}

}
