using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPanel : MonoBehaviour {
    
    private float temperature;
    private float lightness;

    // Use this for initialization
    void Start () {
        // Medium default value.
        temperature = 0.5f;
        lightness = 0.5f;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void UpdateTemperature(float newTemperature)
    {
        temperature = newTemperature;
    }

    public void UpdateLightness(float newLightness)
    {
        lightness = newLightness;
    }

    public float GetTemperature()
    {
        return temperature;
    }

    public float GetLightness()
    {
        return lightness;
    }
}
