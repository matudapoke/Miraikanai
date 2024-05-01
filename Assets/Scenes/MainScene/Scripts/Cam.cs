using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Cam : MonoBehaviour
{
    [Tooltip("追従するターゲット")]
    public Transform Target;
    [Tooltip("カメラのずらす位置")]
    public Vector3 ShiftPos;
    [Tooltip("カメラのスピード")]
    public float CamSpeed;
    [Tooltip("仮にカメラスピードを入れておく")]
    float CamSpeedTem;
    [Tooltip("カメラが追従する位置")]
    Vector3 CamPos;

    [Tooltip("カメラの大きさ")]
    float CamZoomNow;
    [Tooltip("どれだけズームするか")]
    float CamZoomPulas = 5;
    [Tooltip("ズームの速さ")]
    float CamZoomSpeed;

    [Tooltip("震えの量")]
    float CamShake_Amount; // 震えの量
    [Tooltip("カメラが震えるかどうか")]
    bool CamShakeNow;

    float DeltaTIme;
    float CamShake_Time;
    float CamShake_IntervalTime;
    Coroutine camOneShake;

    void LateUpdate()
    {
        CamPos = Target.position + ShiftPos;
        CamPos.z = -1;
        transform.position = Vector3.Lerp(transform.position, CamPos, CamSpeed * Time.deltaTime);
        //カメラズーム
        CamZoomNow = Camera.main.orthographicSize;
        Camera.main.orthographicSize = Mathf.Lerp(CamZoomNow, CamZoomPulas, CamZoomSpeed * Time.deltaTime);

        // 震え
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
        ShiftPos = new Vector3(0, 0.9f,0);
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
        //元のカメラスピードを格納
        CamSpeedTem = CamSpeed;
        //カメラ移動
        CamSpeed = Speed;
        ShiftPos += MoveValue;
    }

    public void CamZoom(float ZoomSpeed, float CamZoomLevel)
    {
        CamZoomPulas = CamZoomNow / CamZoomLevel; // 目標のズームレベルを設定
        CamZoomSpeed = ZoomSpeed;
    }

    public void CamZoomReset()
    {
        CamZoomPulas = CamZoomNow * 5 / CamZoomNow;
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