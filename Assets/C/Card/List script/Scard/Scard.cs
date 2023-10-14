using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using DG.Tweening;

public class Scard : MonoBehaviour
{
    [SerializeField] ItemSO itemSO;

    [SerializeField] SpriteRenderer side;
    [SerializeField] public TMP_Text name;
    [SerializeField] public TMP_Text cost;
    [SerializeField] TMP_Text percent;

    public int range; //근거리인지 원거리인지

    public Item item;
    public PRS originPRS;

    public void SetUp(Item item)
    {
        //this.item = item;

        name.text = item.name;
        cost.text = item.cost.ToString();
        percent.text = "x " + item.percent.ToString();

        range = this.item.range;

        for (int i = 0; i < itemSO.items.Count; i++)
        {
            if (name.text == itemSO.items[i].name)
                side.sprite = itemSO.items[i].side;
        }
    }

    public int num;
    void Update()
    {
        PercentFIx();
        if (NowOver && EventSystem.current.IsPointerOverGameObject() == false)
        {
            if (Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(0))
            {
                ListCardManager.Inst.AddrassDownList(name.text, this);
            }
        }

    }

    bool NowOver = false;
    void OnMouseOver()
    {
        NowOver = true;
    }
    void OnMouseExit()
    {
        NowOver = false;
    }

    public void Destro()
    {
        Destroy(gameObject);
    }

    void PercentFIx()
    {
        for (int i = 0; i < ListCardManager.Inst.Samples.Count; i++)
        {
            if (ListCardManager.Inst.Samples[i].name == name.text)
                percent.text = "x " + ListCardManager.Inst.Samples[i].percent;
        }
    }

    public void MoveTransform(PRS prs, bool useDotween, float dotweenTime = 0)
    {
        if (useDotween)
        {
            transform.DOMove(prs.pos, dotweenTime);
            transform.DORotateQuaternion(prs.rot, dotweenTime);
            transform.DOScale(prs.scale, dotweenTime);
        }
        else
        {
            transform.position = prs.pos;
            transform.rotation = prs.rot;
            transform.localScale = prs.scale;
        }
    }
}