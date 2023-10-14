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

    int player_HP; //체력
    int player_HTYPE; //참격내구

    int player_PTYPE1; //참격
    int player_PTYPE2; //타격

    int player_MAXMANA; //마나 최대치
    int player_REMANA; //마나 재생치
    int player_MOVE; //이동 재생치

    void Start()
    {
        Invoke("Invoke_Start", 0.01f);
    }

    void Invoke_Start()
    {
        box = Player.Inst.playerdata.box_1;

        //체력
        player_HP = Player.Inst.playerdata.hp;

        //내구
        player_HTYPE = Player.Inst.playerdata.htype;

        //공격력
        player_PTYPE1 = Player.Inst.playerdata.ptype1;
        player_PTYPE2 = Player.Inst.playerdata.ptype2;

        //마나
        player_MAXMANA = Player.Inst.playerdata.maxmana;
        player_REMANA = Player.Inst.playerdata.remana;

        //움직임
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
            
            if (acc.effect[i].Contains("체력"))
            {
                string name = acc.effect[i].Substring(2);
                bool plus = name.Contains("+");
                int num = int.Parse(name.Substring(1));

                PowerUp("체력", plus, num);
            }
            else if (acc.effect[i].Contains("내구"))
            {
                string name = acc.effect[i].Substring(2);
                bool plus = name.Contains("+");
                int num = int.Parse(name.Substring(1));

                PowerUp("내구", plus, num);
            }
            else if (acc.effect[i].Contains("참격"))
            {
                string name = acc.effect[i].Substring(2);
                bool plus = name.Contains("+");
                int num = int.Parse(name.Substring(1));

                PowerUp("참격", plus, num);
            }
            else if (acc.effect[i].Contains("타격"))
            {
                string name = acc.effect[i].Substring(2);
                bool plus = name.Contains("+");
                int num = int.Parse(name.Substring(1));

                PowerUp("타격", plus, num);
            }
            else if (acc.effect[i].Contains("마나 최대치"))
            {
                string name = acc.effect[i].Substring(6);
                bool plus = name.Contains("+");
                int num = int.Parse(name.Substring(1));

                PowerUp("마나 최대치", plus, num);
            }
            else if (acc.effect[i].Contains("마나 재생"))
            {
                string name = acc.effect[i].Substring(5);
                bool plus = name.Contains("+");
                int num = int.Parse(name.Substring(1));

                PowerUp("마나 재생", plus, num);
            }
            else if (acc.effect[i].Contains("이동 포인트"))
            {
                string name = acc.effect[i].Substring(6);
                bool plus = name.Contains("+");
                int num = int.Parse(name.Substring(1));

                PowerUp("이동 포인트", plus, num);
            }
        }
    }

    void PowerUp(string name, bool plus, int num)
    {
        if (name == "체력")
        {
            if (plus)
                player_HP += num;
            else
                player_HP -= num;
        }
        else if (name == "내구")
        {
            if (plus)
                player_HTYPE += num;
            else
                player_HTYPE -= num;
        }
        else if (name == "참격")
        {
            if (plus)
                player_PTYPE1 += num;
            else
                player_PTYPE1 -= num;
        }
        else if (name == "타격")
        {
            if (plus)
                player_PTYPE2 += num;
            else
                player_PTYPE2 -= num;
        }
        else if (name == "마나 최대치")
        {
            if (plus)
                player_MAXMANA += num;
            else
                player_MAXMANA -= num;
        }
        else if (name == "마나 재생")
        {
            if (plus)
                player_REMANA += num;
            else
                player_REMANA -= num;
        }
        else if (name == "이동 포인트")
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

        if (name == "체력")
            return player_HP;
        else if (name == "내구")
            return player_HTYPE;
        else if (name == "참격")
            return player_PTYPE1;
        else if (name == "타격")
            return player_PTYPE2;
        else if (name == "마나 최대치")
            return player_MAXMANA;
        else if (name == "마나 재생")
            return player_REMANA;
        else if (name == "이동 포인트")
            return player_MOVE;

        return 0;
    }
}
