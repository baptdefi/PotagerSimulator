using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseAreaScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay(Collider other)
    {
        GameObject go = other.gameObject;

        PlantScript ps = go.GetComponent<PlantScript>();
        if (ps != null)
        {
            if (transform.parent.GetComponent<PlantScript>().IsPlanted)
            {
                Debug.Log("une plante est trop proche !");
                transform.parent.GetComponent<PlantScript>().AddPenalties();
            }
        }

        WeedScript ws = go.GetComponent<WeedScript>();
        if (ws != null)
        {
            Debug.Log("une mauvaise herbe est trop proche !");
            transform.parent.GetComponent<PlantScript>().AddPenalties();
        }
 
    }
}
