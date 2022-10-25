using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandSoilScript : SoilScript
{

	// Use this for initialization
	protected override void Start () {
        humidityLevel = 0.05f;
        drySpeed = 0.01f;
    }

    // Update is called once per frame
    protected override void Update () {
		
	}
}
