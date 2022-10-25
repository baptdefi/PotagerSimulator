using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TCursorScript : MonoBehaviour {

    // Bounds of the cursor.
    private float minX;
    private float maxX;

    // Temperature value: 0 = cold / 1 = hot.
    private float temperatureLevel;
    private Temperature temperature;

    public Temperature Temperature
    {
        get
        {
            return temperature;
        }
    }

    public float TemperatureLevel
    {
        get
        {
            return temperatureLevel;
        }
    }

    // Use this for initialization
    void Start () {
        // Get the bounds relative to the Rail position and size.
        Transform railTransform = GameObject.Find("RailT").transform;
        minX = railTransform.position.x - railTransform.localScale.x / 2 + transform.localScale.x;
        maxX = railTransform.position.x + railTransform.localScale.x / 2 - transform.localScale.x;

        // Init Temperature.
        temperatureLevel = 0.5f;
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
        temperatureLevel = (transform.position.x - maxX) / (minX - maxX);

        if(temperatureLevel < 0.25f)
        {
            temperature = Temperature.Cold;
        } else if (temperatureLevel < 0.5f)
        {
            temperature = Temperature.Fresh;
        }
        else if (temperatureLevel < 0.75f)
        {
            temperature = Temperature.Temperate;
        }
        else
        {
            temperature = Temperature.Hot;
        }
    }
}
