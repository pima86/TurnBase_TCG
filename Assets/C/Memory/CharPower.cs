using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharPower : MonoBehaviour
{
    public static CharPower Inst { get; private set; }
    void Awake() => Inst = this;

    [SerializeField] TMP_Text player_HP; //체력
    [SerializeField] TMP_Text player_STRONG; //내구
    [SerializeField] TMP_Text player_PTYPE1; //참격
    [SerializeField] TMP_Text player_PTYPE2; //타격
    [SerializeField] TMP_Text player_MAXMANA; //마나 최대치
    [SerializeField] TMP_Text player_REMANA; //마나 재생치
    [SerializeField] TMP_Text player_MOVE; //마나 재생치

    /*
    public int hp;
    public int power; //힘
    public int ptype1; //참격
    public int ptype2; //타격
    public int strong; //내구
    public int maxmana;
    public int remana;
    public int move;
    */
    void Start()
    {
        Invoke("Invoke_Start", 0.01f);
    }

    public void Invoke_Start()
    {
        //체력
        player_HP.text = Player_UseItem.Inst.Out_Set("체력").ToString();

        player_STRONG.text = Player_UseItem.Inst.Out_Set("내구").ToString();

        //공격력
        player_PTYPE1.text = Player_UseItem.Inst.Out_Set("참격").ToString();
        player_PTYPE2.text = Player_UseItem.Inst.Out_Set("타격").ToString();

        //마나
        player_MAXMANA.text = Player_UseItem.Inst.Out_Set("마나 최대치").ToString();
        player_REMANA.text = Player_UseItem.Inst.Out_Set("마나 재생").ToString();

        //움직임
        player_MOVE.text = Player_UseItem.Inst.Out_Set("이동 포인트").ToString();
    }
}
