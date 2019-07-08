using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyerBoundary : MonoBehaviour {
	//This Script is used to destroy any GameObject that goes past the boundary
	void OnTriggerExit(Collider other){
		Destroy	(other.gameObject);
	}
}
