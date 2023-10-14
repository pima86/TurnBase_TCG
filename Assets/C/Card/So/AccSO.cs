using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Acc
{
    public Sprite icon; //아이콘
    public string rank; //레어도
    public string name; //이름
    public int amount;
    public string description; //아이템 설명
    public string[] effect;
}

[CreateAssetMenu(fileName = "AccSO", menuName = "Scriptable Object/AccSO")]
public class AccSO : ScriptableObject
{
    public List<Acc> acc; //덱
}
