using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public static CameraMove Inst { get; private set; }
    void Awake() => Inst = this;

    public float cameraSpeed;
    public Vector3 테이블_좌표;
    public Vector3 테이블_각도;
    public Vector3 정면_좌표;
    public Vector3 정면_각도;
    public Vector3 정면_하_좌표;
    public Vector3 정면근접_좌표;
    public Vector3 정면근접_각도;

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
            this.transform.position = Vector3.Lerp(this.transform.position, 테이블_좌표, cameraSpeed);
            this.transform.localEulerAngles = Vector3.Lerp(this.transform.localEulerAngles, 테이블_각도, cameraSpeed);
        }

        if (Face_Selet == true)
        {
            this.transform.position = Vector3.Lerp(this.transform.position, 정면_좌표, cameraSpeed);
            this.transform.localEulerAngles = Vector3.Lerp(this.transform.localEulerAngles, 정면_각도, cameraSpeed);
        }

        if (Face_low_Selet == true)
        {
            this.transform.position = Vector3.Lerp(this.transform.position, 정면_하_좌표, cameraSpeed);
            this.transform.localEulerAngles = Vector3.Lerp(this.transform.localEulerAngles, 정면_각도, cameraSpeed);
        }

        if (FaceClose_Selet == true)
        {
            this.transform.position = Vector3.Lerp(this.transform.position, 정면근접_좌표, cameraSpeed);
            this.transform.localEulerAngles = Vector3.Lerp(this.transform.localEulerAngles, 정면근접_각도, cameraSpeed);
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

    // target 위치로 카메라 속도에 맞게 이동
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