using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Boundary{
	public float Xmin,Xmax,Zmin,Zmax;
	public Boundary(){
		Xmin=-5.4f;
		Xmax=5.4f;
		Zmin=0.2f;
		Zmax=16.8f;
	}
}
public class PlayerController : MonoBehaviour {

	//This Scrip conrols the movement of the Player gameObject

	public float speed=5;
	public bool onTable=true;
	public Boundary boundary=new Boundary();
	public float tilt = 2;
	public GameObject shot;
	public Transform shotSpawn;

	public GameObject playerExplosion;
	public float fireRate;
	private float nextFire;
	private GameControllerScript gameController;
	
	void Awake(){
		GameObject gameControllerObject = GameObject.FindGameObjectWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent <GameControllerScript>();
		}
		if (gameController == null) {
			Debug.Log ("Cannot find 'GameController' script");
		}
	}

	void Update ()												
	{
		/*
			Controls the fireRate
		*/
		if (Input.GetButton("Fire1") && Time.time > nextFire)
		{
			nextFire = Time.time + fireRate;
			// Handheld.Vibrate();
			Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
		}
		
	}
	void FixedUpdate () {
		/*
			Controls the movement of the Player gameObject and restricts it's movement
			to values specified in public variable boundary of class Boundary.
			It also rotates the player with respect to horizontal motion.
		*/
		Vector3 movement=Input.acceleration;
		if(onTable){	// If orientation of phone is horizontal. True always.
			movement=Quaternion.Euler(90,0,0)*movement;
		}
		
		GetComponent<Rigidbody>().velocity=new Vector3(movement.x*speed,0,movement.z*speed);
		Vector3 CurrVel=GetComponent<Rigidbody>().velocity;
		Vector3 angles=new Vector3(0,0,CurrVel.x*(-tilt));
		Quaternion Ang = Quaternion.identity;
		Ang.eulerAngles=angles;
		GetComponent<Rigidbody>().rotation=Ang;
		
		GetComponent<Rigidbody>().position=new Vector3
		(	Mathf.Clamp(GetComponent<Rigidbody>().position.x,boundary.Xmin,boundary.Xmax),
			0.0f,
			Mathf.Clamp(GetComponent<Rigidbody>().position.z,boundary.Zmin,boundary.Zmax)
		);
	}
	void OnTriggerEnter(Collider other)
    {
    	/*
			Check collision with powerUp pickup object and take appropriate actions.
        */
        if (other.gameObject.CompareTag("FireUp"))
        {
            Destroy(other.gameObject);	
			
            fireRate=0;		//Reduce FireRate to 0 to give benefit to the player for the current level
            
            
        }
		else{
		
			Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
			Destroy(other.gameObject);
			this.gameObject.SetActive(false);
			// Destroy(other.gameObject);
			gameController.GameOver();
		}
    }
    	

}
