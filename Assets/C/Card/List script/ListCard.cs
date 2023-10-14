using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class ListCard : MonoBehaviour
{
    [SerializeField] ItemSO itemSO;

    //카드 정보
    [SerializeField] SpriteRenderer card;
    [SerializeField] SpriteRenderer illust;
    [SerializeField] SpriteRenderer side;

    [SerializeField] SpriteRenderer[] RangeSprite;

    [SerializeField] TMP_Text name;
    [SerializeField] TMP_Text type;

    [SerializeField] TMP_Text cost;
    [SerializeField] TMP_Text attack;
    [SerializeField] TMP_Text defense;
    [SerializeField] TMP_Text effect;
    [SerializeField] TMP_Text Key;
    [SerializeField] TMP_Text amount_tmp;

    [SerializeField] GameObject select;
    [SerializeField] GameObject amount;

    public int range; //근거리인지 원거리인지

    public Item item;
    public PRS originPRS;
    int percent;

    public void SetUp(Item item)
    {
        this.item = item;

        name.text = this.item.name;
        type.text = this.item.type;
        cost.text = this.item.cost.ToString();
        attack.text = this.item.attack.ToString();
        defense.text = this.item.defense.ToString();
        effect.text = this.item.effect;

        percent = this.item.percent;

        range = this.item.range;
        for (int i = 0; i < range; i++)
        {
            if (this.item.type == "참격")
                RangeSprite[i].color = new Color(1f, 200 / 255f, 0 / 255f, 1f);
            else if (this.item.type == "타격")
                RangeSprite[i].color = new Color(1f, 100 / 255f, 0f, 1f);
        }

        if (this.item.percent >= 1)
        {
            amount_tmp.text = "X " + this.item.percent.ToString();
        }

        for (int i = 0; i < itemSO.items.Count; i++)
        {
            if (name.text == itemSO.items[i].name)
            {
                illust.sprite = itemSO.items[i].illust;
                side.sprite = itemSO.items[i].side;
            }
        }

        HaveThisCard(item);
    }

    bool NotHaveThisCard;
    void HaveThisCard(Item item)
    {
        this.item = item;

        for (int i = 0; i < Player.Inst.playerdata.CardCollect.Count; i++)
        {
            if (Player.Inst.playerdata.CardCollect[i].name == this.item.name)
            {
                if (Player.Inst.playerdata.CardCollect[i].percent == -1) //특수한 제작 또는 보스로부터만 획득
                {
                    DeepDarkCard();
                }
                else if (Player.Inst.playerdata.CardCollect[i].percent == 0) //일반 제작이 가능함
                {
                    DarkCard();
                }
                else
                {
                    LightCard();
                }
            }
        }
    }

    void DeepDarkCard()
    {
        NotHaveThisCard = true;
        illust.color = new Color(0, 0, 0, 1f);
        card.color = new Color(0.2f, 0.2f, 0.2f, 1f);
        name.color = new Color(0, 0, 0, 0);
        cost.color = new Color(0, 0, 0, 0);
        attack.color = new Color(0, 0, 0, 0);
        defense.color = new Color(0, 0, 0, 0);
        effect.color = new Color(0, 0, 0, 0);

        for (int i = 0; i < RangeSprite.Length; i++)
            RangeSprite[i].color = new Color(RangeSprite[i].color.r, RangeSprite[i].color.g, RangeSprite[i].color.b, 20/255f);

        select.SetActive(false);
    }

    void DarkCard()
    {
        NotHaveThisCard = true;
        illust.color = new Color(0.2f, 0.2f, 0.2f, 1f);
        card.color = new Color(0.2f, 0.2f, 0.2f, 1f);
        name.color = new Color(30 / 255f, 30 / 255f, 30 / 255f, 0.5f);
        cost.color = new Color(0.2f, 0.2f, 0.2f, 1f);

        if (type.text == "소비")
        {
            attack.color = new Color(1f, 1f, 1f, 0f);
            defense.color = new Color(1f, 1f, 1f, 0f);
        }
        else
        {
            attack.color = new Color(0.2f, 0.2f, 0.2f, 1f);
            defense.color = new Color(0.2f, 0.2f, 0.2f, 1f);
        }
        effect.color = new Color(0.2f, 0.2f, 0.2f, 1f);

        for (int i = 0; i < RangeSprite.Length; i++)
            RangeSprite[i].color = new Color(RangeSprite[i].color.r, RangeSprite[i].color.g, RangeSprite[i].color.b, 20 / 255f);

        select.SetActive(false);
    }

    void LightCard()
    {
        NotHaveThisCard = false;
        illust.color = new Color(1, 1, 1, 1f);
        card.color = new Color(1, 1, 1, 1f);
        name.color = new Color(30 / 255f, 30 / 255f, 30 / 255f, 1f);
        cost.color = new Color(1, 1, 1, 1f);

        if (type.text == "소비")
        {
            attack.color = new Color(1f, 1f, 1f, 0f);
            defense.color = new Color(1f, 1f, 1f, 0f);
        }
        else
        {
            attack.color = new Color(1, 1, 1, 1f);
            defense.color = new Color(1, 1, 1, 1f);
        }
        effect.color = new Color(1, 1, 1, 1f);

        for (int i = 0; i < RangeSprite.Length; i++)
            RangeSprite[i].color = new Color(RangeSprite[i].color.r, RangeSprite[i].color.g, RangeSprite[i].color.b, 1f);
    }

    private KeyCode[] //키보드로 카드 선택 1~Y까지(15개)
       keyCodes = {
        KeyCode.Alpha1,
        KeyCode.Alpha2,
        KeyCode.Alpha3,
        KeyCode.Alpha4,
        KeyCode.Alpha5,
        KeyCode.Alpha6,
        KeyCode.Alpha7,
        KeyCode.Alpha8,
        KeyCode.Alpha9,
        KeyCode.Q,
        KeyCode.W,
        KeyCode.E,
        KeyCode.R,
        KeyCode.T,
        KeyCode.Y
       };

    void Update()
    {
        PercentFIx();
        if (NowOver && !NotHaveThisCard)
        {
            if (Input.GetMouseButtonDown(1))
            {
                ListCardManager.Inst.AddSample(item);
            }
        }
        /*
        if (Key.text != "")
        {
            if (Input.GetKeyDown(keyCodes[int.Parse(Key.text) - 1]))
            {
                //CardManager.Inst.selectCard = this;
                //CardManager.Inst.CardMouseDown();
            }
            if (Input.GetKeyUp(keyCodes[int.Parse(Key.text) - 1]))
            {
                //CardManager.Inst.CardMouseUp();
            }
        }
        */
    }
    void PercentFIx()
    {
        bool plz = true;

        for (int i = 0; i < ListCardManager.Inst.Samples.Count; i++)
        {
            if (ListCardManager.Inst.Samples[i].name == name.text && percent - ListCardManager.Inst.Samples[i].percent == 0)
            {
                plz = false;
                amount_tmp.text = "";
                DarkCard();
                NowOver = false;
            }
            else if (ListCardManager.Inst.Samples[i].name == name.text && percent - ListCardManager.Inst.Samples[i].percent > 0)
            {
                plz = false;
                for (int x = 0; x < ListCardManager.Inst.Samples.Count; x++)
                {
                    if (name.text == ListCardManager.Inst.Samples[x].name)
                    {
                        if (ListCardManager.Inst.Samples[x].percent > 0)
                        {
                            LightCard();
                            int del = percent - ListCardManager.Inst.Samples[x].percent;
                            amount_tmp.text = "x " + del.ToString();
                        }
                    }
                }
            }
            else if (ListCardManager.Inst.Samples[i].name == name.text)
            {
                plz = false;
            }
        }
        if (plz)
        {
            if (percent > 0)
            {
                LightCard();
                amount_tmp.text = "x " + percent.ToString();
            }
        }
    }

    public void KeyText(int num) //각 카드들의 순서 할당
    {
        if (num == 0)
        {
            Key.text = null;
        }
        else
        {
            Key.text = num.ToString();
        }
    }

    bool NowOver = false;
    void OnMouseOver()
    {
        if (!NotHaveThisCard)
        {
            NowOver = true;
            select.SetActive(true);
            ListManager.Inst.CardMouseOver(this);
        }
    }

    void OnMouseExit()
    {
        if (!NotHaveThisCard)
        {
            NowOver = false;
            select.SetActive(false);
            ListManager.Inst.CardMouseExit(this);
        }
    }

    void OnMouseDown()
    {
        if (!NotHaveThisCard)
        {
            amount.SetActive(false);
            ListManager.Inst.CardMouseDown(item);
        }
    }

    void OnMouseUp()
    {
        if (!NotHaveThisCard)
        {
            amount.SetActive(true);
            ListManager.Inst.CardMouseUp();
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
