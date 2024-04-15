using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Cam : MonoBehaviour
{
    [Tooltip("�Ǐ]����^�[�Q�b�g")]
    public Transform Target;
    [Tooltip("�^�[�Q�b�g�̒��S"), SerializeField]
    Vector3 TargetShiftPosition;
    Vector3 TargetShiftPositionTem;
    [HideInInspector, Tooltip("�J�����̂��炷�ʒu")]
    public Vector3 ShiftPos;
    [Tooltip("�J�����̃X�s�[�h")]
    public float CamSpeed;
    [Tooltip("���ɃJ�����X�s�[�h�����Ă���")]
    float CamSpeedTem;
    [Tooltip("�J�������Ǐ]����ʒu")]
    Vector3 CamPos;

    [Tooltip("�J�����̑傫��")]
    float CamZoomNow;
    [Tooltip("�ǂꂾ���Y�[�����邩")]
    float CamZoomPulas = 5;
    [Tooltip("�Y�[���̑���")]
    float CamZoomSpeed;

    [Tooltip("�k���̗�")]
    float CamShake_Amount; // �k���̗�
    [Tooltip("�J�������k���邩�ǂ���")]
    bool CamShakeNow;

    float DeltaTIme;
    float CamShake_Time;
    float CamShake_IntervalTime;
    Coroutine camOneShake;

    void LateUpdate()
    {
        CamPos = Target.position + ShiftPos + TargetShiftPosition * 5 /
            CamZoomPulas;
        CamPos.z = -1;
        transform.position = Vector3.Lerp(transform.position, CamPos, CamSpeed * Time.deltaTime);
        //�J�����Y�[��
        CamZoomNow = Camera.main.orthographicSize;
        Camera.main.orthographicSize = Mathf.Lerp(CamZoomNow, CamZoomPulas, CamZoomSpeed * Time.deltaTime);

        // �k��
        DeltaTIme += Time.deltaTime;
        if (DeltaTIme >= CamShake_IntervalTime && CamShakeNow)
        {
            Vector3 randomPoint = CamPos + Random.insideUnitSphere * CamShake_Amount;
            Camera.main.transform.position = new Vector3(randomPoint.x, randomPoint.y, Camera.main.transform.position.z);
            CamShake_IntervalTime += CamShake_Time;
        }
    }


    public void CamReset()
    {
        CamShakeStop();
        CamZoomReset();
        CamSpeed = CamSpeedTem;
        ShiftPos = new Vector3(0,0,0);
        CamZoomPulas = 5;
        CamShake_Amount = 0;
        if (camOneShake != null)
        {
            StopCoroutine(camOneShake);
        }
        CamShakeNow = false;
    }

    public void CamMove(float Speed, Vector3 MoveValue)
    {
        //���̃J�����X�s�[�h���i�[
        CamSpeedTem = CamSpeed;
        //�J�����ړ�
        CamSpeed = Speed;
        ShiftPos += MoveValue;
    }

    public void CamZoom(float ZoomSpeed, float CamZoomLevel)
    {
        CamZoomPulas = CamZoomNow / CamZoomLevel; // �ڕW�̃Y�[�����x����ݒ�
        CamZoomSpeed = ZoomSpeed;
        /*
        TargetShiftPosition = TargetShiftPositionTem;
        TargetShiftPositionTem = TargetShiftPosition;
        TargetShiftPosition = TargetShiftPosition * CamZoomPulas;
        */
    }

    public void CamZoomReset()
    {
        CamZoomPulas = CamZoomNow * 5 / CamZoomNow;
        //TargetShiftPosition = TargetShiftPositionTem;
    }

    public void CamShake(float CamShakeAmount, float CamShakeIntervalTime)
    {
        CamShake_Amount += CamShakeAmount;
        CamShake_IntervalTime = CamShakeIntervalTime;
        CamShakeNow = true;
    }

    public void CamOneShake(float CamShakeAmount, float CamShakeIntervalTime, float CamShakeTime)
    {
        camOneShake = StartCoroutine(CamOneShake_Corotine(CamShakeAmount, CamShakeIntervalTime, CamShakeTime));
    }

    IEnumerator CamOneShake_Corotine(float CamShakeAmount, float CamShakeIntervalTime, float CamShakeTime)
    {
        CamShake_Amount += CamShakeAmount;
        CamShake_IntervalTime = CamShakeIntervalTime;
        CamShakeNow = true;
        yield return new WaitForSeconds(CamShakeTime);
        if (CamShakeNow)
        {
            CamShake_Amount -= CamShakeAmount;
        }
    }

    public void CamShakeStop()
    {
        CamShakeNow = false;
        CamShake_Amount = 0;
    }

    public void ChangeTarget(Transform NewTarget)
    {
        Target = NewTarget;
    }
}