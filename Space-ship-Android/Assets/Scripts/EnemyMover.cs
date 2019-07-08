using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour {

	// This script Defines the movement of the enemy and the fire rate.(Forward)
	
	public int speed;
	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;
	private float nextFire;
	// Use this for initialization
	void Start () {
		GetComponent<Rigidbody>().velocity = transform.forward * (-speed);
	}
	

	// Update is called once per frame
	void FixedUpdate () {
		/*
			Controls the fire Rate of the enemy
		*/
		if ( Time.time > nextFire && shotSpawn!=null)
		{
			nextFire = Time.time + fireRate;
			Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
		}
	}
}
