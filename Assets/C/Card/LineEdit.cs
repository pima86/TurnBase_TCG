using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineEdit : MonoBehaviour
{
    public static LineEdit Inst { get; private set; }
    void Awake() => Inst = this;

    [SerializeField] Camera MouseCam;

    // ���������� ���������� ����� ���Ǿ�
    public Transform s1;
    public Transform target;
    
    // ���������� �������� ��Ƶ� �ӽú���
    Vector3 startPos, endPos;

    LineRenderer lr;

    void Start()
    {
        lr = GetComponent<LineRenderer>();
    }

    bool NowUse = true;
    void Update()
    {
        if (NowUse)
            Pointer();
    }

    public Ray worldpoint()
    {
        return MouseCam.ScreenPointToRay(Input.mousePosition);
    }

    void Pointer()
    {
        lr.positionCount = 100;
        // ��������, �������� �ӽú����� �־ ���
        startPos = s1.position;
        startPos.z = -9;
        endPos = MouseCam.ScreenToWorldPoint(Input.mousePosition);
        endPos.y -= 3.5f;
        endPos.z = -9;
        target.position = endPos;
        // �� ������ �߾Ӱ��� ���ؼ� ����
        Vector3 center = (startPos + endPos) * 0.5f;

        // �������� ���� �׷������� �߾Ӱ� �Ʒ��� ������
        center.y -= 10;

        // ��������, �������� ������ �߾Ӱ��� �������� ����
        startPos = startPos - center;
        endPos = endPos - center;

        // ���η������� ���� ������ ������ŭ �ݺ�
        for (int i = 0; i < lr.positionCount; i++)
        {
            // ������������ ���������� ���������� ��ġ�� ������ ��ȣ��ŭ ������ �Ҵ�
            Vector3 point = Vector3.Slerp(startPos, endPos, i / (float)(lr.positionCount - 1));

            // �������� ���� �׸��� ���� center�� �������Ƿ� �ٽ� ���ؼ� ���󺹱�
            point += center;

            // �� ��ġ���� ���� �������� ����
            lr.SetPosition(i, point);
        }
    }

    public void MobMouseOver(Vector3 mob)
    {
        NowUse = false;
        lr.positionCount = 100;

        startPos = s1.position;
        mob.y += 3f;
        mob.z = -9;
        endPos = mob;
        target.position = endPos;

        Vector3 center = (startPos + endPos) * 0.5f;
        center.y -= 10;
        startPos = startPos - center;
        endPos = endPos - center;

        for (int i = 0; i < lr.positionCount; i++)
        {
            Vector3 point = Vector3.Slerp(startPos, endPos, i / (float)(lr.positionCount - 1));
            point += center;
            lr.SetPosition(i, point);
        }
    }

    public void MobMouseExit()
    {
        NowUse = true;
    }

    public void DownAct()
    {
        NowUse = true;
        gameObject.transform.position = new Vector3(-20, 10, 0);
        target.position = new Vector3(-20, 10, 0);
        lr.positionCount = 0;
        gameObject.SetActive(false);
    }
}
