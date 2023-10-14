using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FieldOn : MonoBehaviour
{
    [SerializeField] int num;
    public ParticleSystem part;

    SpriteRenderer img;

    void Start()
    {
        img = GetComponent<SpriteRenderer>();
    }

    void OnMouseOver()
    {
        if (!BattleCam.Inst.isPlay && !TurnManager.Inst.isLoading) //배틀캠이 진행중인지
        {
            if (!CardManager.Inst.isMyCardDrag && StageBlock.Inst.movenum[0].activeSelf) //드래그 중이 아니고 움직임이 가능한 상태일 때
            {
                if (num != 6 && StageBlock.Inst.situation[num + 1] == GameObject.FindGameObjectWithTag("Player") && StageBlock.Inst.situation[num] == null)
                    StartPart(true);
                else if (num != 0 && StageBlock.Inst.situation[num - 1] == GameObject.FindGameObjectWithTag("Player") && StageBlock.Inst.situation[num] == null)
                    StartPart(true);
            }
        }
        else
            StartPart(false);
    }

    public bool usePart = false;
    public void StartPart(bool bo)
    {
        usePart = bo;

        CardManager.Inst.StartPart(part, bo);
    }

    public void OnMouseDown()
    {
        if (usePart && Time.timeScale == 1)
        {
            int play_point = Array.IndexOf(StageBlock.Inst.situation, StageBlock.Inst.player);

            if(play_point > num)
                CardManager.Inst.CharMove(gameObject, -1);
            else if (play_point < num)
                CardManager.Inst.CharMove(gameObject, 1);
        }
    }

    void OnMouseExit()
    {
        StartPart(false);
    }
}
