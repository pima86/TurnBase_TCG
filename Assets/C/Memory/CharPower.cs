using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharPower : MonoBehaviour
{
    public static CharPower Inst { get; private set; }
    void Awake() => Inst = this;

    [SerializeField] TMP_Text player_HP; //ü��
    [SerializeField] TMP_Text player_STRONG; //����
    [SerializeField] TMP_Text player_PTYPE1; //����
    [SerializeField] TMP_Text player_PTYPE2; //Ÿ��
    [SerializeField] TMP_Text player_MAXMANA; //���� �ִ�ġ
    [SerializeField] TMP_Text player_REMANA; //���� ���ġ
    [SerializeField] TMP_Text player_MOVE; //���� ���ġ

    /*
    public int hp;
    public int power; //��
    public int ptype1; //����
    public int ptype2; //Ÿ��
    public int strong; //����
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
        //ü��
        player_HP.text = Player_UseItem.Inst.Out_Set("ü��").ToString();

        player_STRONG.text = Player_UseItem.Inst.Out_Set("����").ToString();

        //���ݷ�
        player_PTYPE1.text = Player_UseItem.Inst.Out_Set("����").ToString();
        player_PTYPE2.text = Player_UseItem.Inst.Out_Set("Ÿ��").ToString();

        //����
        player_MAXMANA.text = Player_UseItem.Inst.Out_Set("���� �ִ�ġ").ToString();
        player_REMANA.text = Player_UseItem.Inst.Out_Set("���� ���").ToString();

        //������
        player_MOVE.text = Player_UseItem.Inst.Out_Set("�̵� ����Ʈ").ToString();
    }
}
