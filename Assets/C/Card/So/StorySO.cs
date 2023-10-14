using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Story
{
    public string name; //스토리 제목
    public string content; //스토리 줄거리
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
