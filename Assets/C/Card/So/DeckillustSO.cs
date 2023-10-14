using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Deck
{
    public Sprite illust; //�Ϸ���Ʈ
    public int number;
    public int addrass;
    public string name;
}

[CreateAssetMenu(fileName = "DeckillustSO", menuName = "Scriptable Object/DeckillustSO")]
public class DeckillustSO : ScriptableObject
{
    public Deck[] decks; //��
}
