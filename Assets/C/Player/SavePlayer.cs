using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePlayer : MonoBehaviour
{
    public static SavePlayer Inst { get; private set; }
    void Awake() => Inst = this;

    List<Item>[] items;
    List<Item> item_1;
    List<Item> item_2;
    int num = 0;

    private void Deck_List(int addrass)
    {
        items = new List<Item>[8];
        num = 0;

        if (addrass >= 0){ items[num] = Player.Inst.playerdata.Decks_box_1; num++; }
        if (addrass >= 1){ items[num] = Player.Inst.playerdata.Decks_box_2; num++; }
        if (addrass >= 2){ items[num] = Player.Inst.playerdata.Decks_box_3; num++; }
        if (addrass >= 3){ items[num] = Player.Inst.playerdata.Decks_box_4; num++; }
        if (addrass >= 4){ items[num] = Player.Inst.playerdata.Decks_box_5; num++; }
        if (addrass >= 5){ items[num] = Player.Inst.playerdata.Decks_box_6; num++; }
        if (addrass >= 6){ items[num] = Player.Inst.playerdata.Decks_box_7; num++; }
        if (addrass >= 7){ items[num] = Player.Inst.playerdata.Decks_box_8; num++; }
        Debug.Log(addrass + "번째부터 " + num + "개를 저장");
    }

    private void DeckReset(int addrass)
    {
        Debug.Log("addrass" + addrass);
        Deck_List(addrass);

        int maxaddrass = num - 1;
        for (int x = 0; x + addrass + 1 < MailManager.Inst.InMail.Count; x++)
        {
            Debug.Log("items[x] => " + (x + addrass));
            item_1 = items[x];
            
            Debug.Log("items[x + 1] => " + (x + addrass + 1));
            item_2 = items[x + 1];
            for (int i = 0; i < 15; i++)
            {
                if (item_2[i].name != null || item_2[i].name != "")
                {
                    item_1[i].name = item_2[i].name.ToString();
                    item_1[i].cost = item_2[i].cost;
                    item_1[i].attack = item_2[i].attack;
                    item_1[i].defense = item_2[i].defense;
                    item_1[i].percent = item_2[i].percent;
                }
            }
        }
        Debug.Log("maxaddrass => " + maxaddrass);
        item_1 = items[maxaddrass];
        for (int i = 0; i < 15; i++)
        {
            item_1[i].name = "";
            item_1[i].cost = 0;
            item_1[i].attack = 0;
            item_1[i].defense = 0;
            item_1[i].percent = 0;
        }
    }

    public void DelectDeck(int addrass)
    {
        for (int i = addrass; i+1 < MailManager.Inst.InMail.Count; i++)
        {
            Player.Inst.playerdata.Decks_illust[i].number = Player.Inst.playerdata.Decks_illust[i + 1].number;
            Player.Inst.playerdata.Decks_illust[i].name = Player.Inst.playerdata.Decks_illust[i + 1].name.ToString();
        }
        int maxaddrass = MailManager.Inst.InMail.Count - 1;
        Player.Inst.playerdata.Decks_illust[maxaddrass].number = 0;
        Player.Inst.playerdata.Decks_illust[maxaddrass].name = "";

        DeckReset(addrass);

        Player.Inst.Save();
    }
}
