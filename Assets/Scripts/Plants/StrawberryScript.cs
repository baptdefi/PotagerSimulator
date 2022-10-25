using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrawberryScript : PlantScript
{
    // Use this for initialization
    public override void Start()
    {
        base.Start();
        optimalHumidity = Humidity.Wet;
        optimalIllumination = 0.7f;
        optimalTemperature = Temperature.Hot;
        growthSpeed = 0.006f;
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }
}