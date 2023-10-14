using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnEnableSelect : MonoBehaviour
{
    void OnEnable()
    {
        ClipManager.Inst.GateLight();
    }
}
