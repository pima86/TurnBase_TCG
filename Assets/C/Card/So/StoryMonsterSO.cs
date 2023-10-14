using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SM
{
    public string name; //���丮 ����
    public int player;
    public bool where;
    public string[] monster;
}

[CreateAssetMenu(fileName = "sm", menuName = "Scriptable Object/sm")]
public class StoryMonsterSO : ScriptableObject
{
    public SM[] sm;
}