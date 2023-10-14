using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rain : MonoBehaviour
{
    [SerializeField] ParticleSystem ps;

    void Start()
    {
        StartCoroutine(raintime());
    }

    IEnumerator raintime()
    {
        var emission = ps.emission;

        yield return new WaitForSeconds(2f);
        emission.rate = 10;
        yield return new WaitForSeconds(2f);
        emission.rate = 100;
        yield return new WaitForSeconds(2f);
        emission.rate = 250;
        yield return new WaitForSeconds(2f);
        emission.rate = 500;
    }
}
