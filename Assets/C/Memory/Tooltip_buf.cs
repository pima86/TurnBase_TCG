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
        iscontect = true; //���콺�� ���󰡱� ����

        this.name.text = name;

        if(name == "���")
            effect.text = "�޴� ���ذ� 50% �����մϴ�.";
        else if (name == "��ȭ")
            effect.text = "���ݷ��� 50% �����մϴ�.";
        else if (name == "�ߵ�")
            effect.text = "1�ϸ��� ��ġ��ŭ ���ظ� �޽��ϴ�.";
        else if (name == "����")
            effect.text = "���ݰ� �̵� ���ϸ��� ��ġ��ŭ ���ظ� �޽��ϴ�.";
        else if (name == "��")
            effect.text = "��ġ��ŭ ���ݷ��� �����մϴ�.";
        else if (name == "����")
            effect.text = "��ġ��ŭ ������� �����մϴ�.";
        else if (name == "���������ӽ���")
            effect.text = "???";
    }

    public void CloseSet()
    {
        iscontect = false;
    }
}
