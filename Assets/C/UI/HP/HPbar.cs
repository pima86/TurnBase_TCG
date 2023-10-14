using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPbar : MonoBehaviour
{
    public static HPbar Inst { get; private set; }
    void Awake() => Inst = this;

    [SerializeField] GameObject Prefab;

    GameObject Play;
    GameObject Play_illust;
    void Start()
    {
        Play = GameObject.Find("Character");
        Play_illust = GameObject.Find("Character_illust");

        PlayCalculator();
    }

    void PlayCalculator()
    {
        GameObject hpbar = Instantiate(Prefab, Play.transform.position, Quaternion.identity, GameObject.Find("SubCanvas_HP").transform);
        hpbar.GetComponent<HPUpdate>().playEnter(Play_illust);
        Play.GetComponent<PlayerCharacter>().HPobj = hpbar;
    }

    public void MobCalculator(GameObject mob)
    {
        GameObject hpbar = Instantiate(Prefab, mob.transform.position, Quaternion.identity, GameObject.Find("SubCanvas_HP").transform);
        hpbar.GetComponent<HPUpdate>().mobEnter(mob);
        mob.GetComponent<Mob>().HPobj = hpbar;
    }
}
