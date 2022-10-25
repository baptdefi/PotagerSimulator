using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EarthSoilScript : SoilScript {

	public GameObject dirtCubeModel;
	public GameObject weedsModel;

	private float YThresholdUp;
	private float YThresholdDown;

	private bool emptyPot = false;
	private bool fullPot = false;
    private bool bIsSeed = false;

    private float minY;
    private float maxY;

	private int weedsCount = 0;

    // Use this for initialization
    protected override void Start () {

        base.Start();

		YThresholdUp = transform.position.y;
		YThresholdDown = YThresholdUp - 0.1f;

		humidityLevel = 0f;
        drySpeed = 0.001f;

        minY = 1000000000;
        maxY = 1000000000;
    }

	// Update is called once per frame
	protected override void Update () {
		base.Update ();

		//Debug.Log ("Weed count : " + weedsCount);

        // Get the current game difficulty.
        Difficulty gameDifficulty = GameObject.Find("ControlPannel").GetComponent<DifficultyScript>().GameDifficulty;

        // Just for hard mode.
        if (gameDifficulty == Difficulty.Hard)
        {
            // 1 chance sur 500
            float randomScore = Random.Range(0, 5000);
            if (randomScore == 1)
            {
                SpawnWeeds();
            }
        }
	}

	public void SpawnWeeds(){

		if (weedsCount < 5) {
			float minX, maxX, minZ, maxZ;

			minX = transform.position.x - transform.localScale.x/1.5f;
			maxX = transform.position.x + transform.localScale.x/1.5f;
			minZ = transform.position.z - transform.localScale.z/5f;
			maxZ = transform.position.z + transform.localScale.z/5f;

			Vector3 weedsPosition = new Vector3 (Random.Range (minX, maxX), transform.position.y + 0.4f, Random.Range (minZ, maxZ));

			GameObject go = Instantiate (weedsModel, weedsPosition, Quaternion.identity);
			if (Random.Range (0, 2) == 0) {
				go.transform.GetChild (0).gameObject.SetActive (false);
				go.transform.GetChild (1).gameObject.SetActive (true);
			}

			// Pas de AddWeed ici car il va être fait pas le activation zone

			Debug.Log ("ajout d'une mauvaise herbe");
		}
	}

	public void addWeeds(){
		weedsCount++;
	}

	public void removeWeeds(){
		weedsCount--;
	}

	public void AddDirtCube(float pSize){
		if (!fullPot) {
			transform.Translate (new Vector3 (0, pSize / 10.0f, 0));
			if (emptyPot) {
				emptyPot = false;
				this.transform.parent.GetChild (7).transform.GetChild (0).gameObject.GetComponent<Text> ().text = "";
			}
			if (transform.position.y >= YThresholdUp) {
				fullPot = true;
                if (bIsSeed)
                    this.transform.parent.GetChild (7).transform.GetChild (0).gameObject.GetComponent<Text> ().text = "Pret à arroser";
			}
		}
	}



    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Shovel"))
        {
            if (other.gameObject.transform.position.y < minY)
            {
                minY = other.gameObject.transform.position.y;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Shovel"))
        {
            maxY = other.gameObject.transform.position.y;
        }
    }

    void OnTriggerExit(Collider other) {

		if (!emptyPot) {

			// Si la pelle touche un sol
			if (other.gameObject.CompareTag ("Shovel")) {

				other.gameObject.GetComponent<BoxCollider> ().isTrigger = false;
				other.transform.parent.gameObject.transform.GetChild(0).gameObject.GetComponent<BoxCollider> ().isTrigger = false;
				other.transform.parent.gameObject.transform.GetChild(1).gameObject.GetComponent<BoxCollider> ().isTrigger = false;
				other.transform.parent.gameObject.transform.GetChild(2).gameObject.GetComponent<BoxCollider> ().isTrigger = false;
				other.transform.parent.gameObject.transform.GetChild(3).gameObject.GetComponent<BoxCollider> ().isTrigger = false;
				other.transform.parent.gameObject.transform.GetChild(4).gameObject.GetComponent<BoxCollider> ().isTrigger = false;

                float cubeSize = maxY - minY;

				// Position du fond
				Vector3 basePosition = other.transform.parent.gameObject.transform.GetChild(4).gameObject.transform.position;
				basePosition.y += 0.25f;
				Vector3 cubesPosition;

				//baisser le sol
				transform.Translate (new Vector3 (0, cubeSize / -10.0f, 0));
				if (fullPot) {
					fullPot = false;
					this.transform.parent.GetChild (7).transform.GetChild (0).gameObject.GetComponent<Text> ().text = "";
				}
				if (transform.position.y <= YThresholdDown) {
					emptyPot = true;
                    if (!bIsSeed)
                        this.transform.parent.GetChild (7).transform.GetChild (0).gameObject.GetComponent<Text> ().text = "Pret à planter";
				}

				// Crée les petits cubes de terre
				cubesPosition = basePosition;
				cubesPosition.x += 0.05f;
				cubesPosition.z += 0.05f;
				GameObject cube = Instantiate (dirtCubeModel, cubesPosition, Quaternion.identity);
                cube.transform.localScale = new Vector3(cubeSize, cubeSize, cubeSize);
                
                // Reset.
                minY = 1000000000;
                maxY = 1000000000;
            }
		}
	}

    public void ExtractDirt(Transform shovelTransform)
    {
        if (!emptyPot)
        {
            float cubeSize = 0.15f;

            // Position du fond
            Vector3 basePosition = shovelTransform.position;
            basePosition.y += 0.1f;
            Vector3 cubesPosition;

            //baisser le sol
            transform.Translate(new Vector3(0, cubeSize / -10.0f, 0));
            if (fullPot)
            {
                fullPot = false;
                this.transform.parent.GetChild(7).transform.GetChild(0).gameObject.GetComponent<Text>().text = "";
            }
            if (transform.position.y <= YThresholdDown)
            {
                emptyPot = true;
                if (!bIsSeed)
                    this.transform.parent.GetChild(7).transform.GetChild(0).gameObject.GetComponent<Text>().text = "Pret à planter";
            }

            // Crée les petits cubes de terre
            cubesPosition = basePosition;
            cubesPosition.x += 0.05f;
            cubesPosition.z += 0.05f;
            cubesPosition += shovelTransform.right * 0.15f;
            GameObject cube = Instantiate(dirtCubeModel, cubesPosition, Quaternion.identity);
            cube.transform.localScale = new Vector3(cubeSize, cubeSize, cubeSize);

            shovelTransform.gameObject.GetComponent<ShovelScript>().SetKeptCube(cube);
        }
    }

    public void SetSeed(bool isSeed)
    {
        bIsSeed = isSeed;
    }

    public void SetPlantMessage()
    {
        this.transform.parent.GetChild(7).transform.GetChild(0).gameObject.GetComponent<Text>().text = "Graine plantée";
    }

    public void CleanPlantMessage(bool bForce = false)
    {
        if (bForce || this.transform.parent.GetChild(7).transform.GetChild(0).gameObject.GetComponent<Text>().text == "Conditions optimales")
        {
            this.transform.parent.GetChild(7).transform.GetChild(0).gameObject.GetComponent<Text>().text = "";
        }
    }

    public void SetPlantMessageGrowing()
    {
        this.transform.parent.GetChild(7).transform.GetChild(0).gameObject.GetComponent<Text>().text = "Conditions optimales";
    }

    public override void Water(float water){
		base.Water (water);
	}	

	public bool EmptyPot
	{
		get
		{
			return emptyPot;
		}
	}

	public bool FullPot
	{
		get
		{
			return fullPot;
		}
	}

	public float getYThresholdUp
	{
		get
		{
			return YThresholdUp;
		}
	}

}
