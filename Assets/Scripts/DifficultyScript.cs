using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Difficulty { Easy, Normal, Hard};

public class DifficultyScript : MonoBehaviour {

    private Difficulty gameDifficulty;

    public Difficulty GameDifficulty
    {
        get
        {
            return gameDifficulty;
        }

        set
        {
            gameDifficulty = value;
        }
    }

    // Use this for initialization
    void Start () {
        UpdateDifficultyLevel(Difficulty.Easy);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void UpdateDifficultyLevel(Difficulty difficulty)
    {
        switch(difficulty)
        {
            case Difficulty.Easy:
                SetCanvasFirstPersonMessage("Pour commencer creusez, plantez ce que vous voulez et arrosez afin de garder le sol au taux d’humidité indiqué");
                break;
            case Difficulty.Normal:                
                SetCanvasFirstPersonMessage("Bravo ! Maintenant attention, il faut aussi prendre en compte la luminosité et la température. A vous de jouer !");
                break;
            case Difficulty.Hard:           
                SetCanvasFirstPersonMessage("Vous avez la main verte à ce que je vois. Concentrez vous pour rester le plus possible dans des conditions optimales de poussée sinon votre plante sera de moindre qualité. Pensez à arracher les mauvaises herbes qui poussent et espacez suffisamment vos graines !");             
                break;
        }
        gameDifficulty = difficulty;
        GameObject.Find("CanvasFirstPerson").GetComponent<CanvasFirstPersonScript>().UpdateDifficulty(gameDifficulty);
    }

    public void SetCanvasFirstPersonMessage(string message)
    {
        GameObject.Find("CanvasFirstPerson").GetComponent<CanvasFirstPersonScript>().SetText(message);
    }
}
