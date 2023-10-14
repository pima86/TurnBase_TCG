using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public static CameraMove Inst { get; private set; }
    void Awake() => Inst = this;

    public float cameraSpeed;
    public Vector3 ���̺�_��ǥ;
    public Vector3 ���̺�_����;
    public Vector3 ����_��ǥ;
    public Vector3 ����_����;
    public Vector3 ����_��_��ǥ;
    public Vector3 �������_��ǥ;
    public Vector3 �������_����;

    bool Table_Selet = true;
    bool Face_Selet = false;
    bool Face_low_Selet = false;
    bool FaceClose_Selet = false;

    int i = 0;

    // Start is called before the first frame update

    void Update()
    {
        if (Table_Selet == true)
        {
            this.transform.position = Vector3.Lerp(this.transform.position, ���̺�_��ǥ, cameraSpeed);
            this.transform.localEulerAngles = Vector3.Lerp(this.transform.localEulerAngles, ���̺�_����, cameraSpeed);
        }

        if (Face_Selet == true)
        {
            this.transform.position = Vector3.Lerp(this.transform.position, ����_��ǥ, cameraSpeed);
            this.transform.localEulerAngles = Vector3.Lerp(this.transform.localEulerAngles, ����_����, cameraSpeed);
        }

        if (Face_low_Selet == true)
        {
            this.transform.position = Vector3.Lerp(this.transform.position, ����_��_��ǥ, cameraSpeed);
            this.transform.localEulerAngles = Vector3.Lerp(this.transform.localEulerAngles, ����_����, cameraSpeed);
        }

        if (FaceClose_Selet == true)
        {
            this.transform.position = Vector3.Lerp(this.transform.position, �������_��ǥ, cameraSpeed);
            this.transform.localEulerAngles = Vector3.Lerp(this.transform.localEulerAngles, �������_����, cameraSpeed);
        }
    }

    public void TableIn(float speed)
    {
        cameraSpeed = speed;
        Table_Selet = true;
    }

    public void TableOut()
    {
        Table_Selet = false;
    }

    // target ��ġ�� ī�޶� �ӵ��� �°� �̵�
    public void FaceIn(float speed)
    {
        cameraSpeed = speed;
        Face_Selet = true;
    }

    public void FaceOut()
    {
        Face_Selet = false;
    }

    public void FaceLowIn(float speed)
    {
        cameraSpeed = speed;
        Face_low_Selet = true;
    }

    public void FaceLowOut()
    {
        Face_low_Selet = false;
    }


    public void FaceCloseIn(float speed)
    {
        cameraSpeed = speed;
        FaceClose_Selet = true;
    }

    public void FaceCloseOut()
    {
        FaceClose_Selet = false;
    }

}