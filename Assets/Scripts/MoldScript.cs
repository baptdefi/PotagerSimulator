using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoldScript : MonoBehaviour {

	public GameObject dirtCubeModel;
    private float minY, maxY;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
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
			minY = other.gameObject.transform.position.y;
        }
    }

    void OnTriggerExit(Collider other) {

		Debug.Log ("trigger exit");
		Debug.Log (other.gameObject.tag);

        // Si la pelle touche un sol
        if (other.gameObject.CompareTag("Shovel"))
        {
			Debug.Log ("pelle detectée");

			other.gameObject.GetComponent<BoxCollider> ().isTrigger = false;
			other.transform.parent.gameObject.transform.GetChild(0).gameObject.GetComponent<BoxCollider> ().isTrigger = false;
			other.transform.parent.gameObject.transform.GetChild(1).gameObject.GetComponent<BoxCollider> ().isTrigger = false;
			other.transform.parent.gameObject.transform.GetChild(2).gameObject.GetComponent<BoxCollider> ().isTrigger = false;
			other.transform.parent.gameObject.transform.GetChild(3).gameObject.GetComponent<BoxCollider> ().isTrigger = false;

			if (other.gameObject.transform.position.y > maxY)
			{
				maxY = other.gameObject.transform.position.y;
			}

            float cubeSize = maxY - minY;

            Vector3 basePosition = other.gameObject.transform.position;
            basePosition.y += 0.25f;
            Vector3 cubesPosition;

            // Crée les petits cubes de terre
            cubesPosition = basePosition;
            cubesPosition.x += 0.05f;
            cubesPosition.z += 0.05f;
            GameObject cube = Instantiate(dirtCubeModel, cubesPosition, Quaternion.identity);
            cube.transform.localScale = new Vector3(cubeSize, cubeSize, cubeSize);

            // Reset.
            minY = 1000000000;
            maxY = 1000000000;
        }
    }

    public void ExtractDirt(Transform shovelTransform)
    {
        float cubeSize = 0.30f;

        // Position du fond
        Vector3 basePosition = shovelTransform.position;
        basePosition.y += 0.1f;
        Vector3 cubesPosition;

        // Crée les petits cubes de terre
        cubesPosition = basePosition;
        cubesPosition.x += 0.05f;
        cubesPosition.z += 0.05f;
        cubesPosition.y += 0.08f;
        cubesPosition += shovelTransform.right * 0.15f;
        GameObject cube = Instantiate(dirtCubeModel, cubesPosition, Quaternion.identity);
        cube.transform.localScale = new Vector3(cubeSize, cubeSize, cubeSize);

        shovelTransform.gameObject.GetComponent<ShovelScript>().SetKeptCube(cube);
    }

}
