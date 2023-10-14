using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class StoryManager : MonoBehaviour
{
    public static StoryManager Inst { get; private set; }
    void Awake() => Inst = this;


    [SerializeField] StorySO storySO;
    [SerializeField] Transform[] StorySpawnPoint;
    [SerializeField] GameObject StoryPrefab;
    [SerializeField] public List<StoryBlocks> StoryLists;

    [SerializeField] TMP_Text[] BlocksCount;
    
    [SerializeField] int[] Adnumber; //존재 여부
    [SerializeField] int[] Sticker; //처음 여부

    [SerializeField] int SpawnAddrass = 0;

    int[] Blocks = new int[3];
    void Start()
    {
        SoundPlayer.Inst.BGM_START("정비");


        Adnumber = new int[storySO.stories.Length];
        Sticker = new int[storySO.stories.Length];

        WhatSize();

        for (int i = 0; i < Player.Inst.playerdata.storyList.Count; i++)
        {
            AddCard();
            Invoke("Testing", 0.1f);
        }
    }

    void Testing()
    {
        lr_Testing.Inst.LineStart();
    }

    void WhatSize()
    {
        for (int i = 0; i < Blocks.Length; i++)
        {
            if (Blocks[i] == null)
                Blocks[i] = 0;
        }

        for (int i = 0; i < Player.Inst.playerdata.storyList.Count; i++)
        {
            if (Player.Inst.playerdata.storyList[i].name == storySO.stories[i].name && storySO.stories[i].name != "")
            {
                Adnumber[i] = 1;
                Blocks[0] += 1;

                if (Player.Inst.playerdata.storyList[i].content == "1")
                    Blocks[1] += 1;
                if (Player.Inst.playerdata.storyList[i].clear == true)
                    Blocks[2] += 1;
            }
            if (Player.Inst.playerdata.storyList[i].content != "0")
                Sticker[i] = 1;
        }
        BlocksCount[0].text = "현재 스토리 : " + Blocks[0].ToString();
        BlocksCount[1].text = "미해금 : " + (Blocks[0] - Blocks[1]).ToString();
        BlocksCount[2].text = "해금 : " + Blocks[1].ToString();
        BlocksCount[3].text = "클리어 : " + Blocks[2].ToString();
    }

    #region 블럭 애니메이션
    /*
    public IEnumerator blocks_Sh(StoryBlocks sb)
    {
        StoryCam.Inst.CamMove = false;
        Vector3 pos = sb.transform.position;

        for (int i = 0; i < sh_num; i++)
        {
            sb.transform.position = new Vector3(pos.x + 0.1f, pos.y, pos.z);
            yield return new WaitForSeconds(sh_speed);
            sb.transform.position = new Vector3(pos.x - 0.1f, pos.y, pos.z);
            yield return new WaitForSeconds(sh_speed);
            if(sh_speed - sh_speed_di > 0)
                sh_speed -= sh_speed_di;
        }
    }
    */
    #endregion
    #region 프리팹 추가
    public Story PopItem()
    {
        Story story = storySO.stories[SpawnAddrass];
        SpawnAddrass++;

        return story;
    }

    void AddCard()
    {
        string name_dummy = Player.Inst.playerdata.storyList[SpawnAddrass].name;
        if (name_dummy != null && name_dummy != "")
        {
            var storyObject = Instantiate(StoryPrefab, StorySpawnPoint[SpawnAddrass].position, Utils.QI);
            var storycontent = storyObject.GetComponent<StoryBlocks>();
            storyObject.transform.SetParent(this.transform, true);
            storycontent.SetUp(PopItem());

            if (StoryLists.Count < SpawnAddrass)
            {
                while (StoryLists.Count != SpawnAddrass)
                {
                    StoryBlocks sb = null;
                    StoryLists.Add(sb);
                }
            }
            StoryLists[SpawnAddrass-1] = storycontent;

            SetOriginOrder();

            if (Adnumber[SpawnAddrass - 1] != 1)
                storycontent.SetAct();
        }
        else
            SpawnAddrass++;
    }

    void SetOriginOrder()
    {
        for (int i = 0; i < StoryLists.Count; i++)
        {
            if(StoryLists[i] != null)
                StoryLists[i].GetComponent<Order>().SetOriginOrder(i);
        }
    }
    #endregion
}
