using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;


public class ScardManager : MonoBehaviour
{
    public static ScardManager Inst { get; private set; }
    void Awake() => Inst = this;

    [SerializeField] Transform cardSpawnPoint;
    [SerializeField] GameObject SPrefab;
    [SerializeField] List<Scard> InDeck;

    public Scard selectCard;

    public void DeletCardInDeck(Scard item)
    {
        InDeck.Remove(item);

        CardAlignment();
    }

    public void DeletAll(int num)
    {
        for (int i = 0; i < InDeck.Count; i++)
        {
            InDeck[i].Destro();
        }

        InDeck.RemoveRange(0, InDeck.Count);

        ListCardManager.Inst.SettingMyDeck2(num);
    }

    void Update()
    {
        LineUpdate();
    }

    int[] addrass = new int[15];
    void LineUpdate()
    {
        for (int i = 0; i < InDeck.Count; i++)
        {
            for (int j = 0; j < InDeck.Count; j++)
            {
                if (InDeck[i].name.text == ListCardManager.Inst.Samples[j].name)
                {
                    addrass[i] = j;
                }
            }
        }

        for (int i = 0; i < InDeck.Count; i++)
        {
            InDeck[i].MoveTransform(originCardPRSs[addrass[i]], false);
        }
    }

    public void AddCard(Item item)
    {
        var cardObject = Instantiate(SPrefab, cardSpawnPoint.position, Utils.QI);
        var card = cardObject.GetComponent<Scard>();
        card.SetUp(item);
        InDeck.Add(card);

        
        SetOriginOrder();
        CardAlignment();
    }

    void SetOriginOrder() //카드 내 인터페이스 정렬
    {
        for (int i = 0; i < InDeck.Count; i++)
        {
            InDeck[i].GetComponent<Order>().SetOriginOrder(i);
        }
    }

    List<PRS> originCardPRSs = new List<PRS>();
    void CardAlignment()
    {
        originCardPRSs = RoundAlignment(cardSpawnPoint, InDeck.Count, new Vector3(0.4654933f, 0.06699346f, 1));

        for (int i = 0; i < InDeck.Count; i++)
        {
            InDeck[i].originPRS = originCardPRSs[i];
            InDeck[i].MoveTransform(InDeck[i].originPRS, false, 0);
        }
    }

    List<PRS> RoundAlignment(Transform CardTr, int objCount, Vector3 scale)
    {
        float objLines_y = 6.716f;
        List<PRS> results = new List<PRS>(objCount);

        for (int i = 0; i < objCount; i++)
        {
            objLines_y -= 0.5f;

            var myCardPos = new Vector3(-6.63f, objLines_y, CardTr.position.z);
            var myCardRot = Quaternion.identity;
            results.Add(new PRS(myCardPos, myCardRot, scale));
        }
        return results;
    }
}
