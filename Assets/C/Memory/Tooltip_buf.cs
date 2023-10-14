using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Tooltip_buf : MonoBehaviour
{
    public static Tooltip_buf Inst { get; private set; }
    void Awake() => Inst = this;

    [SerializeField] TMP_Text name;
    [SerializeField] TMP_Text effect;

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
                    transform.position += new Vector3(1.5f, 0, 0);
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

    public void SetUp(string name)
    {
        iscontect = true; //마우스를 따라가기 위해

        this.name.text = name;

        if(name == "취약")
            effect.text = "받는 피해가 50% 증가합니다.";
        else if (name == "약화")
            effect.text = "공격력이 50% 감소합니다.";
        else if (name == "중독")
            effect.text = "1턴마다 수치만큼 피해를 받습니다.";
        else if (name == "출혈")
            effect.text = "공격과 이동 패턴마다 수치만큼 피해를 받습니다.";
        else if (name == "힘")
            effect.text = "수치만큼 공격력이 증가합니다.";
        else if (name == "끈기")
            effect.text = "수치만큼 수비력이 증가합니다.";
        else if (name == "보스슬라임스택")
            effect.text = "???";
    }

    public void CloseSet()
    {
        iscontect = false;
    }
}
