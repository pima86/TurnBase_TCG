using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffManager : MonoBehaviour
{
    public static BuffManager Inst { get; private set; }
    void Awake() => Inst = this;

    [SerializeField] PlayerCharacter pc;

    public int damage_player(Mob mob, int num)
    {
        if (mob.vulnerable != 0)
            num = (int)(num * 1.5f);

        return num;
    }

    public int damage_mob(Mob mob, int num)
    {
        if (mob != null)
        {
            Debug.Log("damage = " + num);
            if (mob.weak != 0)
                num = Mathf.CeilToInt(num / 2f);
        }
        if (pc.vulnerable != 0)
            num = (int)(num * 1.5f);

        return num;
    }
}
