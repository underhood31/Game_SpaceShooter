using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Mover_Bolt : MonoBehaviour {
public float speed;
	// Gives to movment to the bolt
    void Start ()
    {
        GetComponent<Rigidbody>().velocity = transform.up * speed;
    }
   
}