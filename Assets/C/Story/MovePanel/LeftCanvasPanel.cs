using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LeftCanvasPanel : MonoBehaviour
{
    [SerializeField] float x;
    [SerializeField] float y;
    [SerializeField] float y_n;
    [SerializeField] float speed;
    [SerializeField] bool clickorOver;

    GameObject panel;
    Vector3 orgpos;

    void Start()
    {
        panel = gameObject;
        orgpos = panel.transform.position;
    }

    public void OnMouseOver()
    {
        if (clickorOver)
            return;
        DOTween.Kill(panel.transform);
        if (y_n == 0)
            panel.transform.DOMove(orgpos + new Vector3(x, y, 0), speed);
        else
        {
            float Ratio = (float)Screen.width / (float)Screen.height;
            if (Ratio > 1.65f)
            {
                panel.transform.DOMove(orgpos + new Vector3(x, y, 0), speed);
            }
            else if (Ratio < 1.65f)
            {
                panel.transform.DOMove(orgpos + new Vector3(x, y_n, 0), speed);
            }
        }
    }

    public void OnMouseExit()
    {
        if (clickorOver)
            return;
        DOTween.Kill(panel.transform);
        panel.transform.DOMove(orgpos, speed);
    }

    public void On_Move()
    {
        DOTween.Kill(panel.transform);
        isMove = true;
        if (y_n == 0)
            panel.transform.DOMove(orgpos + new Vector3(x, y, 0), speed);
        else
        {
            float Ratio = (float)Screen.width / (float)Screen.height;
            if (Ratio > 1.65f)
            {
                panel.transform.DOMove(orgpos + new Vector3(x, y, 0), speed);
            }
            else if (Ratio < 1.65f)
            {
                panel.transform.DOMove(orgpos + new Vector3(x, y_n, 0), speed);
            }
        }
    }

    public void Off_Move()
    {
        DOTween.Kill(panel.transform);
        isMove = false;
        panel.transform.DOMove(orgpos, speed);
    }


    bool isMove = false;
    public void OnMouseDown()
    {
        if (!clickorOver)
            return;

        DOTween.Kill(panel.transform);
        if (!isMove)
        {
            isMove = true;
            if (y_n == 0)
                panel.transform.DOMove(orgpos + new Vector3(x, y, 0), speed);
            else
            {
                float Ratio = (float)Screen.width / (float)Screen.height;
                if (Ratio > 1.65f)
                {
                    panel.transform.DOMove(orgpos + new Vector3(x, y, 0), speed);
                }
                else if (Ratio < 1.65f)
                {
                    panel.transform.DOMove(orgpos + new Vector3(x, y_n, 0), speed);
                }
            }
        }
        else
        {
            isMove = false;
            panel.transform.DOMove(orgpos, speed);
        }
    }
}

//panel.transform.DOMove(orgpos + new Vector3(250, 0, 0), 1f);