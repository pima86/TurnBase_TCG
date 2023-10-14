using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MailManager : MonoBehaviour
{
    public static MailManager Inst { get; private set; }
    void Awake()
    {
        Inst = this;
    }

    [SerializeField] Transform MailSpawnPoint;
    [SerializeField] GameObject MailPrefab;
    [SerializeField] public List<Mail> InMail;

    public int selectMail;
    public int size = 0;
    public int maxsize = 0;

    [SerializeField] [Tooltip("시작 카드 개수를 정합니다")] public int startCardCount;

    public static Action OnAddCard;

    void Start()
    {
        DeckNextManager.OnAddCard += AddCard;
        StartGameCo();
    }

    [SerializeField] GameObject PlusDeck;

    void Update()
    {
        if (MailManager.Inst.size == 8)
            PlusDeck.SetActive(false);
        else
            PlusDeck.SetActive(true);
    }


    int RemoveAD;
    public void DelectThisDeck(int Mail)
    {
        selectMail = Mail;

        SavePlayer.Inst.DelectDeck(Mail);

        for (int i = 0; i < InMail.Count; i++)
        {
            InMail[i].Destro();
        }
        InMail.RemoveRange(0, InMail.Count);

        //item[selectMail].percent = 0;
        
        maxsize = size - 1;
        size = 0;

        for (int i = 0; i < maxsize; i++)
        {
            AddCard();
        }

        CardAlignment();
    }

    void StartGameCo()
    {
        for (int i = 0; i < startCardCount - 1; i++)
        {
            OnAddCard.Invoke();
        }
    }

    public Deck PopItem()
    {
        Deck deck = Player.Inst.playerdata.Decks_illust[size];
        size++;
        return deck;
    }

    public void AddCard()
    {
        if (Player.Inst.playerdata.Decks_illust[size].name != "")
        {
            var cardObject = Instantiate(MailPrefab, MailSpawnPoint.position, Utils.QI);
            var card = cardObject.GetComponent<Mail>();
            card.SetUp(PopItem());
            InMail.Add(card);

            CardAlignment();
        }

        Player.Inst.Save();
    }

    void OnDestroy()
    {
        DeckNextManager.OnAddCard -= AddCard;
    }

    void CardAlignment()
    {
        List<PRS> originCardPRSs = new List<PRS>();
        originCardPRSs = RoundAlignment(MailSpawnPoint, InMail.Count, new Vector3(1.1f, 1.1f, 1));

        for (int i = 0; i < InMail.Count; i++)
        {
            InMail[i].originPRS = originCardPRSs[i];
            InMail[i].MoveTransform(InMail[i].originPRS, false, 0);
        }
    }

    List<PRS> RoundAlignment(Transform CardTr, int objCount, Vector3 scale)
    {
        float objLines_x = CardTr.position.x;
        float objLines_y = CardTr.position.y;
        List<PRS> results = new List<PRS>(objCount);

        for (int i = 0; i < objCount; i++)
        {
            if (size != 8) //추가하기 삭제하면 중지
                objLines_x += 3f;
            else if (i != 0)
                objLines_x += 3f;

            if (objLines_x == 11)
            {
                objLines_x = CardTr.position.x;
                objLines_y -= 5f;
            }

            var myCardPos = new Vector3(objLines_x, objLines_y, CardTr.position.z);
            var myCardRot = Quaternion.identity;
            results.Add(new PRS(myCardPos, myCardRot, scale));
        }
        return results;
    }
}
