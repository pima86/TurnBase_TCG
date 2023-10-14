using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class ListManager : MonoBehaviour
{
    public static ListManager Inst { get; private set; }
    void Awake() => Inst = this;

    [SerializeField] ItemSO itemSO;
    [SerializeField] Transform cardSpawnPoint;
    [SerializeField] GameObject ListPrefab;
    [SerializeField] List<ListCard> myCards;

    public ListCard selectCard;
    public bool numKeyUse; //나중에 옵션값 만들어서 유지할 것
    bool isMyCardDrag;
    bool onMyCardArea;

    public int size = 10;
    int i = 0;

    public Item PopItem()
    {
        Item item = Player.Inst.playerdata.CardCollect[i];
        i++;
        return item;
    }

    public void NextList()
    {
        int x = 0;
        for (int j = size - ListCardManager.Inst.startCardCount; j < i; j++)
        {
            Destroy(myCards[x].gameObject);
            x++;
        }
        for(int j = size - ListCardManager.Inst.startCardCount; j < i; j++)
            myCards.RemoveAt(0);
    }

    public void MinusList()
    {
        size -= ListCardManager.Inst.startCardCount;
        i = size - ListCardManager.Inst.startCardCount;
        ListEnd = false;
    }

    void Update()
    {
        if (i > size)
        {
            size += ListCardManager.Inst.startCardCount;
        }

        if (isMyCardDrag)
            CardDrag();
        /*
        else if(SelM)
            selectCard.GetComponent<Order>().SetMostFrontOrder(false);
        */
        DetectCardArea();
        SetECardState();
        NumberKey();
    }

    void Start()
    {
        ListCardManager.OnAddCard += AddCard;
    }

    void OnDestroy()
    {
        ListCardManager.OnAddCard -= AddCard;
    }

    public bool ListEnd = false;
    void AddCard()
    {
        if (Player.Inst.playerdata.CardCollect.Count > i)
        {
            if (Player.Inst.playerdata.CardCollect[i].percent >= -1)
            {
                var cardObject = Instantiate(ListPrefab, cardSpawnPoint.position, Utils.QI);
                var card = cardObject.GetComponent<ListCard>();
                card.SetUp(PopItem());
                myCards.Add(card);

                SetOriginOrder();
                CardAlignment();
            }
        }
        else
        {
            ListEnd = true;
        }
    }

    void SetOriginOrder() //카드 내 인터페이스 정렬
    {
        for (int i = 0; i < myCards.Count; i++)
        {
            myCards[i].GetComponent<Order>().SetOriginOrder(i);
        }
    }

    void CardAlignment()
    {
        List<PRS> originCardPRSs = new List<PRS>();
        originCardPRSs = RoundAlignment(cardSpawnPoint, myCards.Count, Vector3.one * 0.8f);
        for (int i = 0; i < myCards.Count; i++)
        {
            myCards[i].originPRS = originCardPRSs[i];
            myCards[i].MoveTransform(myCards[i].originPRS, false, 0);
        }
    }
    
    List<PRS> RoundAlignment(Transform CardTr, int objCount, Vector3 scale)
    {
        float[] objLines_x = new float[5];
        float[] objLines_y = new float[3];
        List<PRS> results = new List<PRS>(objCount);

        objLines_x = new float[] {-1F, 1F, 3F, 5F, 7F};
        objLines_y = new float[] { 1.2f, -2.2f};

        for (int i = 0; i < objCount; i++)
        {
            int w = 0;
            int h = 0;

            switch (i % 5)
            {
                case 0: w = 0; break;
                case 1: w = 1; break;
                case 2: w = 2; break;
                case 3: w = 3; break;
                case 4: w = 4; break;
            }
            switch (i / 5)
            {
                case 0: h = 0; break;
                case 1: h = 1; break;
            }

            var myCardPos = new Vector3(objLines_x[w], objLines_y[h], CardTr.position.z);
            var myCardRot = Quaternion.identity;
            results.Add(new PRS(myCardPos, myCardRot, scale));
        }
        return results;
    }

    void NumberKey()
    {

        for (int i = 0; i < myCards.Count; i++)
        {
            if (numKeyUse)
                myCards[i].GetComponent<ListCard>().KeyText(i + 1);
            else
                myCards[i].GetComponent<ListCard>().KeyText(0);
        }
    }


    #region MyCard
    bool DragM; //드래그 유무
    bool SelM; //선택 유무
    Item DragAdd;
    public void CardMouseDown(Item item)
    {
        DragM = true;
        DragAdd = item;
    }

    public void CardMouseUp()
    {
        if (DragM && !onMyCardArea)
        {
            ListCardManager.Inst.CardAdd(DragAdd);
        }

        DragM = false;
        isMyCardDrag = false;
        CardAlignment();
        /*
        if(SelM)
            EnlargeCard(true, card);
        */
    }

    public void CardMouseOver(ListCard card)
    {
        if(!DragM)
            selectCard = card;
        SelM = true;
        //EnlargeCard(true, card);
    }

    public void CardMouseExit(ListCard card)
    {
        SelM = false;
        //EnlargeCard(false, card);
        if (DragM)
        {
            isMyCardDrag = true;
        }
    }



    void CardDrag()
    {
        //Vector3 enlargePos = new Vector3(0, 0, 100f);
        selectCard.MoveTransform(new PRS(Utils.MousePos, Utils.QI, selectCard.originPRS.scale), false);
        selectCard.GetComponent<Order>().SetMostFrontOrder(isMyCardDrag);
    }

    void DetectCardArea()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(Utils.MousePos, Vector3.forward);
        int layer = LayerMask.NameToLayer("MyCardArea");
        onMyCardArea = Array.Exists(hits, x => x.collider.gameObject.layer == layer);
    }

    void EnlargeCard(bool isEnlarge, ListCard card)
    {
        if (isEnlarge)
        {
            Vector3 enlargePos = new Vector3(0, 0, -100f);
            card.MoveTransform(new PRS(enlargePos, Utils.QI, Vector3.one * 0.6f), false);
        }
        else
            card.MoveTransform(card.originPRS, false);

        card.GetComponent<Order>().SetMostFrontOrder(isEnlarge);
    }

    void SetECardState()
    {
        /*
        if (TurnManager.Inst.isLoading)
            eCardState = ECardState.Nothing;
        else if (!TurnManager.Inst.myTurn)
            eCardState = ECardState.CanMouseOver;
        else if (TurnManager.Inst.myTurn)
            eCardState = ECardState.CanMouseDrag;
        */
    }
    #endregion
}
