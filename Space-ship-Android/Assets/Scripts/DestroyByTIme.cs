using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByTIme : MonoBehaviour {
	//This Script is used to destroy Objects after certain lifetime
	public float lifetime;
	void Start () {
		Destroy (gameObject, lifetime);	
	}
	

}
