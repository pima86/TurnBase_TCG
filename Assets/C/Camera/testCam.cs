using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testCam : MonoBehaviour
{
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                print("raycasthit!");
                Debug.DrawRay(ray.origin, ray.direction * 10f, Color.red, 5f);
            }
        }
    }
}
