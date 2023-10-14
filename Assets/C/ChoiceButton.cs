using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChoiceButton : MonoBehaviour
{
    public GameObject MouseEv;

    private string tt;

    public void Choice()
    {
        if (TextBox.Inst.Bracket_bool)
        {
            if (gameObject.GetComponent<TMP_Text>().text != "")
            {
                GameObject.Find("하단 검정배경").GetComponent<TextBox>().Bracket_point = 0;
                GameObject.Find("managerGame").GetComponent<TypeEffect>().Sel = false;
                tt = gameObject.GetComponent<TextMeshProUGUI>().text;
                MouseEv.GetComponent<MouseEvent>().Chapter(tt);
            }
        }
    }
}
