using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class VideoOption : MonoBehaviour
{
    FullScreenMode screenMode;
    public Toggle fullscreenBtn;
    public Toggle NumberKey;

    List<Resolution> resolutions = new List<Resolution>();
    int resolutionNum;
    private void Awake()
    {
        fullscreenBtn.onValueChanged.AddListener(FullScreenBtn);
        NumberKey.onValueChanged.AddListener(NuberKeyChange);
    }

    void Start()
    {
        NumberKey.isOn = Player.Inst.playerdata.NumberKey;

        Invoke("Fullis", 0.01f);
    }

    void Fullis()
    {
        if (Player.Inst.playerdata.ScreenFull)
            Screen1920();
        else
            Screen1280();
    }

    
    public void InitUI()
    {
        fullscreenBtn.isOn = Screen.fullScreenMode.Equals(FullScreenMode.FullScreenWindow) ? true : false;
    }

    public void DropboxOptionChange(int x)
    {
        resolutionNum = x;
        OkBtnClick();
    }

    public void FullScreenBtn(bool isFull)
    {
        Player.Inst.playerdata.ScreenFull = isFull;

        if (isFull)
            Screen1920();
        else
            Screen1280();
        OkBtnClick();
    }

    void Screen1920() => Screen.SetResolution(1920, 1080, true);
    void Screen1280() => Screen.SetResolution(1280, 720, false);

    public void NuberKeyChange(bool isUse)
    {
        Player.Inst.playerdata.NumberKey = isUse;
    }

    public void OkBtnClick()
    {
        Screen.SetResolution(resolutions[resolutionNum].width,
            resolutions[resolutionNum].height,
            screenMode);
    }
}
