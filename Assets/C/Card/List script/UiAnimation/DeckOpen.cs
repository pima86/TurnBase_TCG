using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DeckOpen : MonoBehaviour
{
    [SerializeField] GameObject num1;

    [SerializeField] float y_point;
    [SerializeField] float speed;

    Vector3 prs;

    void Start()
    {
        prs = new Vector3(transform.position.x, y_point, 0);
        MoveTransform(prs, speed);
    }

    float time = 0;
    bool timeto = false;
    void Update()
    {
        time += Time.deltaTime;
        if (time > 0.1f)
            timeto = true;
        if (timeto)
        {
            if (transform.position.y >= y_point)
            {
                if (time > 0.001f)
                    MoveTransform(prs, speed);
            }
        }
    }

    void MoveTransform(Vector3 prs, float dotweenTime = 0)
    {
        num1.SetActive(true);
        transform.position = Vector3.Lerp(transform.position, prs, dotweenTime);
    }
}
