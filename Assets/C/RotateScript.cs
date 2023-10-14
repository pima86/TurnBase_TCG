using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateScript : MonoBehaviour
{
    public float Speed;
    public bool Rota;
    void Update()
    {
        if (Rota)
        {
            transform.Rotate(Vector3.up * Speed, Space.Self);
            if (transform.rotation.y == -1)
            {
                Rota = false;
            }
        }
    }

    public void RotateChar()
    {
        Rota = true;
    }
}
