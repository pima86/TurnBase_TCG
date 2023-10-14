using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Questcontent
{
    public bool clear;
    public bool HaveThisQuest;
    public string name; //퀘스트 이름
    public int star; //별 개수
    public string[] monster;
}

[CreateAssetMenu(fileName = "Quests", menuName = "Scriptable Object/Quests")]
public class QuestSO : ScriptableObject
{
    public Questcontent[] Quests; //퀘스트 목록
}

