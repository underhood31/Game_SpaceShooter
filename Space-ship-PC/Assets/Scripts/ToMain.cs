using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ToMain : MonoBehaviour {
	
	// Script to perform the given action when the function is called
	public void SceneChange(){
		SceneManager.LoadScene("Main");
	}
	
	public void ExitGame(){
		Application.Quit();
	}
}
