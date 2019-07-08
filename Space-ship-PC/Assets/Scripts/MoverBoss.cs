using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverBoss : MonoBehaviour {
	
	//Controlls the movement of the boss Enemy(Horizontal).

	public float Xmax;
	public float speed;
	
	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;
	private float nextFire;
	// Use this for initialization
	void Start () {

		GetComponent<Rigidbody>().velocity = transform.right * speed;
		
	}

	
	// Update is called once per frame
	void FixedUpdate () {
		/*
			Check the boundries and Give the velocity in the opposite direction.
			And Control the fire of the GameObject.
		*/

		if(GetComponent<Rigidbody>().position.x>=Xmax){
			GetComponent<Rigidbody>().velocity = transform.right * (-speed);
		}	
		if(GetComponent<Rigidbody>().position.x<=-Xmax){
			GetComponent<Rigidbody>().velocity = transform.right * (speed);
			
		}
		if ( Time.time > nextFire && shotSpawn!=null)
		{
			nextFire = Time.time + fireRate;
			Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
		}
	}
}
