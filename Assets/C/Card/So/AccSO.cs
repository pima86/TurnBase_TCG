using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Acc
{
    public Sprite icon; //������
    public string rank; //���
    public string name; //�̸�
    public int amount;
    public string description; //������ ����
    public string[] effect;
}

[CreateAssetMenu(fileName = "AccSO", menuName = "Scriptable Object/AccSO")]
public class AccSO : ScriptableObject
{
    public List<Acc> acc; //��
}
