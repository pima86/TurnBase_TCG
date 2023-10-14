using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelMove : MonoBehaviour
{
    public Vector3 point;
    Vector3 pos;
    void Start()
    {
        pos = gameObject.transform.localPosition;
    }

    public void Move()
    {
        move = true;
    }

    public void Stop()
    {
        move = false;
    }

    bool move;
    void Update()
    {
        if (move)
            gameObject.transform.localPosition = Vector3.Lerp(gameObject.transform.localPosition, pos + point, Time.deltaTime * 10);
        else
            gameObject.transform.localPosition = Vector3.Lerp(gameObject.transform.localPosition, pos, Time.deltaTime * 10);
    }

    void OnMouseOver()
    {
        Move();
    }

    void OnMouseExit()
    {
        Stop();
    }
}
