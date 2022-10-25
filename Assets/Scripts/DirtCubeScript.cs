using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtCubeScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Destroy (gameObject, 60);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision other) {

		if (other.gameObject.CompareTag ("Soil")) {
			Destroy (gameObject);
			other.gameObject.GetComponent<EarthSoilScript> ().AddDirtCube (transform.localScale.y);
		}
	}
}
