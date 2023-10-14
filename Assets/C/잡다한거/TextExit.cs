using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextExit : MonoBehaviour
{
    [SerializeField] GameObject exit;

    void Start()
    {
        Invoke("Check", 0.1f);
    }

    private void Check()
    {
        for (int i = 0; i < Player.Inst.playerdata.storyList.Count; i++)
        {
            if (Player.Inst.playerdata.story == Player.Inst.playerdata.storyList[i].name && Player.Inst.playerdata.storyList[i].clear == true)
                    exit.SetActive(true);
        }
    }
}
