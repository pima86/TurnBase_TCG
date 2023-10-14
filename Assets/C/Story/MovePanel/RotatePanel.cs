using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePanel : MonoBehaviour
{
    [SerializeField] float x;
    [SerializeField] float y;
    [SerializeField] float z;
    [SerializeField] float speed;

    void Update()
    {
        transform.Rotate(new Vector3(x, y, z) * Time.deltaTime * speed);
    }
}
