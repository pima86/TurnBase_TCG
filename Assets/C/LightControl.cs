using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightControl : MonoBehaviour
{
    public float Angle;

    Light lt;


    void Start()
    {
        lt = GetComponent<Light>();
    }

    public void LightOn()
    {
        lt.spotAngle = Angle;
    }
    public void LightOff()
    {
        lt.spotAngle = 10;
    }
}
