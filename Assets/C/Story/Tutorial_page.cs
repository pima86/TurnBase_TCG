using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_page : MonoBehaviour
{
    public static Tutorial_page Inst { get; private set; }
    void Awake() => Inst = this;

    [SerializeField] GameObject[] panel;
    int addrass;

    void Start()
    {
    }

    public void Tutorial_1(int num)
    {
        if (!Player.Inst.playerdata.tutorial[num])
        {
            addrass = num;
            panel[num].SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void Story_Tuto()
    {
        if (!panel[addrass].activeSelf)
        {
            panel[addrass].SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void OnMouseUp()
    {
        if (panel[addrass].activeSelf)
        {
            Time.timeScale = 1;

            if (!Player.Inst.playerdata.tutorial[addrass])
            {
                Player.Inst.playerdata.tutorial[addrass] = true;
                Player.Inst.Save();
            }
            panel[addrass].SetActive(false);
        }
    }


}
