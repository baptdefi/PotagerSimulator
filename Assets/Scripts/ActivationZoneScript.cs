using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivationZoneScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other) {

		/*if (other.gameObject.CompareTag ("Shovel"))
		{
			transform.parent.gameObject.transform.GetChild (0).GetComponent<BoxCollider> ().isTrigger = true;
		}*/

		GameObject go = other.gameObject;
		WeedScript ws = go.GetComponent<WeedScript>();
		if (ws != null)
		{
			Debug.Log("mauvaise ajoutée !");
			transform.parent.transform.GetChild (0).gameObject.GetComponent<EarthSoilScript> ().addWeeds ();
		}

	}

	void OnTriggerExit(Collider other) {

		/*if (other.gameObject.CompareTag ("Shovel"))
		{
			transform.parent.gameObject.transform.GetChild (0).GetComponent<BoxCollider> ().isTrigger = false;
		}*/

		GameObject go = other.gameObject;
		WeedScript ws = go.GetComponent<WeedScript>();
		if (ws != null)
		{
			Debug.Log("mauvaise herbe retirée !");
			transform.parent.transform.GetChild (0).gameObject.GetComponent<EarthSoilScript> ().removeWeeds ();
		}

	}
}
