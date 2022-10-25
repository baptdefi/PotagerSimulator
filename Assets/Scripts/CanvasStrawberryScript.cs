using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasStrawberryScript : MonoBehaviour {

    public GameObject humidityTB;
    public GameObject illuminationTB;
    public GameObject temperatureTB;

    private Text humidityText;
    private Text illuminationText;
    private Text temperatureText;

    private string humidity;
    private string illumination;
    private string temperature;

    // Use this for initialization
    void Start () {

		// Update the humidity.
		humidity = "Humide";
		humidityText = humidityTB.GetComponent<Text>();
		humidityText.text = humidity;

		// Update the illumination.
		illumination = "70 %";
		illuminationText = illuminationTB.GetComponent<Text>();
		illuminationText.text = illumination;

        // Update the temperature.
        temperature = "Chaud";
        temperatureText = temperatureTB.GetComponent<Text>();
        temperatureText.text = temperature;

    }

	// Update is called once per frame
	void Update () {

	}
}
