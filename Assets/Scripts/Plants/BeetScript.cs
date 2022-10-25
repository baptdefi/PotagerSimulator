using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeetScript : PlantScript {

	// Use this for initialization
	public override void Start () {

		base.Start ();
        optimalHumidity = Humidity.Normal;
        optimalIllumination = 0.8f;
        growthSpeed = 0.001f;
        optimalTemperature = Temperature.Fresh;
	}
	
	// Update is called once per frame
	public override void Update () {
        base.Update ();
	}

}
