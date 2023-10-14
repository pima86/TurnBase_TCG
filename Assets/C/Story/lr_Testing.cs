using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lr_Testing : MonoBehaviour
{
    public static lr_Testing Inst { get; private set; }
    void Awake() => Inst = this;

    [SerializeField] public Transform[] points;
    [SerializeField] public StoryBlocks[] points_obj;
    [SerializeField] List<lr_LineController> line;
    [SerializeField] List<Transform> temp;

    private int[] story_1 = { 0, 1, 5, -3, 9};
    private int[] dummy_1 = { 1};

    private int[] story_2 = { 1, 5};
    private int[] dummy_2 = { };

    private int[] story_3 = { 1, -2, -4, 8 };
    private int[] dummy_3 = { 0, 2};

    [SerializeField] List<Transform> dummy;
    int dummynum;
    public void LineStart()
    {
        points = new Transform[StoryManager.Inst.StoryLists.Count];
        points_obj = new StoryBlocks[StoryManager.Inst.StoryLists.Count];

        for (int i = 0; i < StoryManager.Inst.StoryLists.Count; i++)
        {
            if (StoryManager.Inst.StoryLists[i] != null)
            {
                points[i] = StoryManager.Inst.StoryLists[i].transform;
                points_obj[i] = StoryManager.Inst.StoryLists[i];
            }
        }

        for (int i = 0; i < line.Count; i++)
        {
            dummynum = 0;
            if (temp.Count != 0)
                temp.Clear();

            if (line[i].number == 1)
                StoryTemp(i, story_1, dummy_1);
            
            if (line[i].number == 2)
                StoryTemp(i, story_2, dummy_2);

            if (line[i].number == 3)
                StoryTemp(i, story_3, dummy_3);
        }
    }

    
    void StoryTemp(int i, int[] number, int[] dum)
    {
        
        for (int x = 0; x < number.Length; x++)
        {
            int num = number[x];

            if (points_obj.Length <= Mathf.Abs(num))
                continue;
            
            if (num < 0)
            {
                num *= -1;
                if (points.Length >= num && points_obj[num] != null)
                {
                    if(points_obj[num].SetA == true)
                        temp.Add(dummy[dum[dummynum]]);
                    dummynum += 1;
                }
            }
            if (points_obj[num] != null)
            {
                if (points_obj[num].SetA == true)
                {
                    if (points.Length >= num)
                        temp.Add(points[num]);
                }
                else
                    break;
            }
        }
        line[i].SetUpLine(temp, points_obj);
    }
}
