using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DestroyLaser : MonoBehaviour {

	// This Script is Used to destroy GameObjects using the Laser, for both Player and enemies
	public GameObject explosion;					// Explosion for other GameObject
	public GameObject playerExplosion;				// Explosion for the Player
	
	private GameControllerScript gameControllerLsr;	// GameController Script
	void Start ()
	{
		GameObject gameControllerObject = GameObject.FindGameObjectWithTag ("GameController");
		if (gameControllerObject != null) {
			gameControllerLsr = gameControllerObject.GetComponent <GameControllerScript>();
		}
		if (gameControllerLsr == null) {
			Debug.Log ("Cannot find 'GameControllerLsr' script");
		}
	}
	void OnTriggerExit (Collider other)
	{
		/*
			When laser comes in contact with any GameObject this function is called.
			If other gameObject is player then call gameOver, else Distroy other as well as this gameObject
		*/

		if (other.tag == "Player")
		{
			Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
			gameControllerLsr.GameOver();
		}
		
		Instantiate(explosion, transform.position, transform.rotation);
		gameControllerLsr.AddScore(newScoreValue: -1);
		Destroy (other.gameObject);
	}
}
