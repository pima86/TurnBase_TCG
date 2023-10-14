using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BlockSearch : MonoBehaviour
{
    public static BlockSearch Inst { get; private set; }
    void Awake() => Inst = this;

    [SerializeField] StoryMonsterSO smSO;
    [SerializeField] MonsterSO monsterSO;

    #region 교전용 변수
    [SerializeField] Image icon;
    [SerializeField] Image illust;
    [SerializeField] TMP_Text name;
    [SerializeField] TMP_Text name_content;
    [SerializeField] TMP_Text story_content;
    [SerializeField] TMP_Text content;

    [SerializeField] GameObject leftButten;
    [SerializeField] GameObject rightButten;
    #endregion

    #region 스토리용 변수
    [SerializeField] Image icon_1;
    [SerializeField] TMP_Text name_1;
    [SerializeField] TMP_Text content_1;
    #endregion
    [SerializeField] GameObject[] UIAct;

    public void NameSetting(string name, string kind, Sprite sp, string story = "")
    {
        MonsterNum = 0;

        if (kind == "스토리")
        {
            UIAct[0].SetActive(true);
            UIAct[1].SetActive(false);

            this.name_1.text = name;
            icon_1.sprite = sp;
            content_1.text = story;
        }
        else
        {
            UIAct[0].SetActive(false);
            UIAct[1].SetActive(true);

            this.name.text = name;
            icon.sprite = sp;

            for (int i = 0; i < smSO.sm.Length; i++)
            {
                if (smSO.sm[i].name == name)
                {
                    MonsterList.Clear();
                    for (int j = 0; j < smSO.sm[i].monster.Length; j++)
                    {
                        if(smSO.sm[i].monster[j] != "")
                            MonsterList.Add(smSO.sm[i].monster[j]);
                    }
                }
            }
        }
    }

    public void SetUp(Monster mb)
    {
        illust.sprite = mb.illust;
        

        name_content.text = mb.name;
        story_content.text = mb.story;
        content.text = 
            "세부사항\n" +
            "-서식지:" + mb.live + "\n" +
            "-등급:" + mb.rating + "\n" +
            "\n" +
            "특이사항\n" +
            mb.passive_1 + "\n" +
            mb.passive_2 + "\n" +
            mb.passive_3 + "\n";
    }

    public List<string> MonsterList;
    public int MonsterNum = 0;
    void Update()
    {
        if (MonsterList.Count != 0)
        {
            if (MonsterNum == 0)
                leftButten.SetActive(false);
            else
                leftButten.SetActive(true);
            if (MonsterNum == MonsterList.Count - 1)
                rightButten.SetActive(false);
            else
                rightButten.SetActive(true);

            NameWhere(MonsterList[MonsterNum]);
        }
    }

    void NameWhere(string monster)
    {
        if (monster.Length != 0)
        {
            string name = monster;
            for (int i = 0; i < monsterSO.monsters.Length; i++)
            {
                if (name == monsterSO.monsters[i].name)
                {
                    SetUp(monsterSO.monsters[i]);
                }
            }
        }
    }
    #region 버튼 역할
    public void MonsterNumleft()
    {
        if (0 < MonsterNum)
            MonsterNum--;
    }
    public void MonsterNumright()
    {
        if (MonsterList.Count > MonsterNum + 1)
            MonsterNum++;
    }
    #endregion
}
