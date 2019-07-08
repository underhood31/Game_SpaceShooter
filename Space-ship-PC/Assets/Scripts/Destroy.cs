using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Destroy : MonoBehaviour {

	// This Script is used for Destroying objects on collision 
	public GameObject explosion;					//Explosion effect for Other GameObject
	public GameObject playerExplosion;				//Explosion effect for the player
	
	public int scoreValue;							//Score Value to increment after destruction
	private GameControllerScript gameController;	//GameController Script
	void Start ()
	{
		GameObject gameControllerObject = GameObject.FindGameObjectWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent <GameControllerScript>();
		}
		if (gameController == null) {
			Debug.Log ("Cannot find 'GameController' script");
		}
	}
	void OnTriggerExit (Collider other)
	{
		if (other.tag == "Boundary" || other.tag=="Laser" || other.tag=="FireUp")	//Do nothing if the other object is of 
		{																			// there tags
			return;
		}
		if (other.tag == "Player")													// If player then initiate GameOver	
		{
			Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
			gameController.GameOver();
			other.gameObject.SetActive(false);
		}
		
		Instantiate(explosion, transform.position, transform.rotation);				// Show Explosion
		gameController.AddScore(newScoreValue: scoreValue);							// Increment Score
		if(other.tag!="Player")														//If not player then destroy both objects 
			Destroy (other.gameObject);
		Destroy (gameObject);
	}
}
	