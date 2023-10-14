using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Onclick : MonoBehaviour
{
    public static Onclick Inst { get; private set; }
    Scene scene;

    void Awake()
    {
        Inst = this;
        scene = SceneManager.GetActiveScene();

        
        /*
        float targetWidthAspect = 16.0f;
        float targetHeightAspect = 9.0f;

        Camera.main.aspect = targetWidthAspect / targetHeightAspect;

        float widthRatio = (float)Screen.width / targetWidthAspect;
        float heightRatio = (float)Screen.height / targetHeightAspect;

        float heightadd = ((widthRatio / (heightRatio / 100)) - 100) / 200;
        float widthadd = ((heightRatio / (widthRatio / 100)) - 100) / 200;

        // 시작지점을 0으로 만들어준다.
        if (heightRatio > widthRatio)
            widthRatio = 0.0f;
        else
            heightRatio = 0.0f;

        Camera.main.rect = new Rect(
            Camera.main.rect.x + Mathf.Abs(widthadd),
            Camera.main.rect.x + Mathf.Abs(heightadd),
            Camera.main.rect.width + (widthadd * 2),
            Camera.main.rect.height + (heightadd * 2));
        */
    }


    public TextMeshProUGUI ButtenText;
    public GameObject OptionPanel; //옵션을 키고 끄기 위해

    public bool Field; //옵션을 키고 끄기 위해

    public void OnClickAutoOn()
    {
        if (ButtenText.text == "Auto")
        {
            ButtenText.text = "Manual";
            GameObject.Find("managerGame").GetComponent<TypeEffect>().AutoMode = false;
        }
        else if (ButtenText.text == "Manual")
        {
            ButtenText.text = "Auto";
            GameObject.Find("managerGame").GetComponent<TypeEffect>().AutoMode = true;
            if (GameObject.Find("managerGame").GetComponent<TypeEffect>().IsAnim == false && GameObject.Find("managerGame").GetComponent<TypeEffect>().Sel == false)
                Invoke("Autosecond", 2);
        }
    }
    void Autosecond()
    {
        GameObject.Find("Card").GetComponent<MouseEvent>().Auto();
    }

    bool OptionPanelActive = false;
    public void OnClickOption()
    {
        if (Field)
        {
            if (OptionPanelActive)
            {
                Time.timeScale = 1;
                OptionPanel.transform.position = new Vector3(0,-1590,0);
                OptionPanelActive = false;

                Player.Inst.Save();
            }
            else if (!OptionPanelActive)
            {
                Time.timeScale = 0;

                OptionPanel.transform.position = new Vector3(0.0f, 4.7f, -16.8f);
                OptionPanelActive = true;
            }
        }
        else
        {
            if (OptionPanelActive)
            {
                Time.timeScale = 1;
                OptionPanel.transform.localPosition = new Vector3(0, -1400, 0);
                OptionPanelActive = false;

                Player.Inst.Save();
            }
            else if (!OptionPanelActive)
            {
                Time.timeScale = 0;

                OptionPanel.transform.localPosition = new Vector3(0, 0, 0);
                OptionPanelActive = true;
            }
        }
    }

    public void OnClickGameExit()
    {
        Debug.Log("게임 종료됌");
        Application.Quit();
    }

    public void OnClickGameTitle()
    {
        if(scene.name != "Title")
            SceneManager.LoadScene("Title");
    }

    public void OnClickGameStart()
    {
        if (Player.Inst.playerdata.storyList[0].content != "1")
            LoadPanel.Inst.SceneMove("Main");
        else
            LoadPanel.Inst.SceneMove("Story");
    }

    public void OnClickMain()
    {
        if (scene.name != "Main")
            SceneManager.LoadScene("Main");
    }

    public void OnClickFree_to()
    {
        if (scene.name != "Select")
            SceneManager.LoadScene("Select");
    }

    public void OnClickDeck_to()
    {
        if (scene.name != "DeckList")
            SceneManager.LoadScene("DeckList");
    }

    public void OnClickStory_to()
    {
        if (scene.name != "Story")
            SceneManager.LoadScene("Story");
    }

    public void OnClickMemory_to()
    {
        if (scene.name != "Memory")
            SceneManager.LoadScene("Memory");
    }

    public void OnClickStory_main()
    {
        if (scene.name != "Story")
            SceneManager.LoadScene("Story");
    }

    public void OnClickStory()
    {
        if (scene.name != "Story")
            LoadPanel_2.Inst.SceneMove("Story");
    }

    public void OnClickN1_Battle()
    {
        if (scene.name != "N1_Battle")
        {
            SceneManager.LoadScene("N1_Battle");
        }
    }
}
