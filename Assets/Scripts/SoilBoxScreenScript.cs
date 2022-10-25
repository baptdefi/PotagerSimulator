using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoilBoxScreenScript : MonoBehaviour {

	public GameObject humidityTB;

	private Text humidityText;

    private float lastHumidityValue = 0.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        string newMessage = "";


        humidityText = humidityTB.GetComponent<Text>();
        // Update the humidity.
        float humidityValue = (int)Mathf.Round(this.transform.parent.gameObject.transform.GetChild(0).GetComponent<EarthSoilScript>().HumidityLevel * 100);
        if (humidityValue < 25)
        {
            newMessage = "Sec";
        } else if (humidityValue < 50)
        {
            newMessage = "Normal";
        }
        else if (humidityValue < 75)
        {
            newMessage = "Humide";
        }
        else
        {
            newMessage = "Très humide";
        }

        newMessage += " " + humidityValue;

        //if ((lastHumidityValue - humidityValue) < 0)
        //{
        //    newMessage += " " + humidityValue;
        //    lastHumidityValue = humidityValue;
        //}
        
        humidityText.text = newMessage;
    }
}
