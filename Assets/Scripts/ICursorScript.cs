using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ICursorScript : MonoBehaviour {

	// Bounds of the cursor.
	private float minX;
	private float maxX;

	// Illumination value: 0 = dark / 1 = bright.
	private float illumination;

	// Use this for initialization
	void Start () {
		// Get the bounds relative to the Rail position and size.
		Transform railTransform = GameObject.Find("RailI").transform;
		minX = railTransform.position.x - railTransform.localScale.x / 2 + transform.localScale.x;
		maxX = railTransform.position.x + railTransform.localScale.x / 2 - transform.localScale.x;

		// Init Illumination.
		illumination = 0.5f;
	}
	
	// Update is called once per frame
	void Update () {
		// Block the slider.
		if(transform.position.x <= minX)
		{
			transform.position = new Vector3(minX, transform.position.y, transform.position.z);
		} else if (transform.position.x >= maxX)
		{
			transform.position = new Vector3(maxX, transform.position.y, transform.position.z);
		}

		// Stop the move.
		GetComponent<Rigidbody>().velocity = Vector3.zero;

		// Update the temperature value.
		illumination = (transform.position.x - maxX) / (minX - maxX);
	}

	public float Illumination
	{
		get
		{
			return illumination;
		}
	}
}
