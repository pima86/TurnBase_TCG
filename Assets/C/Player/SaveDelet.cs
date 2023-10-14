using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveDelet : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            //Player.Inst.DeletSaveFile();
            Onclick.Inst.OnClickGameTitle();
        }
    }
}
