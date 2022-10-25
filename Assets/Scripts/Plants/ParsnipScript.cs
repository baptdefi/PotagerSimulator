using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParsnipScript : PlantScript
{
    // Use this for initialization
    public override void Start()
    {
        base.Start();
        optimalHumidity = Humidity.Normal;
        optimalIllumination = 0.8f;
        optimalTemperature = Temperature.Fresh;
        growthSpeed = 0.003f;
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }
}
