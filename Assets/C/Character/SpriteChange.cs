using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteChange : MonoBehaviour
{
    public static SpriteChange Inst { get; private set; }
    void Awake() => Inst = this;

    [SerializeField] Animator anim; //ÀÔ

    [SerializeField] SpriteRenderer main;
    [SerializeField] SpriteRenderer eyes;
    [SerializeField] SpriteRenderer brows;
    [SerializeField] SpriteRenderer mouth;

    [Header("½ÉÆÇ°ü ÇÇºÎ")]
    [SerializeField] Sprite[] scales_skin;
    [SerializeField] Sprite[] obsession_skin;

    [Header("½ÉÆÇ°ü ´«")]
    [SerializeField] Sprite[] scales_eyes; 
    [SerializeField] Sprite[] obsession_eyes; 

    [Header("½ÉÆÇ°ü ´«½ç")]
    [SerializeField] Sprite[] scales_brows;
    [SerializeField] Sprite[] obsession_brows;

    [Header("½ÉÆÇ°ü ÀÔ")]
    [SerializeField] Sprite[] scales_mouth;
    [SerializeField] Sprite[] obsession_mouth;
    #region ºñ´Ã
    #region ºñ´Ã ÇÇºÎ
    public void scales_Skin_1() => main.sprite = scales_skin[0];
    public void scales_Skin_2() => main.sprite = scales_skin[1];
    #endregion
    #region ºñ´Ã ´«¸ð¾ç
    public void scales_Eyes_1() => eyes.sprite = scales_eyes[0];
    public void scales_Eyes_2() => eyes.sprite = scales_eyes[1];
    public void scales_Eyes_3() => eyes.sprite = scales_eyes[2];
    public void scales_Eyes_4() => eyes.sprite = scales_eyes[3];
    public void scales_Eyes_5() => eyes.sprite = scales_eyes[4];
    #endregion
    #region ºñ´Ã ´«½ç¸ð¾ç
    public void scales_Brows_1() => brows.sprite = scales_brows[0];
    public void scales_Brows_2() => brows.sprite = scales_brows[1];
    #endregion
    #region ºñ´Ã ÀÔ¸ð¾ç
    public void scales_Mouth_1() => mouth.sprite = scales_mouth[0];
    public void scales_Mouth_2() => mouth.sprite = scales_mouth[1];
    public void scales_Mouth_3() => mouth.sprite = scales_mouth[2];
    #endregion
    #endregion
    #region ÁýÂø
    #region ÁýÂø ¼¼Æ®
    public void obsession_Set_1()
    {
        brows.sprite = null;

        //StopCoroutine(CloseEyes());

        Talk_FALSE();
        obsession_Skin_2();
        obsession_Eyes_5();
        obsession_Mouth_4();
        obsession_Step_TRUE();
    }
    #endregion
    #region ÁýÂø ÇÇºÎ
    public void obsession_Skin_1() => main.sprite = obsession_skin[0];
    public void obsession_Skin_2() => main.sprite = obsession_skin[1];
    #endregion
    #region ÁýÂø ´«¸ð¾ç
    public void obsession_Eyes_1() => eyes.sprite = obsession_eyes[0]; // ±âº»°ª
    public void obsession_Eyes_2() => eyes.sprite = obsession_eyes[1]; // »ìÂ¦ ¿Ã¶ó°£ ´«
    public void obsession_Eyes_3() => eyes.sprite = obsession_eyes[2]; // °¨Àº ´«
    public void obsession_Eyes_4() => eyes.sprite = obsession_eyes[3]; // ¿ô´Â ´«
    public void obsession_Eyes_5() => eyes.sprite = obsession_eyes[4]; // ¼­·ù ´«
    public void obsession_Eyes_6() => eyes.sprite = obsession_eyes[5]; // °¨Àº ´«
    #endregion
    #region ÁýÂø ´«½ç¸ð¾ç
    public void obsession_Brows_1() => brows.sprite = obsession_brows[0]; //±âº»
    public void obsession_Brows_2() => brows.sprite = obsession_brows[1]; //¤µ
    public void obsession_Brows_3() => brows.sprite = obsession_brows[2]; //À§·Î
    #endregion
    #region ÁýÂø ÀÔ¸ð¾ç
    public void obsession_Mouth_1() => mouth.sprite = obsession_mouth[0];
    public void obsession_Mouth_2() => mouth.sprite = obsession_mouth[1];
    public void obsession_Mouth_3() => mouth.sprite = obsession_mouth[2];
    public void obsession_Mouth_4() => mouth.sprite = obsession_mouth[3];
    #endregion
    #endregion
    #region ¿òÁ÷ÀÓ - ÀÔ
    public void scales_TRUE() 
    {
        Talk_FALSE();
        anim.SetBool("isScales", true); 
    }
    public void obsession_TRUE()
    {
        Talk_FALSE();
        anim.SetBool("isObsession", true);
    }
    public void obsession_Step_TRUE()
    {
        Talk_FALSE();
        anim.SetBool("isObsession", true);
        anim.SetBool("isStep", true);
    }
    public void Talk_FALSE() 
    {
        anim.SetBool("isScales", false);
        anim.SetBool("isObsession", false);
        anim.SetBool("isStep", false);
    }
    public void Talk_ing() => anim.SetBool("isTalk", true);
    public void Talk_end() => anim.SetBool("isTalk", false);
    #endregion
    #region Ä³¸¯ÅÍ ÀÔÀå
    public void Default_Clear()
    {
        Talk_FALSE();
        main.sprite = null;
        eyes.sprite = null;
        brows.sprite = null;
        mouth.sprite = null;
    }
    public void scales_Default()
    {
        SoundPlayer.Inst.BGM_START("ºñ´Ã_±âº»");

        //StopCoroutine(CloseEyes());

        scales_TRUE();
        scales_Skin_1();
        scales_Eyes_2();
        scales_Brows_1();
    }

    public void obsession_Default()
    {
        SoundPlayer.Inst.BGM_START("ÁýÂø_±âº»");

        //StopCoroutine(CloseEyes());

        obsession_TRUE();
        obsession_Skin_1();
        obsession_Eyes_1();
        obsession_Brows_1();
    }
    #endregion

    void Start()
    {
        SoundPlayer.Inst.Clear_BgSound();
    }

    /*
    float EyeClose = 0;
    void Update()
    {
        EyeClose += Time.deltaTime;
        if(EyeClose > 3)
            StartCoroutine(CloseEyes());
    }

    IEnumerator CloseEyes()
    {
        EyeClose = 0;
        if (eyes.sprite == scales_eyes[1])
        {
            scales_Eyes_1();
            yield return new WaitForSeconds(0.1f);
            scales_Eyes_2();
        }
        else if (eyes.sprite == scales_eyes[2])
        {
            scales_Eyes_1();
            yield return new WaitForSeconds(0.1f);
            scales_Eyes_3();
        }
        else if (eyes.sprite == scales_eyes[3])
        {
            scales_Eyes_1();
            yield return new WaitForSeconds(0.1f);
            scales_Eyes_4();
        }
        else if (eyes.sprite == scales_eyes[4])
        {
            scales_Eyes_1();
            yield return new WaitForSeconds(0.1f);
            scales_Eyes_5();
        }

        if (eyes.sprite == obsession_eyes[0])
        {
            obsession_Eyes_3();
            yield return new WaitForSeconds(0.1f);
            obsession_Eyes_1();
        }
        else if(eyes.sprite == obsession_eyes[1])
        {
            obsession_Eyes_3();
            yield return new WaitForSeconds(0.1f);
            obsession_Eyes_2();
        }
        else if (eyes.sprite == obsession_eyes[4])
        {
            obsession_Eyes_6();
            yield return new WaitForSeconds(0.1f);
            obsession_Eyes_5();
        }
    }
    */
}
