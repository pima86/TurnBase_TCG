using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using System.Linq;

public class ListCardManager : MonoBehaviour
{
    public static ListCardManager Inst { get; private set; }
    void Awake()
    {
        Inst = this;
    }

    [Header("Develop")]
    [SerializeField] [Tooltip("시작 카드 개수를 정합니다")] public int startCardCount;

    [SerializeField] public List<Item> Samples;
    [SerializeField] TMP_Text DeckName;
    [SerializeField] TMP_InputField AfterDeckName;
    [SerializeField] public TMP_Text DeckCount;
    [SerializeField] int MaxCard = 3; //동일카드의 허용 최대 포함 장 수

    [Header("Properties")]
    public bool myTurn = true;
    public bool isLoading;

    public static Action OnAddCard;

    void Update()
    {
        ChangeList();
        DeckListNumber();
    }

    public void DeckNameUpdata()
    {
        DeckName.text = DeckSelect.Inst.DeckDropdown.options[DeckSelect.Inst.DeckDropdown.value].text;
    }

    public void SettingMyDeck(int num)
    {
        DeckNameUpdata();
        AfterDeckName.text = "";
        ScardManager.Inst.DeletAll(num);   
    }

    public void SettingMyDeck2(int num)
    {
        switch (num)
        {
            case 0: TakeMyCard(Player.Inst.playerdata.Decks_box_1); break;
            case 1: TakeMyCard(Player.Inst.playerdata.Decks_box_2); break;
            case 2: TakeMyCard(Player.Inst.playerdata.Decks_box_3); break;
            case 3: TakeMyCard(Player.Inst.playerdata.Decks_box_4); break;
            case 4: TakeMyCard(Player.Inst.playerdata.Decks_box_5); break;
            case 5: TakeMyCard(Player.Inst.playerdata.Decks_box_6); break;
            case 6: TakeMyCard(Player.Inst.playerdata.Decks_box_7); break;
            case 7: TakeMyCard(Player.Inst.playerdata.Decks_box_8); break;
        }
    }

    void TakeMyCard(List<Item> deck)
    {
        for (int i = 0; i < deck.Count; i++)
        {
            if (deck[i].percent > 0)
            {
                ScardManager.Inst.AddCard(deck[i]);
            }
        }
        Samples = deck;
    }

        int addrass = 0;
    public int DeckNumber = 0;
    public void AddSample(Item card) //덱에 카드 추가하기
    {
        if (DeckNumber < 15) 
        {
            CardAdd(card);
        }
    }
    
    public void SaveThisDeck()
    {
        if (AfterDeckName.text != "")
        {
            Player.Inst.SaveListToData(AfterDeckName.text, Samples);

            DeckSelect.Inst.DropdownName();
        }
        else
            Player.Inst.SaveListToData(DeckName.text, Samples);
    }

    public void CardAdd(Item card)
    {
        bool AddGo = true;

        PM = true;
        for (int i = 0; i < Samples.Count; i++)
        {
            if (Samples[i].name == card.name && Samples[i].percent != 0)
            {
                AddGo = false;
                if (Samples[i].percent < MaxCard)
                {
                    //덱에 추가되는 애니메이션
                    Samples[i].percent += 1;
                    //card.percent -= 1;
                }
            }
        }

        if (AddGo)
        {
            ScardManager.Inst.AddCard(card);

            string name_temp = card.name;
            int range_temp = card.range;
            Sprite illust_temp = card.illust;
            Sprite side_temp = card.side;
            int Cost_temp = card.cost;
            int Attack_temp = card.attack;
            int Defense_temp = card.defense;
            string Effect_temp = card.effect;
            string Type_temp = card.type;

            int Percent_temp = card.percent;

            Samples[addrass].name = name_temp;
            Samples[addrass].range = range_temp;
            Samples[addrass].illust = illust_temp;
            Samples[addrass].side = side_temp;
            Samples[addrass].cost = Cost_temp;
            Samples[addrass].attack = Attack_temp;
            Samples[addrass].defense = Defense_temp;
            Samples[addrass].effect = Effect_temp;
            Samples[addrass].type = Type_temp;
            Samples[addrass].percent = 1;
            //card.percent -= 1;
        }
    }

    void DeckListNumber()
    {
        addrass = 0;
        DeckNumber = 0;
        for (int i = 0; i < Samples.Count; i++)
        {
            DeckNumber += (int)Samples[i].percent;
            if (Samples[i].percent != 0)
                addrass += 1;
        }


        DeckCount.text = DeckNumber.ToString();
    }

    [SerializeField] bool PM = false;
    void ChangeList()
    {
        string name_temp;
        int range_temp;
        Sprite illust_temp;
        Sprite side_temp;
        int Cost_temp;
        int Attack_temp;
        int Defense_temp;
        string Effect_temp;
        string Type_temp;
        int Percent_temp;


        if (PM)
        {
            for (int i = 0; i + 1 < addrass; i++)
            {
                if (Samples[i].cost > Samples[i + 1].cost)
                {
                    name_temp = Samples[i + 1].name;
                    range_temp = Samples[i + 1].range;
                    illust_temp = Samples[i + 1].illust;
                    side_temp = Samples[i + 1].side;
                    Cost_temp = Samples[i + 1].cost;
                    Attack_temp = Samples[i + 1].attack;
                    Defense_temp = Samples[i + 1].defense;
                    Effect_temp = Samples[i + 1].effect;
                    Type_temp = Samples[i + 1].type;
                    Percent_temp = Samples[i + 1].percent;

                    Samples[i + 1].name = Samples[i].name;
                    Samples[i + 1].range = Samples[i].range;
                    Samples[i + 1].illust = Samples[i].illust;
                    Samples[i + 1].side = Samples[i].side;
                    Samples[i + 1].cost = Samples[i].cost;
                    Samples[i + 1].attack = Samples[i].attack;
                    Samples[i + 1].defense = Samples[i].defense;
                    Samples[i + 1].effect = Samples[i].effect;
                    Samples[i + 1].type = Samples[i].type;
                    Samples[i + 1].percent = Samples[i].percent;

                    Samples[i].name = name_temp;
                    Samples[i].range = range_temp;
                    Samples[i].illust = illust_temp;
                    Samples[i].side = side_temp;
                    Samples[i].cost = Cost_temp;
                    Samples[i].attack = Attack_temp;
                    Samples[i].defense = Defense_temp;
                    Samples[i].effect = Effect_temp;
                    Samples[i].type = Type_temp;
                    Samples[i].percent = Percent_temp;
                }
            }
        }
        else
        {
            for (int i = addrass; i - 1 >= 0; i--)
            {
                if (Samples[i - 1].percent == 0)
                {
                    Samples[i - 1].name = Samples[i].name;
                    Samples[i - 1].range = Samples[i].range;
                    Samples[i - 1].illust = Samples[i].illust;
                    Samples[i - 1].side = Samples[i].side;
                    Samples[i - 1].cost = Samples[i].cost;
                    Samples[i - 1].attack = Samples[i].attack;
                    Samples[i - 1].defense = Samples[i].defense;
                    Samples[i - 1].effect = Samples[i].effect;
                    Samples[i - 1].type = Samples[i].type;
                    Samples[i - 1].percent = Samples[i].percent;

                    Samples[i].name = null;
                    Samples[i].range = 0;
                    Samples[i].illust = null;
                    Samples[i].side = null;
                    Samples[i].cost = 0;
                    Samples[i].attack = 0;
                    Samples[i].defense = 0;
                    Samples[i].effect = null;
                    Samples[i].type = null;
                    Samples[i].percent = 0;
                }
            }
        }
    }

    public void AddrassDownList(string name, Scard item)
    {
        PM = false;
        for (int i = 0; i < addrass; i++)
        {
            if (Samples[i].name == name)
            {
                Samples[i].percent -= 1;
                //AddCardToPlayer(name);
                if (Samples[i].percent == 0)
                {
                    ScardManager.Inst.DeletCardInDeck(item);
                    Destroy(item.gameObject);
                }
            }
        }
    }

    void AddCardToPlayer(string name)
    {
        for (int i = 0; i < Player.Inst.playerdata.CardCollect.Count; i++)
        {
            if (Player.Inst.playerdata.CardCollect[i].name == name)
                Player.Inst.playerdata.CardCollect[i].percent += 1;
        }
    }

    public IEnumerator StartGameCo()
    {
        yield return new WaitForSeconds(0.1f);

        isLoading = true;
        for (int i = 0; i < startCardCount - 1; i++)
        {
            OnAddCard.Invoke();
        }
        StartTurnCo();
    }

    void StartTurnCo()
    {
        OnAddCard.Invoke();
    }

    public void EndTurn()
    {
        myTurn = !myTurn;
        StartTurnCo();
    }
}
