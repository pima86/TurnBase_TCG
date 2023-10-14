using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class StoryOpen : MonoBehaviour
{
    public static StoryOpen Inst { get; private set; }
    void Awake() => Inst = this;

    #region 플레이어 데이터
    
    void NewStoryAdd(string name, int number)
    {
        while (Player.Inst.playerdata.storyList.Count <= number)
        {
            Story story_null = new Story();
            Player.Inst.playerdata.storyList.Add(story_null);
        }
        Player.Inst.playerdata.storyList[number].name = name;
        /*
        Story story_dummy = new Story();
        story_dummy.name = name;
        Player.Inst.playerdata.storyList.Add(story_dummy);
        */
        //Player.Inst.playerdata.storyList[number].name = name;
    }

    void DummyStoryAdd(int number)
    {
        Player.Inst.playerdata.storyList[number].content = "0"; //해금!!
    }

    void NoDummyStoryAdd(int number)
    {
        Player.Inst.playerdata.storyList[number].content = "1"; //해금!!
    }

    void ClearStoryAdd(int number)
    {
        Player.Inst.playerdata.storyList[number].clear = true; //clear!!
    }
    #endregion

    public void Openning(string name)
    {
        switch (name)
        {
            case "흐릿한 기억":
                흐릿한기억();
                break;
            case "전투 연습":
                전투연습();
                break;
            case "악취나는 녀석들":
                악취나는녀석들();
                break;
            case "거대한 악취":
                거대한악취();
                break;
            case "풀리지 않은 의문":
                풀리지않은의문();
                break;
            case "검은 그림자":
                검은그림자();
                break;
            case "특수개체":
                특수개체();
                break;
            case "철가면을 쓴 외팔의 무사":
                철가면을쓴외팔의무사();
                break;
        }
    }

    void 흐릿한기억()
    {
        if (!Player.Inst.playerdata.storyList[0].clear)
        {
            NoDummyStoryAdd(0);
            ClearStoryAdd(0);
            NewStoryAdd("전투 연습", 1); //스토리 추가
            DummyStoryAdd(1); //해금
            /*
            NewStoryAdd("유니크 몬스터 교전", 2); //스토리 추가
            DummyStoryAdd(2); //해금
            NewStoryAdd("보스 몬스터 교전", 3); //스토리 추가
            NewStoryAdd("테스트용3", 3); //스토리 추가
            NewStoryAdd("테스트용4", 5); //스토리 추가
            */
        }
        Player.Inst.Save();
        Onclick.Inst.OnClickStory_main();
    }

    void 전투연습()
    {
        if(!Player.Inst.playerdata.storyList[1].clear)
        {
            ClearStoryAdd(1);
            NewStoryAdd("악취나는 녀석들", 2); //스토리 추가
            DummyStoryAdd(2); //해금
            NewStoryAdd("풀리지 않은 의문", 5); //스토리 추가
            DummyStoryAdd(5); //해금
        }
        Player.Inst.Save();
        Onclick.Inst.OnClickStory();
    }

    void 악취나는녀석들()
    {
        if (!Player.Inst.playerdata.storyList[2].clear)
        {
            ClearStoryAdd(2);
            NewStoryAdd("거대한 악취", 4); //스토리 추가
            DummyStoryAdd(4); //해금
        }
        Player.Inst.Save();
        Onclick.Inst.OnClickStory();
    }

    void 거대한악취()
    {
        if (!Player.Inst.playerdata.storyList[4].clear)
        {
            ClearStoryAdd(4);
            NewStoryAdd("특수개체", 8); //스토리 추가
            DummyStoryAdd(8); //해금
        }
        Player.Inst.Save();
        Onclick.Inst.OnClickStory();
    }

    void 풀리지않은의문()
    {
        if (!Player.Inst.playerdata.storyList[5].clear)
        {
            ClearStoryAdd(5);
            NewStoryAdd("검은 그림자", 3); //스토리 추가
            DummyStoryAdd(3); //해금
        }
        Player.Inst.Save();
        Onclick.Inst.OnClickStory_main();
    }

    void 검은그림자()
    {
        if (!Player.Inst.playerdata.storyList[3].clear)
        {
            ClearStoryAdd(3);
            NewStoryAdd("철가면을 쓴 외팔의 무사", 9); //스토리 추가
            DummyStoryAdd(9); //해금
        }
        Player.Inst.Save();
        Onclick.Inst.OnClickStory();
    }

    void 특수개체()
    {
        if (!Player.Inst.playerdata.storyList[8].clear)
        {
            ClearStoryAdd(8);
        }
        Player.Inst.Save();
        Onclick.Inst.OnClickStory();
    }

    void 철가면을쓴외팔의무사()
    {
        if (!Player.Inst.playerdata.storyList[9].clear)
        {
            ClearStoryAdd(9);
        }
        Player.Inst.Save();
        Onclick.Inst.OnClickStory();
    }
}
