using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WateringCanScript : MonoBehaviour {

	public GameObject waterDropModel;

	private float waterDropTimer = 0;
	private bool inTimer = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		// Si une goutte a été créée, timer
		if (inTimer) {
			waterDropTimer -= Time.deltaTime;
			//Debug.Log (waterDropTimer);
			if (waterDropTimer <= 0) {
				inTimer = false;
			}
		} else {
			if (transform.rotation.eulerAngles.z >= 55 && transform.rotation.eulerAngles.z <= 120) {

				Vector3 basePosition = gameObject.transform.GetChild (0).transform.position;
				Vector3 waterDropPosition;

				waterDropPosition = basePosition;
				waterDropPosition.x += 0.03f;
				waterDropPosition.z += 0.015f;
				Instantiate (waterDropModel, waterDropPosition, Quaternion.identity);

				waterDropPosition = basePosition;
				waterDropPosition.x -= 0.03f;
				waterDropPosition.z += 0.015f;
				Instantiate (waterDropModel, waterDropPosition, Quaternion.identity);

				waterDropPosition = basePosition;
				waterDropPosition.z -= 0.03f;
				Instantiate (waterDropModel, waterDropPosition, Quaternion.identity);

				inTimer = true;
				// timer de 0.1 secondes
				waterDropTimer = 0.1f;

			}
		}

	}

}
