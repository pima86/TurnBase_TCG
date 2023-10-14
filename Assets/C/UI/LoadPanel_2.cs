using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class LoadPanel_2 : MonoBehaviour
{
    public static LoadPanel_2 Inst { get; private set; }
    void Awake() => Inst = this;

    [Header("������ ������Ʈ")]
    GameObject left_Door;
    GameObject right_Door;
    GameObject cam_obj;
    Camera cam;

    Vector3 left;
    Vector3 right;

    void Start()
    {
        left_Door = GameObject.Find("�ε���_��");
        right_Door = GameObject.Find("�ε���_��");

        cam_obj = GameObject.Find("Main Camera");
        cam = cam_obj.GetComponent<Camera>();

        StartCoroutine(door_open());
    }

    void UpdatePos()
    {
        left = left_Door.transform.position;
        right = right_Door.transform.position;
    }

    public void SceneMove(string name)
    {
        StartCoroutine(door_close(name));
    }

    IEnumerator door_open()
    {
        Debug.Log("[System]:ȭ���� ���� ������ ������ ����˴ϴ�.");
        ClipManager.Inst.BigGateOpenSound();

        UpdatePos();
        left_Door.transform.DOMove(new Vector3(-7.2f, left.y, left.z), 0.2f);
        right_Door.transform.DOMove(new Vector3(7.2f, right.y, right.z), 0.2f);
        yield return new WaitForSeconds(0.1f);

        DOTween.Kill(left_Door.transform);
        DOTween.Kill(right_Door.transform);

        UpdatePos();
        left_Door.transform.DOMove(new Vector3(-7f, left.y, left.z), 0.2f);
        right_Door.transform.DOMove(new Vector3(7f, right.y, right.z), 0.2f);
        yield return new WaitForSeconds(0.1f);

        DOTween.Kill(left_Door.transform);
        DOTween.Kill(right_Door.transform);

        UpdatePos();
        left_Door.transform.DOMove(new Vector3(-21, left.y, left.z), 2f);
        right_Door.transform.DOMove(new Vector3(21, right.y, right.z), 2f);
    }

    IEnumerator door_close(string name)
    {
        //yield return new WaitForSeconds(0.5f);
        Debug.Log("[System]:ȭ���� ���� ������ ������ ����˴ϴ�.");
        ClipManager.Inst.BigGateCloseSound();

        UpdatePos();
        left_Door.transform.DOMove(new Vector3(-7f, left.y, left.z), 2f);
        right_Door.transform.DOMove(new Vector3(7f, right.y, right.z), 2f);
        yield return new WaitForSeconds(1.8f);

        SceneManager.LoadScene("Story");
    }
}
