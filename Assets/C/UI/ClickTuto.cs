using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickTuto : MonoBehaviour
{
    [SerializeField] int num;

    void OnMouseDown()
    {
        StartCoroutine(coroutine_tuto());
    }

    IEnumerator coroutine_tuto()
    {
        yield return new WaitForSeconds(1f);
        Tutorial_page.Inst.Tutorial_1(num);
    }
}
