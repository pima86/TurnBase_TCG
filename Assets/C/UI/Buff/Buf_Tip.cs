using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buf_Tip : MonoBehaviour
{
    public void OnMouseOver()
    {
        Tooltip_buf.Inst.SetUp(gameObject.name);
    }

    public void OnMouseExit()
    {
        Tooltip_buf.Inst.CloseSet();
    }
}
