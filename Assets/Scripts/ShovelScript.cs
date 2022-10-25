using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShovelScript : MonoBehaviour {

    GameObject keptCube = null;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (keptCube != null)
        {
            //keptCube.transform.position = this.transform.position + direction;
        }
	}

	void OnCollisionEnter(Collision collision){
		if (collision.gameObject.CompareTag ("Soil") || collision.gameObject.CompareTag("Mold")) {
			transform.GetChild (0).gameObject.GetComponent<BoxCollider> ().isTrigger = true;
			transform.GetChild (1).gameObject.GetComponent<BoxCollider> ().isTrigger = true;
			transform.GetChild (2).gameObject.GetComponent<BoxCollider> ().isTrigger = true;
			transform.GetChild (3).gameObject.GetComponent<BoxCollider> ().isTrigger = true;
			transform.GetChild (4).gameObject.GetComponent<BoxCollider> ().isTrigger = true;
		}
	}

    public void SetKeptCube(GameObject newCube)
    {
        keptCube = newCube;

        keptCube.transform.parent = this.transform;
        keptCube.GetComponent<Rigidbody>().isKinematic = true;
        keptCube.tag = "DirtCube";
    }

    public void FreeCube()
    {
        if (keptCube != null)
        {
            keptCube.tag = "Draggable";
            keptCube.GetComponent<Rigidbody>().isKinematic = false;
            keptCube.transform.parent = null;
        }
        keptCube = null;
    }

    public bool IsCubeAlready()
    {
        return keptCube != null;
    }
 }
