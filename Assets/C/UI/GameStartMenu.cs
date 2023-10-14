using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameStartMenu : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] GameObject gate;
    [SerializeField] Vector3 point;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (gate.transform.position != point)
        {
            ClipManager.Inst.GateLight();
            gate.transform.position = gameObject.transform.position + point;
        }
    }
}
