using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Tooltip : MonoBehaviour
{
    public static Tooltip Inst { get; private set; }
    void Awake() => Inst = this;

    [SerializeField] Image rank;

    [SerializeField] TMP_Text name;
    [SerializeField] TMP_Text[] effect;

    [SerializeField] bool space_camera = false;

    Vector3 originPos;

    void Start()
    {
        originPos = gameObject.transform.position;
    }

    bool iscontect = false;
    void Update()
    {
        if (iscontect)
        {
            if (space_camera)
            {
                var screenPoint = Input.mousePosition;
                screenPoint.z = 10.0f; //distance of the plane from the camera
                transform.position = Camera.main.ScreenToWorldPoint(screenPoint);
                if (transform.position.x < -5f)
                    transform.position += new Vector3(1.5f,0,0);
            }
            else
            {
                transform.position = Input.mousePosition;
                if (transform.position.x < -5f)
                    transform.position += new Vector3(1.5f, 0, 0);
            }
        }
        else
            transform.position = originPos;
    }

    public void SetUp(Acc acc)
    {
        iscontect = true; //마우스를 따라가기 위해

        name.text = acc.name;
        RankColor(acc.rank);
        for (int i = 0; i < effect.Length; i++)
        {
            if (i < acc.effect.Length)
                effect[i].text = acc.effect[i];
            else
                effect[i].text = "";
        }
    }

    public void CloseSet()
    {
        iscontect = false;
    }

    void RankColor(string str)
    {
        if (str == "일반")
            rank.color = new Color(1f, 1f, 1f, 1f);
        else if (str == "고급")
            rank.color = new Color(150 / 255f, 255 / 255f, 150/255f, 1f);
        else if (str == "희귀")
            rank.color = new Color(172 / 255f, 184 / 255f, 1f, 1f);
        else if (str == "영웅")
            rank.color = new Color(160 / 255f, 70 / 255f, 1f, 1f);
    }
}
