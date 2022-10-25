using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadishScript : PlantScript {

    // Use this for initialization
    public override void Start()
    {
        base.Start();
        optimalHumidity = Humidity.Normal;
        optimalIllumination = 0.8f;
        optimalTemperature = Temperature.Temperate;
        growthSpeed = 0.01f;
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }
}
