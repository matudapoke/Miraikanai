using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class FishShadow : MonoBehaviour
{
    /// <summary>
    /// 1.魚がカーソル方向に回転(カーソルアラウンドに当たって釣り竿の性能が見合っていたら)
    /// 2.魚がカーソルに近づく
    /// 3.カーソルをつつく(これは実行しない)
    /// 4.ヒット
    /// 5.結果(もしも：メーターが右端まで行ったら && OK範囲の中に留めたら)
    /// 6.魚が逃げる(もしも：メーターが左端まで行ったら && OK範囲の外なら)
    /// </summary>
    

    [Tooltip("【フラグ】Fishingが実行されているかどうか")]
    bool FishingRun;
    [Tooltip("【フラグ】カメラのズームど振動を行ったかどうか")]
    bool CamZoomRun;
    [Tooltip("【フラグ】魚の回転を行っているかどうか")]
    bool FishTurnRun;
    [Tooltip("【フラグ】魚がカーソルにちかづいているかどうか")]
    bool FishApproacheRun;
    [Tooltip("【フラグ】魚がHITしているかどうか")]
    bool FishHitRun;
    //[Tooltip("【当たり判定】カーソルアラウンドに当たっているかどうか")]
    //bool CorsorAroundCollided;
    [Tooltip("【当たり判定】カーソルに当たったかどうか")]
    bool CorsorCollided;

    [Tooltip("魚のデータ(インスペクターで編集しないで)")]
    public FishData FishData;

    [Tooltip("カーソルに近づく時の角度を記録しておく")]
    float InitialRotation;

    [SerializeField, Tooltip("HIT時のメータープレハブ")]
    GameObject FishingMeterPrefab;
    [Tooltip("メータープレハブのオブジェクト")]
    GameObject FishingMeterObj;
    [Tooltip("HIT時のメーターマスクのトランスフォーム")]
    Transform FishingMeterMaskTrs;
    [Tooltip("カーソルのトランスフォーム")]
    Transform CorsorTrs;
    [Tooltip("カメラのスクリプト")]
    Cam CamScript;
    [Tooltip("プレイヤースクリプト")]
    //Player PlayerScript;


    void Awake()
    {
        //PlayerScript = GameObject.Find("Reizi").GetComponent<Player>();
        CamScript = GameObject.Find("Main Camera").GetComponent<Cam>();
    }

    //当たり判定---------------------------------------------------------------

    void OnTriggerStay2D(Collider2D collision)
    {
        
        if (collision.gameObject.name == "CorsorAround")
        {
            //CorsorAroundCollided = true;
        }

        if (collision.gameObject.name == "Corsor")
        {
            CorsorCollided = true;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        
        if (collision.gameObject.name == "CorsorAround")
        {
            //CorsorAroundCollided = false;
        }

        if (collision.gameObject.name == "Corsor")
        {
            CorsorCollided = false;
        }
    }


    void Update()
    {
        // 当たり判定をとってフラグを立てる && プレイヤーがウキを浮かべているか-----------------------------------
        //if (CorsorAroundCollided && PlayerScript.F_FishingFloat)
        //{
            if (!FishingRun)
            {
                //カーソルの周りに当たったら一定間隔でフラグを立てる(Fishingを実行)
                StartCoroutine(Fishing());
                FishingRun = true;
            }
        //}
        else
        {
            //カーソルの周りに当たっていなかったらフラグを外す
            FishTurnRun = false;
            FishApproacheRun = false;
            FishHitRun = false;
            FishingRun = false;
            CamZoomRun = false;
        }


        //フラグがたったら実行------------------------------------------------

        if (FishTurnRun)
        {
            GameObject CorsorObj = GameObject.Find("Corsor");
            CorsorTrs = CorsorObj.GetComponent<Transform>();
            // 向きたい方向を計算
            Vector3 dir = CorsorTrs.position - transform.position;
            // ここで向きたい方向に回転させてます
            Quaternion targetRotation = Quaternion.FromToRotation(Vector3.up, dir);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * FishData.FishTurnSmoothness);
        }// 1.魚がカーソル方向に回転

        if (FishApproacheRun)
        {
            transform.position = Vector3.Lerp(transform.position, CorsorTrs.position, FishData.FishShadowSpeed * Time.deltaTime);
        }// 2.魚カーソルに近づく

        if (FishHitRun && CorsorCollided)
        {
            if (!CamZoomRun)//一度だけカメラを操作
            {
                CamScript.CamReset();
                CamScript.CamMove(5, transform.position - GameObject.Find("Reizi").GetComponent<Transform>().position);//(カメラのスピード, +移動する座標)
                CamScript.CamZoom(1.2f, 5);// カメラズーム(ズーム倍率, ズームスピード)
                //CamScript.CamShake(0.05f);// カメラ振動(振動の大きさ)
                CamZoomRun = true;
            }
            // 現在の時間に基づいて角度を計算（オシレーション）(FishTurnSmoothnessの20倍のスピードで回転)
            float rotation = InitialRotation + FishData.FishTurnAngle * Mathf.Sin(Time.time * FishData.FishTurnSmoothness * 20);
            transform.eulerAngles = new Vector3(0, 0, rotation);// 新しい角度を設定

            // メーター操作
            if (Input.GetKeyDown(KeyCode.Return))
            {
                FishingMeterMaskTrs.localScale += new Vector3(0.1f, 0, 0);
            }
            if (FishingMeterMaskTrs.localScale.x >= 0) FishingMeterMaskTrs.localScale -= new Vector3(0.001f, 0, 0);
        }// 4.魚がHIT
    }


    IEnumerator Fishing() // 各フラグを一定間隔で立てます。カーソルに当たった瞬間に一度だけ実行します。
    {
        // 1.魚がカーソル方向に回転
        {
            FishTurnRun = true;
            yield return new WaitForSeconds(Random.Range(FishData.FishBiteTime, FishData.FishBiteTime * 1.5f));
        }

        // 2.魚カーソルに近づく
        {
            FishApproacheRun = true;
            yield return new WaitForSeconds(Random.Range(FishData.FishBiteTime / 2, FishData.FishBiteTime / 1.5f));
        }

        // 4.魚がHIT
        {
            InitialRotation = transform.eulerAngles.z;// 元の角度を入れておく
            // メーターを生成
            Instantiate(FishingMeterPrefab, transform.position, Quaternion.identity);// HIT時のメーターを生成する
            FishingMeterObj = GameObject.Find("FishingMeter(Clone)");
            FishingMeterMaskTrs = FishingMeterObj.transform.Find("FishingMeterMask").gameObject.transform;// FishingMeterMaskTrsにトランスフォームを入れる
            FishingMeterMaskTrs.localScale = new Vector3(2, 1, 1);
            // PlayerScript.FishingHitNow = true;
            FishHitRun = true;
            // yield return new WaitForSeconds();
        }

        //6.魚が逃げる(もしも：メーターが左端まで行ったら && OK範囲の外なら)
        {
            //PlayerScript.FishingHitNow = false;
            //FishiHitTimeEnd
        }
    }

}
