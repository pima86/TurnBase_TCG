using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherPanelMov : MonoBehaviour
{
    [SerializeField] LeftCanvasPanel obj;

    public void OnMouseDown()
    {
        obj.OnMouseDown();
    }
}
