using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeOutInList : MonoBehaviour
{
    public static FadeOutInList Inst { get; private set; }
    void Awake() => Inst = this;

    public SpriteRenderer left;
    public SpriteRenderer right;
    bool temp = true;
    IEnumerator FadeInLeft()
    {
        if (temp && left.color.a < 0.5f)
        {
            temp = false;
            float fadeCount = 0;
            while (fadeCount < 0.5f)
            {
                fadeCount += 0.01f;
                yield return new WaitForSeconds(0.005f);
                left.color = new Color(1, 1, 1, fadeCount);
            }
            temp = true;
        }
    }

    IEnumerator FadeInRight()
    {
        if (temp && right.color.a < 0.5f)
        {
            temp = false;
            float fadeCount = 0;
            while (fadeCount < 0.5f)
            {
                fadeCount += 0.01f;
                yield return new WaitForSeconds(0.005f);
                right.color = new Color(1, 1, 1, fadeCount);
            }
            temp = true;
        }
    }

    public void Tempbild()
    {
        temp = true;
        StopCoroutine("FadeInLeft");
        StopCoroutine("FadeInRight");
        left.color = new Color(1, 1, 1, 0);
        right.color = new Color(1, 1, 1, 0);
    }
}
