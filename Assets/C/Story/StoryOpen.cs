using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class StoryOpen : MonoBehaviour
{
    public static StoryOpen Inst { get; private set; }
    void Awake() => Inst = this;

    #region �÷��̾� ������
    
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
        Player.Inst.playerdata.storyList[number].content = "0"; //�ر�!!
    }

    void NoDummyStoryAdd(int number)
    {
        Player.Inst.playerdata.storyList[number].content = "1"; //�ر�!!
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
            case "�帴�� ���":
                �帴�ѱ��();
                break;
            case "���� ����":
                ��������();
                break;
            case "���볪�� �༮��":
                ���볪�³༮��();
                break;
            case "�Ŵ��� ����":
                �Ŵ��Ѿ���();
                break;
            case "Ǯ���� ���� �ǹ�":
                Ǯ���������ǹ�();
                break;
            case "���� �׸���":
                �����׸���();
                break;
            case "Ư����ü":
                Ư����ü();
                break;
            case "ö������ �� ������ ����":
                ö�������������ǹ���();
                break;
        }
    }

    void �帴�ѱ��()
    {
        if (!Player.Inst.playerdata.storyList[0].clear)
        {
            NoDummyStoryAdd(0);
            ClearStoryAdd(0);
            NewStoryAdd("���� ����", 1); //���丮 �߰�
            DummyStoryAdd(1); //�ر�
            /*
            NewStoryAdd("����ũ ���� ����", 2); //���丮 �߰�
            DummyStoryAdd(2); //�ر�
            NewStoryAdd("���� ���� ����", 3); //���丮 �߰�
            NewStoryAdd("�׽�Ʈ��3", 3); //���丮 �߰�
            NewStoryAdd("�׽�Ʈ��4", 5); //���丮 �߰�
            */
        }
        Player.Inst.Save();
        Onclick.Inst.OnClickStory_main();
    }

    void ��������()
    {
        if(!Player.Inst.playerdata.storyList[1].clear)
        {
            ClearStoryAdd(1);
            NewStoryAdd("���볪�� �༮��", 2); //���丮 �߰�
            DummyStoryAdd(2); //�ر�
            NewStoryAdd("Ǯ���� ���� �ǹ�", 5); //���丮 �߰�
            DummyStoryAdd(5); //�ر�
        }
        Player.Inst.Save();
        Onclick.Inst.OnClickStory();
    }

    void ���볪�³༮��()
    {
        if (!Player.Inst.playerdata.storyList[2].clear)
        {
            ClearStoryAdd(2);
            NewStoryAdd("�Ŵ��� ����", 4); //���丮 �߰�
            DummyStoryAdd(4); //�ر�
        }
        Player.Inst.Save();
        Onclick.Inst.OnClickStory();
    }

    void �Ŵ��Ѿ���()
    {
        if (!Player.Inst.playerdata.storyList[4].clear)
        {
            ClearStoryAdd(4);
            NewStoryAdd("Ư����ü", 8); //���丮 �߰�
            DummyStoryAdd(8); //�ر�
        }
        Player.Inst.Save();
        Onclick.Inst.OnClickStory();
    }

    void Ǯ���������ǹ�()
    {
        if (!Player.Inst.playerdata.storyList[5].clear)
        {
            ClearStoryAdd(5);
            NewStoryAdd("���� �׸���", 3); //���丮 �߰�
            DummyStoryAdd(3); //�ر�
        }
        Player.Inst.Save();
        Onclick.Inst.OnClickStory_main();
    }

    void �����׸���()
    {
        if (!Player.Inst.playerdata.storyList[3].clear)
        {
            ClearStoryAdd(3);
            NewStoryAdd("ö������ �� ������ ����", 9); //���丮 �߰�
            DummyStoryAdd(9); //�ر�
        }
        Player.Inst.Save();
        Onclick.Inst.OnClickStory();
    }

    void Ư����ü()
    {
        if (!Player.Inst.playerdata.storyList[8].clear)
        {
            ClearStoryAdd(8);
        }
        Player.Inst.Save();
        Onclick.Inst.OnClickStory();
    }

    void ö�������������ǹ���()
    {
        if (!Player.Inst.playerdata.storyList[9].clear)
        {
            ClearStoryAdd(9);
        }
        Player.Inst.Save();
        Onclick.Inst.OnClickStory();
    }
}
