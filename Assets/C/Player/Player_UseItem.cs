using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player_UseItem : MonoBehaviour
{
    public static Player_UseItem Inst { get; private set; }
    void Awake() => Inst = this;

    [SerializeField] AccSO accSO;
    List<string> box;

    int player_HP; //ü��
    int player_HTYPE; //���ݳ���

    int player_PTYPE1; //����
    int player_PTYPE2; //Ÿ��

    int player_MAXMANA; //���� �ִ�ġ
    int player_REMANA; //���� ���ġ
    int player_MOVE; //�̵� ���ġ

    void Start()
    {
        Invoke("Invoke_Start", 0.01f);
    }

    void Invoke_Start()
    {
        box = Player.Inst.playerdata.box_1;

        //ü��
        player_HP = Player.Inst.playerdata.hp;

        //����
        player_HTYPE = Player.Inst.playerdata.htype;

        //���ݷ�
        player_PTYPE1 = Player.Inst.playerdata.ptype1;
        player_PTYPE2 = Player.Inst.playerdata.ptype2;

        //����
        player_MAXMANA = Player.Inst.playerdata.maxmana;
        player_REMANA = Player.Inst.playerdata.remana;

        //������
        player_MOVE = Player.Inst.playerdata.move;


        for (int i = 0; i < box.Count; i++)
        {
            int index = accSO.acc.FindIndex(x => x.name == box[i]);
            if(index != -1)
                Item_Set(accSO.acc[index]);
        }
    }

    void Item_Set(Acc acc)
    {
        for (int i = 0; i < acc.effect.Length; i++)
        {
            
            if (acc.effect[i].Contains("ü��"))
            {
                string name = acc.effect[i].Substring(2);
                bool plus = name.Contains("+");
                int num = int.Parse(name.Substring(1));

                PowerUp("ü��", plus, num);
            }
            else if (acc.effect[i].Contains("����"))
            {
                string name = acc.effect[i].Substring(2);
                bool plus = name.Contains("+");
                int num = int.Parse(name.Substring(1));

                PowerUp("����", plus, num);
            }
            else if (acc.effect[i].Contains("����"))
            {
                string name = acc.effect[i].Substring(2);
                bool plus = name.Contains("+");
                int num = int.Parse(name.Substring(1));

                PowerUp("����", plus, num);
            }
            else if (acc.effect[i].Contains("Ÿ��"))
            {
                string name = acc.effect[i].Substring(2);
                bool plus = name.Contains("+");
                int num = int.Parse(name.Substring(1));

                PowerUp("Ÿ��", plus, num);
            }
            else if (acc.effect[i].Contains("���� �ִ�ġ"))
            {
                string name = acc.effect[i].Substring(6);
                bool plus = name.Contains("+");
                int num = int.Parse(name.Substring(1));

                PowerUp("���� �ִ�ġ", plus, num);
            }
            else if (acc.effect[i].Contains("���� ���"))
            {
                string name = acc.effect[i].Substring(5);
                bool plus = name.Contains("+");
                int num = int.Parse(name.Substring(1));

                PowerUp("���� ���", plus, num);
            }
            else if (acc.effect[i].Contains("�̵� ����Ʈ"))
            {
                string name = acc.effect[i].Substring(6);
                bool plus = name.Contains("+");
                int num = int.Parse(name.Substring(1));

                PowerUp("�̵� ����Ʈ", plus, num);
            }
        }
    }

    void PowerUp(string name, bool plus, int num)
    {
        if (name == "ü��")
        {
            if (plus)
                player_HP += num;
            else
                player_HP -= num;
        }
        else if (name == "����")
        {
            if (plus)
                player_HTYPE += num;
            else
                player_HTYPE -= num;
        }
        else if (name == "����")
        {
            if (plus)
                player_PTYPE1 += num;
            else
                player_PTYPE1 -= num;
        }
        else if (name == "Ÿ��")
        {
            if (plus)
                player_PTYPE2 += num;
            else
                player_PTYPE2 -= num;
        }
        else if (name == "���� �ִ�ġ")
        {
            if (plus)
                player_MAXMANA += num;
            else
                player_MAXMANA -= num;
        }
        else if (name == "���� ���")
        {
            if (plus)
                player_REMANA += num;
            else
                player_REMANA -= num;
        }
        else if (name == "�̵� ����Ʈ")
        {
            if (plus)
                player_MOVE += num;
            else
                player_MOVE -= num;

            if (player_MOVE > 3)
                player_MOVE = 3;
        }
    }

    

    public int Out_Set(string name)
    {
        Invoke_Start();

        if (name == "ü��")
            return player_HP;
        else if (name == "����")
            return player_HTYPE;
        else if (name == "����")
            return player_PTYPE1;
        else if (name == "Ÿ��")
            return player_PTYPE2;
        else if (name == "���� �ִ�ġ")
            return player_MAXMANA;
        else if (name == "���� ���")
            return player_REMANA;
        else if (name == "�̵� ����Ʈ")
            return player_MOVE;

        return 0;
    }
}
