using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class NotificationPanel : MonoBehaviour
{
    [SerializeField] TMP_Text notificationTMP;
    [SerializeField] GameObject notifiPanel;

    void Start()
    {
        Show("전투 승리");
    }

    public void Show(string message)
    {
        notificationTMP.text = message;
        notifiPanel.SetActive(true);
        transform.DOMove(new Vector3(960.0f,790.0f,0.0f), 1.0f, false);
    }
}
    /*
    //내 턴 외치는 곳
    Sequence sequence = DOTween.Sequence()
        .Append(transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.InOutQuad))
        .AppendInterval(0.9f)
        .Append(transform.DOScale(Vector3.zero, 0.3f).SetEase(Ease.InOutQuad));
}
void Start() => ScaleZero();

[ContextMenu("ScaleOne")]
void ScaleOne() => transform.localScale = Vector3.one;

[ContextMenu("ScaleZero")]
public void ScaleZero() => transform.localScale = Vector3.zero;
}
     */