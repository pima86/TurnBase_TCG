using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListStartManager : MonoBehaviour
{
    public static ListStartManager Inst { get; private set; }
    void Awake() => Inst = this;

    void Start()
    {
        Player.Inst.Load();
        DeckSelect.Inst.DropdownName();


        StartGame();
    }

    public void StartGame()
    {
        StartCoroutine(ListCardManager.Inst.StartGameCo());
    }
}
