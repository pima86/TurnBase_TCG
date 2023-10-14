using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rightFade : MonoBehaviour
{
    public bool leftright;
    bool wait = true;
    void OnMouseOver()
    {
        if (wait)
        {
            if (leftright && ListManager.Inst.size != ListCardManager.Inst.startCardCount)
                FadeOutInList.Inst.StartCoroutine("FadeInLeft");
            else if (!leftright && ListManager.Inst.size < Player.Inst.playerdata.CardCollect.Count)
                FadeOutInList.Inst.StartCoroutine("FadeInRight");
        }
    }

    void OnMouseExit()
    {
        FadeOutInList.Inst.Tempbild();
    }

    void OnMouseDown()
    {
        if (OnClickList.Inst.NextListBool && ListManager.Inst.ListEnd == false && !leftright)
        {
            OnClickList.Inst.OnclickNextList();
        }
        else if (OnClickList.Inst.PreviousListBool && ListManager.Inst.size != ListCardManager.Inst.startCardCount && leftright)
        {
            OnClickList.Inst.OnclickPreviousList();
        }

        wait = false;
        FadeOutInList.Inst.Tempbild();
        Invoke("OKwait", 0.5f);
    }

    void OKwait()
    {
        wait = true;
    }
}
