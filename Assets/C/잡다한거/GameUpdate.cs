using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUpdate : MonoBehaviour
{
    //ī�� ������ ������� �� SO�� ��õ���� �߾���޴µ�
    //�װ� Player ���̺� ���Ͽ� ���̹��� ���� �����̰�
    //��ġ�� �ص� ���̺� ������ ī�� ������ ��������
    //�������� "�� �� �����̱���"��� �� ������ ������ ������ ���θ����� �ڵ�.
    //���߿� �ð��Ǹ� �ҽ� �ٲٷ�.. �̰� ��..ġƮ���ּ��� �ϴ� ���̳�..

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
