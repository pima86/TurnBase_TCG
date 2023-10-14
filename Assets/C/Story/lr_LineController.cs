using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class lr_LineController : MonoBehaviour
{
    private LineRenderer lr;
    //private List<Transform> points;
    private StoryBlocks[] points_obj;

    public int number;
    Vector3[] points_In;

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }

    void Start()
    {
        //Invoke("LineUpdate", 0.1f);
    }

    public void SetUpLine(List<Transform> points, StoryBlocks[] obj)
    {
        lr.positionCount = points.Count;
        points_obj = obj;
        //this.points = points;

        for (int i = 0; i < points.Count; i++)
            lr.SetPosition(i, points[i].position);
    }

    /*
    int num = 0;
    private void LineUpdate()
    {
        num = 0;
        if (gameObject.name == "1")
            CountTrash(real); //0 4 3

        if (gameObject.name == "2")
            CountTrash(normal_1);

        if (gameObject.name == "3")
            CountTrash(normal_2);

        for (int i = 0; i < points_In.Length; i++)
            lr.SetPosition(i, points_In[i]);

        LoadPanel.Inst.LoadEnd = true; //로딩 완료
    }

    void CountTrash(int[] temp)
    {
        int PointCount = 0;
        foreach (int i in temp)
        {
            if (points_obj[i].SetA != false)
            {
                PointCount += 1;
            }
        }
        Debug.Log("points_In = " + PointCount);
        points_In = new Vector3[PointCount];
        foreach (int i in temp)
        {
            if (points_obj[i].SetA != false)
            {
                points_In[num] = points[i].position;
                //points_In[PointCount-num-1] = points[i + 1].position;
            }
            num++;
        }
    }
    */
}
