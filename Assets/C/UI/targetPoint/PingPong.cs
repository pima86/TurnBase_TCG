using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PingPong : MonoBehaviour
{
    float nowPosi;

    void Start()
    {
        nowPosi = this.transform.position.y;
    }

    void Update()
    {
        transform.position = new Vector3(transform.position.x, nowPosi + Mathf.PingPong(Time.time * 0.05f, 0.05f), transform.position.z);
    }
}
