using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ContentBox : MonoBehaviour
{
    [SerializeField] GameObject content_1;
    [SerializeField] GameObject content_2;

    bool StoryUpDown;
    public void OnClickUpDown()
    {
        if (StoryUpDown)
            StoryUpDown = false;
        else
            StoryUpDown = true;
    }

    Vector3 pos1;
    Vector3 pos2;
    void Start()
    {
        pos1 = content_1.transform.localPosition;
        pos2 = content_2.transform.localPosition;
    }

    void Update()
    {
        if (StoryUpDown)
        {
            content_1.transform.localPosition = Vector3.Lerp(content_1.transform.localPosition, pos1 + new Vector3(0, 462, 0), Time.deltaTime * 10);
            content_2.transform.localPosition = Vector3.Lerp(content_2.transform.localPosition, pos2 + new Vector3(0, 210, 0), Time.deltaTime * 10);
        }
        else
        {
            content_1.transform.localPosition = Vector3.Lerp(content_1.transform.localPosition, pos1 + new Vector3(0, -370, 0), Time.deltaTime * 10);
            content_2.transform.localPosition = Vector3.Lerp(content_2.transform.localPosition, pos2, Time.deltaTime * 10);
        }
    }
}
