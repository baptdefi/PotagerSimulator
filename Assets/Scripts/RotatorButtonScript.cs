using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatorButtonScript : MonoBehaviour {

	// Bounds of the cursor.
	private float minR = 5;
	private float maxR = 355;

	// Illumination value: 0 = cold / 1 = hot.
	private float illumination;

	// Use this for initialization
	void Start () {

		// Init illumination.
		illumination = 0.5f;
	}
	
	// Update is called once per frame
	void Update () {

		// Block the rotator.
		if(transform.rotation.eulerAngles.y < minR)
		{
			transform.eulerAngles = new Vector3(transform.rotation.eulerAngles.x, minR, transform.rotation.eulerAngles.z);
		} else if (transform.rotation.eulerAngles.y > maxR)
		{
			transform.eulerAngles = new Vector3(transform.rotation.eulerAngles.x, maxR, transform.rotation.eulerAngles.z);
		}

		// Update the illumination value.
		illumination = transform.rotation.eulerAngles.y - 5;
		illumination /= 350;
	}

	void FixedUpdate() {
		transform.localEulerAngles = new Vector3 (0, transform.localEulerAngles.y, 0);
		GetComponent<Rigidbody> ().angularVelocity = Vector3.zero;
	}

	public float Illumination
	{
		get
		{
			return illumination;
		}
	}
}
