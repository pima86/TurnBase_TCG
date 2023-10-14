using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClickList : MonoBehaviour
{
    public static OnClickList Inst { get; private set; }
    void Awake() => Inst = this;


    public void OnclickNextList()
    {
        if (NextListBool && ListManager.Inst.ListEnd == false)
            StartCoroutine("NextList");
    }

    public bool NextListBool = true;
    IEnumerator NextList()
    {
        NextListBool = false;

        yield return new WaitForSeconds(0.0001f);
        ListManager.Inst.NextList();
        StartCoroutine(ListCardManager.Inst.StartGameCo());

        NextListBool = true;
    }

    public void OnclickPreviousList()
    {
        if (PreviousListBool && ListManager.Inst.size != ListCardManager.Inst.startCardCount)
            StartCoroutine("PreviousList");
    }

    public bool PreviousListBool = true;
    IEnumerator PreviousList()
    {
        PreviousListBool = false;

        yield return new WaitForSeconds(0.0001f);
        ListManager.Inst.NextList();
        ListManager.Inst.MinusList();
        StartCoroutine(ListCardManager.Inst.StartGameCo());

        PreviousListBool = true;
    }

    public void OnclickSaveDeck()
    {
        ListCardManager.Inst.SaveThisDeck();
        Debug.Log("ÀúÀå‰Î");
    }
}
