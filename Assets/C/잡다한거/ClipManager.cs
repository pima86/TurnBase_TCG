using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClipManager : MonoBehaviour
{
    public static ClipManager Inst { get; private set; }
    void Awake()
    {
        if (Inst == null)
        {
            Inst = this;
            DontDestroyOnLoad(Inst);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //ClipManager.Inst.hit1();

    [Header("������ ����")]
    [SerializeField] public AudioClip[] clip;

    public void GateOpenSound() => SoundPlayer.Inst.SFXPlay("GateOpen", clip[1]);
    public void GateCloseSound() => SoundPlayer.Inst.SFXPlay("GateClose", clip[2]);

    public void GateLight() => SoundPlayer.Inst.SFXPlay("GateLight", clip[3]);
    public void GameStart() => SoundPlayer.Inst.SFXPlay("GameStart", clip[4]);

    public void BigGateOpenSound() => SoundPlayer.Inst.SFXPlay("BigGateOpen", clip[5]);
    public void BigGateCloseSound() => SoundPlayer.Inst.SFXPlay("BigGateClose", clip[6]);

    [Header("���� ����")]
    [SerializeField] public AudioClip[] cardclip;

    public void CardDrow() => SoundPlayer.Inst.SFXPlay("CardDrow", cardclip[0]);
    public void CardBurn() => SoundPlayer.Inst.SFXPlay("CardBurn", cardclip[1]);
    public void hit1() => SoundPlayer.Inst.SFXPlay("hit1", cardclip[2]);
    public void hit2() => SoundPlayer.Inst.SFXPlay("hit2", cardclip[3]);
    public void hit3() => SoundPlayer.Inst.SFXPlay("hit3", cardclip[4]);
    public void hit4() => SoundPlayer.Inst.SFXPlay("hit4", cardclip[5]);

    [Header("������ ����")]
    [SerializeField] public AudioClip[] Slimeclip;

    public void Slime_1() => SoundPlayer.Inst.SFXPlay("Slime_1", Slimeclip[0]); //�̵�
    public void Slime_2() => SoundPlayer.Inst.SFXPlay("Slime_2", Slimeclip[1]); //�ǰ�
    public void Slime_3() => SoundPlayer.Inst.SFXPlay("Slime_3", Slimeclip[2]); //Ÿ��

    [Header("�� ����")]
    [SerializeField] public AudioClip[] Mouseclip;

    public void Mouse_1() => SoundPlayer.Inst.SFXPlay("Mouse_1", Mouseclip[0]); //�̵�
    public void Mouse_2() => SoundPlayer.Inst.SFXPlay("Mouse_2", Mouseclip[1]); //�ǰ�
    public void Mouse_3() => SoundPlayer.Inst.SFXPlay("Mouse_3", Mouseclip[2]); //Ÿ��
}
