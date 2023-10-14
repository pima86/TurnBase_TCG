using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HPUpdate : MonoBehaviour
{
    GameObject Play;
    [SerializeField] TMP_Text health;
    [SerializeField] Image hp;

    [SerializeField] GameObject Sh_sprite;
    [SerializeField] TMP_Text Shield;

    SpriteRenderer sp;
    float hpx;
    float hpy;

    public void playEnter(GameObject obj)
    {
        this.Play = obj;
        sp = Play.GetComponent<SpriteRenderer>();
        hpx = sp.bounds.size.x / 2;
        hpy = sp.bounds.size.y / 2;

        playerstart = true;
    }

    public void mobEnter(GameObject obj)
    {
        this.Play = obj;
        sp = Play.GetComponent<Mob>().illust;
        hpx = sp.bounds.size.x / 2;
        hpy = sp.bounds.size.y / 2;
        mobstart = true;
    }

    bool playerstart = false;
    bool mobstart = false;
    void Update()
    {
        if (playerstart)
        {
            gameObject.transform.position = Play.transform.position - new Vector3(0, hpy * 0.45f, 1f);
        }
        if (mobstart)
        {
            gameObject.transform.position = Play.transform.position - new Vector3(0, 0, 0.32f);
                //Play.transform.position - new Vector3(0, hpy * 0.25f, 0);
        }
    }

    public void ShIeldSetAct_1()
    {
        if(!Sh_sprite.activeSelf)
            Sh_sprite.SetActive(true);
    }

    public void ShIeldSetAct_2()
    {
        if (Sh_sprite.activeSelf)
            Sh_sprite.SetActive(false);
    }

    public void ShIeldSetAct_3(int num)
    {
        Shield.text = num.ToString();
    }

    public void HPDODO(float num, float max)
    {
        float t = num / max;
        hp.fillAmount = t;
        health.text = num.ToString() + " / " + max.ToString();
    }

    public void Destro() => Destroy(gameObject);
}
