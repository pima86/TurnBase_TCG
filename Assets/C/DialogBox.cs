using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogBox : MonoBehaviour
{
    public static DialogBox Inst { get; private set; }
    void Awake() => Inst = this;

    public Transform box;
    public CanvasGroup background;

    void OnEnable()
    {
        background.alpha = 0;
        background.LeanAlpha(1, 0.5f);
        box.localPosition = new Vector3(0, -Screen.height, 0);
        box.LeanMoveLocalY(0, 0.5f).setEaseOutExpo().delay = 0.1f;
    }

    public void CloseDialog()
    {
        background.LeanAlpha(0, 0.5f);
        box.LeanMoveLocalY(-Screen.height, 0.5f).setEaseInExpo().setOnComplete(OnComplete);
    }

    public void OnComplete()
    {
        gameObject.SetActive(false);
    }
}
