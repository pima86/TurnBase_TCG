using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;



public class ButtenLight : MonoBehaviour , IPointerEnterHandler
{
    public int num;
    [SerializeField] TMP_Text sel_text;

    public void OnPointerEnter(PointerEventData eventData)
    {
        /*
        if (TextBox.Inst.Bracket_bool)
        {
            if (GameObject.Find("하단 검정배경").GetComponent<TextBox>().Tt_1.text != "" && num == 1)
            {
                GameObject.Find("하단 검정배경").GetComponent<TextBox>().Bracket_point = 1;
            }
            if (GameObject.Find("하단 검정배경").GetComponent<TextBox>().Tt_2.text != "" && num == 2)
            {
                GameObject.Find("하단 검정배경").GetComponent<TextBox>().Bracket_point = 2;
            }
            if (GameObject.Find("하단 검정배경").GetComponent<TextBox>().Tt_3.text != "" && num == 3)
            {
                GameObject.Find("하단 검정배경").GetComponent<TextBox>().Bracket_point = 3;
            }
        }
        */
    }

    public void Enter()
    {
        if (sel_text.color != new Color(1,1,1,0))
        {
            if (GameObject.Find("하단 검정배경").GetComponent<TextBox>().Tt_1.text != "" && num == 1)
            {
                GameObject.Find("하단 검정배경").GetComponent<TextBox>().Bracket_point = 1;
            }
            if (GameObject.Find("하단 검정배경").GetComponent<TextBox>().Tt_2.text != "" && num == 2)
            {
                GameObject.Find("하단 검정배경").GetComponent<TextBox>().Bracket_point = 2;
            }
            if (GameObject.Find("하단 검정배경").GetComponent<TextBox>().Tt_3.text != "" && num == 3)
            {
                GameObject.Find("하단 검정배경").GetComponent<TextBox>().Bracket_point = 3;
            }
        }
    }
}
