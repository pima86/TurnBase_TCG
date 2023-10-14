using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCI_Gate : MonoBehaviour
{
    public static SCI_Gate Inst { get; private set; }
    void Awake() => Inst = this;

    [SerializeField] GameObject[] gate;

    bool GateBool = false;
    void Update()
    {
        if (GateBool)
        {
            if (gate[0].transform.position.x < 8.8f)
                gate[0].transform.position = Vector3.Lerp(gate[0].transform.position, new Vector3(9.8f, gate[0].transform.position.y, -1), 2 * Time.deltaTime);
            if (gate[1].transform.position.x > -2.57f)
                gate[1].transform.position = Vector3.Lerp(gate[1].transform.position, new Vector3(-3.67f, gate[1].transform.position.y, 0), 2 * Time.deltaTime);
        }
        if (!GateBool)
        {
            if (gate[0].transform.position.x > 6.1f)
                gate[0].transform.position = Vector3.Lerp(gate[0].transform.position, new Vector3(6.1f, gate[0].transform.position.y, -1), 5 * Time.deltaTime);
            if (gate[1].transform.position.x < 0.1f)
                gate[1].transform.position = Vector3.Lerp(gate[1].transform.position, new Vector3(0.1f, gate[1].transform.position.y, 0), 5 * Time.deltaTime);
        }
    }

    public void OpenGate() => GateBool = true;
    public void CloseGate() => GateBool = false;
}
