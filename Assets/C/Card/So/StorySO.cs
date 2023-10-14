using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Story
{
    public string name; //���丮 ����
    public string content; //���丮 �ٰŸ�
    public string kind;
    public string map;
    public bool clear;
    public string[] monster;
}

[CreateAssetMenu(fileName = "stories", menuName = "Scriptable Object/stories")]
public class StorySO : ScriptableObject
{
    public Story[] stories;
}
