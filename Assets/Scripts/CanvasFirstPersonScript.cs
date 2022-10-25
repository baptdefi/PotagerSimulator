using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasFirstPersonScript : MonoBehaviour {

	private bool messageOn = false;
	private float timer = 0;
	// 60 secondes
	private float timerDuration = 10;
    private bool bPanelHidden = true;

	// Use this for initialization
	void Start () {
        this.transform.GetChild(0).gameObject.GetComponent<Text>().enabled = false;
        this.transform.GetChild(1).gameObject.GetComponent<Text>().enabled = false;
        this.transform.GetChild(2).gameObject.GetComponent<Text>().enabled = false;
        this.transform.GetChild(3).gameObject.GetComponent<Text>().enabled = false;
        this.transform.GetChild(4).gameObject.GetComponent<Text>().enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
		if (messageOn) {
			float seconds = timer % 60;
			if (seconds < timerDuration) {
				timer+= Time.deltaTime;
			}
			else {
				timer = 0;
				messageOn = false;
                DeleteText();
			}		
		}

        if (Input.GetKeyDown(KeyCode.T))
        {
            this.transform.GetChild(0).gameObject.GetComponent<Text>().enabled = bPanelHidden;
            this.transform.GetChild(1).gameObject.GetComponent<Text>().enabled = bPanelHidden;
            this.transform.GetChild(2).gameObject.GetComponent<Text>().enabled = bPanelHidden;
            this.transform.GetChild(3).gameObject.GetComponent<Text>().enabled = bPanelHidden;
            this.transform.GetChild(4).gameObject.GetComponent<Text>().enabled = bPanelHidden;

            bPanelHidden = !bPanelHidden;
        }
    }

	public void SetText(string p_message){
        this.transform.GetChild(0).gameObject.GetComponent<Text>().enabled = true;
        this.transform.GetChild (0).gameObject.GetComponent<Text> ().text = p_message;
		timer = 0;
		messageOn = true;
	}

    void DeleteText()
    {
        this.transform.GetChild(0).gameObject.GetComponent<Text>().enabled = false;
    }

    public void UpdateDifficulty(Difficulty newDifficulty)
    {
        switch(newDifficulty)
        {
            case Difficulty.Easy:
                this.transform.GetChild(1).gameObject.GetComponent<Text>().text = "Difficulté: Facile";
                break;
            case Difficulty.Normal:
                this.transform.GetChild(1).gameObject.GetComponent<Text>().text = "Difficulté: Moyen";
                break;
            case Difficulty.Hard:
                this.transform.GetChild(1).gameObject.GetComponent<Text>().text = "Difficulté: Difficile";
                break;
        }
    }
}
