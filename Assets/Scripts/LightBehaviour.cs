using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBehaviour : MonoBehaviour {

    // Light.
    Light light;

    // Reference values.
    Color coldLight = new Color(164, 244, 214, 255);
    Color defaultLight = new Color(255, 244, 214, 255);
    Color warmLight = new Color(255, 163, 30, 255);

    // 0 -> 164
    // 0.5 -> 255
    // x -> 

    // Use this for initialization
    void Start () {
        light = GetComponent<Light>();
	}
	
	// Update is called once per frame
	void Update () {

		// ILLUMINATION
		// Get the current illumination.
		float illumination = GameObject.Find("CursorI").GetComponent<ICursorScript>().Illumination;

		// Change the intensity of the light
		light.intensity = ComputeIntensity(illumination);
	
		// TEMPERATURE
        // Get the current temperature.
        float temperature = GameObject.Find("CursorT").GetComponent<TCursorScript>().TemperatureLevel;

        // Change the color of the light.
        if(temperature <= 0.5f)
        {
            // Cold.
            light.color = ComputeColdLight(temperature);
        } else
        {
            // Warm.
            light.color = ComputeWarmLight(temperature);
        }        
    }

    /**
     * Return the color of the light when it's cold.
     */
    private Color ComputeColdLight(float pTemperature)
    {
        // Components of the new color;
        float red, green, blue;

        red = coldLight.r + ((defaultLight.r - coldLight.r) / (0.5f)) * pTemperature;
        red /= 255.0f;
        green = coldLight.g + ((defaultLight.g - coldLight.g) / (0.5f)) * pTemperature;
        green /= 255.0f;
        blue = coldLight.b + ((defaultLight.b - coldLight.b) / (0.5f)) * pTemperature;
        blue /= 255.0f;

        return new Color(red, green, blue, 255.0f);
    }

    /**
     * Return the color of the light when it's warm.
     */
    private Color ComputeWarmLight(float pTemperature)
    {
        // Components of the new color;
        float red, green, blue;

        red = warmLight.r + ((defaultLight.r - warmLight.r) / (0.5f)) * (1 - pTemperature);
        red /= 255.0f;
        green = warmLight.g + ((defaultLight.g - warmLight.g) / (0.5f)) * (1 - pTemperature);
        green /= 255.0f;
        blue = warmLight.b + ((defaultLight.b - warmLight.b) / (0.5f)) * (1 - pTemperature);
        blue /= 255.0f;

        return new Color(red, green, blue, 255.0f);
    }

	private float ComputeIntensity(float pIllumination){

		float intensityValue;

		// 0 -> 0.25 & 1 -> 1.75
		intensityValue = pIllumination * 1.5f;
		intensityValue += 0.25f;

		return intensityValue;
	}
}
