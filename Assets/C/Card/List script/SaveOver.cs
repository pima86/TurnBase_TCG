using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SaveOver : MonoBehaviour
{
    [SerializeField] GameObject obj;
    [SerializeField] Vector3 pos;


    IEnumerator coroutine;
    Vector3 originpos;
    void Start()
    {
        originpos = obj.transform.position;
    }

    void OnMouseDown()
    {
        if (int.Parse(ListCardManager.Inst.DeckCount.text) >= 15)
        {
            OnClickList.Inst.OnclickSaveDeck();
            Player.Inst.Save();
            coroutine = delayTip();
            StopCoroutine(coroutine);
            StartCoroutine(coroutine);
        }
        else
            Debug.Log("덱은 최소 15장으로 이루어져야 합니다.");
    }

    IEnumerator delayTip()
    {
        DOTween.Kill(obj.transform);
        obj.transform.DOMove(originpos + pos, 0.5f);
        yield return new WaitForSeconds(1.5f);
        DOTween.Kill(obj.transform);
        obj.transform.DOMove(originpos, 0.5f);
    }
}
