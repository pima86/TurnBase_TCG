using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Questcontent
{
    public bool clear;
    public bool HaveThisQuest;
    public string name; //����Ʈ �̸�
    public int star; //�� ����
    public string[] monster;
}

[CreateAssetMenu(fileName = "Quests", menuName = "Scriptable Object/Quests")]
public class QuestSO : ScriptableObject
{
    public Questcontent[] Quests; //����Ʈ ���
}

