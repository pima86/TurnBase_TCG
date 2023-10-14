using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

public class LightBlink : MonoBehaviour
{
    public UnityEngine.Experimental.Rendering.Universal.Light2D light;

    float limit = 0.1f;
    float time = 0;
    bool SN = true;
    void Update()
    {
        time += Time.deltaTime;
        if (time > limit)
        {
            time = 0;

            if (SN)
                light.intensity += 0.1f;
            else
            {
                limit = 0.1f;
                light.intensity -= 0.1f;
            }


            if (light.intensity < 0.5f)
                SN = true;
            else if (light.intensity > 1.7f)
            {
                limit = 5;
                SN = false;
            }
        }
    }
}
