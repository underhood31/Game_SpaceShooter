using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ToMain : MonoBehaviour {
	// Script to perform the given action when the function is called
	public void SceneChange(){
		Handheld.Vibrate();
		SceneManager.LoadScene("Main");
	}
	
	public void ExitGame(){
		Handheld.Vibrate();
		Application.Quit();
	}
	public void ShowInfo(){
		Handheld.Vibrate();
		SceneManager.LoadScene("HowTo");
	}
}
