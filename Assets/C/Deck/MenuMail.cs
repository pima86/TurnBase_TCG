using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMail : MonoBehaviour
{
    public static MenuMail Inst { get; private set; }
    void Awake() => Inst = this;

    public GameObject Panel;

    Mail mails;
    int num;

    public void SetActivePanel(int i, Mail mail)
    {
        Panel.SetActive(true);
        num = i;
        mails = mail;
    }

    public void DelectDeck()
    {
        MailManager.Inst.DelectThisDeck(num);
        DialogBox.Inst.CloseDialog();
    }
}
