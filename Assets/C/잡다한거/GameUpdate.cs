using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUpdate : MonoBehaviour
{
    //카드 정보를 끌어오는 걸 SO를 원천으로 했어야햇는데
    //그걸 Player 세이브 파일에 들이박은 나는 병신이고
    //패치를 해도 세이브 파일의 카드 정보를 가져오니
    //이제서야 "아 난 병신이구나"라는 걸 깨달은 새끼의 하찮은 몸부림같은 코드.
    //나중에 시간되면 소스 바꾸렴.. 이건 뭐..치트써주세요 하는 꼴이네..

    [SerializeField] ItemSO itemSO;

    void Start()
    {
        Invoke("rr", 0.1f);
    }

    void rr()
    {
        sibar(Player.Inst.playerdata.CardCollect);
        sibar(Player.Inst.playerdata.Decks_box_1);
        sibar(Player.Inst.playerdata.Decks_box_2);
        sibar(Player.Inst.playerdata.Decks_box_3);
        sibar(Player.Inst.playerdata.Decks_box_4);
        sibar(Player.Inst.playerdata.Decks_box_5);
        sibar(Player.Inst.playerdata.Decks_box_6);
        sibar(Player.Inst.playerdata.Decks_box_7);
        sibar(Player.Inst.playerdata.Decks_box_8);
    }

    void sibar(List<Item> temp)
    {
        ItemSO item = Instantiate(itemSO);

        for (int i = 0; i < temp.Count; i++)
        {
            int j = item.items.FindIndex(x => x.name == temp[i].name);
            if (j != -1)
            {
                temp[i].illust = item.items[j].illust;
                temp[i].side = item.items[j].side;
                temp[i].cost = item.items[j].cost;
                temp[i].attack = item.items[j].attack;
                temp[i].defense = item.items[j].defense;
                temp[i].type = item.items[j].type;
                temp[i].effect = item.items[j].effect;
                temp[i].range = item.items[j].range;
            }
        }
    }
}
