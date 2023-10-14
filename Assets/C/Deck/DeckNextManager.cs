using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DeckNextManager : MonoBehaviour
{
    public static DeckNextManager Inst { get; private set; }
    void Awake() => Inst = this;

    [SerializeField] [Tooltip("���� ī�� ������ ���մϴ�")] public int startCardCount;

    public static Action OnAddCard;

    void Start()
    {
        StartCoroutine("StartGameCo");
    }

    public IEnumerator StartGameCo()
    {
        yield return new WaitForSeconds(0.1f);

        for (int i = 0; i < startCardCount - 1; i++)
        {
            OnAddCard.Invoke();
        }
    }
}
