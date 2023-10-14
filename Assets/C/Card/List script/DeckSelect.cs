using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DeckSelect : MonoBehaviour
{
    public static DeckSelect Inst { get; private set; }

    public TMP_Dropdown DeckDropdown;

    private void Awake()
    {
        Inst = this;
        
        Invoke("StartDeckChange", 0.1f);
    }

    void Start()
    {
        Debug.Log(Player.Inst.playerdata.ThisDeck);
        DeckDropdown.value = Player.Inst.playerdata.ThisDeck;
        DeckDropdown.onValueChanged.AddListener(DropboxOptionChange);
    }

    void StartDeckChange()
    {
        Debug.Log(Player.Inst.playerdata.ThisDeck + "¹øÂ° µ¦À» ºÒ·¯¿È");
        ListCardManager.Inst.SettingMyDeck(Player.Inst.playerdata.ThisDeck);
    }

    void DropboxOptionChange(int num)
    {
        if (num == 5)
        {
            int i = 0;
            for (; i < Player.Inst.playerdata.Decks_illust.Count; i++)
            {
                if (Player.Inst.playerdata.Decks_illust[i].name == "")
                {
                    Player.Inst.playerdata.Decks_illust[i].name = "ºó µ¦";
                    DeckDropdown.value = i;
                    DropdownName();
                    break;
                }
            }
            
        }

        ListCardManager.Inst.SettingMyDeck(num);
    }

    public void DropdownName()
    {
        List<TMP_Dropdown.OptionData> option = new List<TMP_Dropdown.OptionData>();
        DeckDropdown.options.Clear();
        string[] Deck_names = new string[21];
        int optionNum = 0;

        for (int i = 0; i < Player.Inst.playerdata.Decks_illust.Count; i++)
        {
            Deck_names[i] = Player.Inst.playerdata.Decks_illust[i].name;
        }

        foreach (string str in Deck_names)
        {
            if(str != "" && str != null)
                option.Add(new TMP_Dropdown.OptionData(str));
        }

        DeckDropdown.AddOptions(option);
        DeckDropdown.RefreshShownValue();
    }
}
