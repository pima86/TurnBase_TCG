using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;
using TMPro;

public class ClickMob : MonoBehaviour
{
    public static ClickMob Inst { get; private set; }
    void Awake() => Inst = this;

    [SerializeField] MonsterSO monsterSO;
    [SerializeField] AccSO accSO;

    [SerializeField] SpriteRenderer icon;
    [SerializeField] TMP_Text name;

    [SerializeField] SpriteRenderer[] range_1;
    [SerializeField] SpriteRenderer[] range_2;
    [SerializeField] SpriteRenderer[] range_3;

    [SerializeField] TMP_Text[] patten;
    [SerializeField] TMP_Text[] effect;

    [SerializeField] List<Stat_Drop> item;
    [SerializeField] GameObject[] patten_panel;

    [SerializeField] GameObject Prefab;

    [SerializeField] Sprite[] RankIcon;

    [SerializeField] LeftCanvasPanel LeftPanel;

    Mob select;
    void Start()
    {
        patten_clear(range_1);
        patten_clear(range_2);
        patten_clear(range_3);
    }

    int test_patten = 0;
    void Update()
    {
        if (select != null)
        {
            if (test_patten != select.patten_num)
            {
                patten_panel_black(select.patten_num);
                test_patten = select.patten_num;
            }
        }
    }

    /*
    public void DropItem_List(string name) //상태창에 아이콘 띄우기용
    {
        for (int i = 0; i < item.Count; i++)
        {
            item[i].objDestroy();
        }
        item.Clear();

        string[] dropitem_str = DropItem_Mob.Inst.dropitem_icon_str(name);
        int[] dropitem_int = DropItem_Mob.Inst.dropitem_icon_int(name);
        for (int i = 0; i < dropitem_str.Length; i++)
        {
            int index = accSO.acc.FindIndex(x => x.name == dropitem_str[i]);
            
            var accObject = Instantiate(Prefab, new Vector3(0, 0, 0), Utils.QI, GameObject.Find("드랍목록").transform);
            var drop_item = accObject.GetComponent<Stat_Drop>();

            drop_item.SetUp(accSO.acc[index], dropitem_int[i]);//SO에서 해당 정보를 가져옵니다.
            item.Add(drop_item);//현재 인스턴스화된 프리팹 오브젝트들의 모임

            CardAlignment();
        }
    }
    */
    void CardAlignment()
    {
        List<Vector3> originPos = new List<Vector3>();
        originPos = RoundAlignment(item.Count);
        for (int i = 0; i < item.Count; i++)
        {
            item[i].ScaleThis(Vector3.one * 0.5f);
            item[i].ReturnGameObject().transform.localPosition = originPos[i];
        }
    }

    List<Vector3> RoundAlignment(int objCount)
    {
        float[] objLerps = new float[objCount];
        List<Vector3> results = new List<Vector3>(objCount);

        switch (objCount)
        {
            case 1: objLerps = new float[] { 0.25f }; break;
            case 2: objLerps = new float[] { 0.25f, 0.6f }; break;
            case 3: objLerps = new float[] { 0.25f, 0.6f, 0.25f }; break;
            case 4: objLerps = new float[] { 0.25f, 0.6f, 0.25f, 0.6f }; break;
            default:
                break;
        }

        for (int i = 0; i < objLerps.Length; i++)
        {
            if(i < 2)
                results.Add(new Vector3(1.2f, objLerps[i], 0));
            else
                results.Add(new Vector3(0.45f, objLerps[i], 0));
        }

        return results;
    }

    public void MobStat(Mob mob)
    {
        select = mob;

        patten_clear(range_1);
        patten_clear(range_2);
        patten_clear(range_3);

        patten_panel_black(mob.patten_num);

        int index = Array.FindIndex(monsterSO.monsters, x => x.name == mob.name.text);

        name.text = mob.name.text;

        patten[0].text = mob.patten_damage[0];
        patten[1].text = mob.patten_damage[1];
        patten[2].text = mob.patten_damage[2];

        patten_color(mob.patten_range[0], range_1);
        patten_color(mob.patten_range[1], range_2);
        patten_color(mob.patten_range[2], range_3);


        effect[0].text = mob.mob_effect[0];
        effect[1].text = mob.mob_effect[1];
        effect[2].text = mob.mob_effect[2];

        if (monsterSO.monsters[index].rating == "F")
            icon.sprite = RankIcon[0];
        else if (monsterSO.monsters[index].rating == "E")
            icon.sprite = RankIcon[1];
        else if (monsterSO.monsters[index].rating == "D")
            icon.sprite = RankIcon[2];
        else if (monsterSO.monsters[index].rating == "C")
            icon.sprite = RankIcon[3];
        else if (monsterSO.monsters[index].rating == "B")
            icon.sprite = RankIcon[4];
    }

    void patten_clear(SpriteRenderer[] range)
    {
        for (int i = 0; i < range.Length; i++)
            range[i].color = new Color(125/255f, 125 / 255f, 125 / 255f, 1f);
    }

    void patten_color(int range, SpriteRenderer[] sprite)
    {
        for (int i = 0; i < range; i++)
            sprite[i].color = new Color(1f, 1f, 1f, 1f);
    }

    public void patten_panel_black(int num)
    {
        if (num == 0)
        {
            patten_panel[0].SetActive(true);
            patten_panel[1].SetActive(true);
            patten_panel[2].SetActive(true);
        }
        else if (num == 1)
        {
            patten_panel[0].SetActive(false);
            patten_panel[1].SetActive(true);
            patten_panel[2].SetActive(true);
        }
        else if (num == 2)
        {
            patten_panel[0].SetActive(true);
            patten_panel[1].SetActive(false);
            patten_panel[2].SetActive(true);
        }
        else if (num == 3)
        {
            patten_panel[0].SetActive(true);
            patten_panel[1].SetActive(true);
            patten_panel[2].SetActive(false);
        }
    }

    public void AllClear()
    {
        LeftPanel.Off_Move();

        name.text = "";

        patten[0].text = "";
        patten[1].text = "";
        patten[2].text = "";

        effect[0].text = "";
        effect[1].text = "";
        effect[2].text = "";

        icon.sprite = null;

        patten_clear(range_1);
        patten_clear(range_2);
        patten_clear(range_3);

        patten_panel[0].SetActive(true);
        patten_panel[1].SetActive(true);
        patten_panel[2].SetActive(true);

        for (int i = 0; i < item.Count; i++)
        {
            item[i].objDestroy();
        }
        item.Clear();
    }
}
