using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseOverDeck : MonoBehaviour
{
    public static MouseOverDeck Inst { get; private set; }
    void Awake() => Inst = this;

    void OnMouseDown()
    {
        for (int i = 0; i < Player.Inst.playerdata.Decks_illust.Count; i++)
        {
            if (Player.Inst.playerdata.Decks_illust[i].name == "")
            {
                Player.Inst.playerdata.Decks_illust[i].name = "ºóµ¦";
                Player.Inst.playerdata.Decks_illust[i].addrass = i;
                MailManager.Inst.AddCard();
                break;
            }
        }
    }
}
