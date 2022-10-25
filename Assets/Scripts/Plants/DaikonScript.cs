using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaikonScript : PlantScript
{
    // Use this for initialization
    public override void Start()
    {
        base.Start();
        optimalHumidity = Humidity.VeryWet;
        optimalIllumination = 0.5f;
        optimalTemperature = Temperature.Cold;
        growthSpeed = 0.008f;
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }
}
