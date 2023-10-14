using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class Text_box_updown : MonoBehaviour
{
    [SerializeField] GameObject panel;
    [SerializeField] TMP_Text text;

    Vector3 originpos;
    [SerializeField] Vector3 pos;
    [SerializeField] float speed;

    [SerializeField] bool incanvas;

    
    void Start()
    {
        Vector3 canvaspos = GameObject.Find("Canvas").transform.position;
        originpos = gameObject.transform.position;
        if (incanvas)
            pos += canvaspos;
    }

    bool DoM = false;
    void Update()
    {
        if (text.text != "")
            movePanel();
    }

    void movePanel()
    {
        panel.transform.localPosition = Vector3.Lerp(panel.transform.localPosition, new Vector3(0, 491, 0), Time.deltaTime * 10);
    }
}
