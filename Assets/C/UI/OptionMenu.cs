using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OptionMenu : MonoBehaviour
{
    public static OptionMenu Inst { get; private set; }
    void Awake() => Inst = this;

    [SerializeField] GameObject[] Screen;
    [SerializeField] GameObject[] Screen_Option;
    [SerializeField] TMP_Text[] Sub_Screen;

    [SerializeField] GameObject[] Sound;
    [SerializeField] GameObject[] Sound_Option;
    [SerializeField] TMP_Text[] Sub_Sound;

    [SerializeField] GameObject[] Game;
    [SerializeField] GameObject[] Game_Option;
    [SerializeField] TMP_Text[] Sub_Game;

    #region 스크린 옵션
    public void ScreenOption_Start()
    {
        for (int i = 0; i < Screen.Length; i++)
            Screen[i].SetActive(true);
        for (int i = 0; i < Sound.Length; i++)
            Sound[i].SetActive(false);
        for (int i = 0; i < Game.Length; i++)
            Game[i].SetActive(false);

        ScreenOption(0);
        Screen_Option[0].SetActive(true);
    }

    public void ScreenOption(int num)
    {
        for (int i = 0; i < Screen.Length; i++)
        {
            if (num != i)
            {
                Sub_Screen[i].color = new Color32(0, 0, 28, 100);
                Screen_Option[i].SetActive(false);
            }
        }
        Sub_Screen[num].color = new Color32(255, 255, 255, 255);
        Screen_Option[num].SetActive(true);
    }
    #endregion

    public void SoundOption_Start()
    {
        for (int i = 0; i < Screen.Length; i++)
            Screen[i].SetActive(false);
        for (int i = 0; i < Sound.Length; i++)
            Sound[i].SetActive(true);
        for (int i = 0; i < Game.Length; i++)
            Game[i].SetActive(false);

        Sub_Sound[0].color = new Color32(255, 255, 255, 255);
        Sound_Option[0].SetActive(true);
    }

    public void GameOption_Start()
    {
        for (int i = 0; i < Screen.Length; i++)
            Screen[i].SetActive(false);
        for (int i = 0; i < Sound.Length; i++)
            Sound[i].SetActive(false);
        for (int i = 0; i < Game.Length; i++)
            Game[i].SetActive(true);

        Sub_Game[0].color = new Color32(255, 255, 255, 255);
        Game_Option[0].SetActive(true);
    }

}
