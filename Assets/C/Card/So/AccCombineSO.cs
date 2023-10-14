using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AccCombine
{
    public string name_result; //�̸�

    public string[] name; //�̸�
}

[CreateAssetMenu(fileName = "AccCombineSO", menuName = "Scriptable Object/AccCombineSO")]
public class AccCombineSO : ScriptableObject
{
    public List<AccCombine> combineSO; //��
}
