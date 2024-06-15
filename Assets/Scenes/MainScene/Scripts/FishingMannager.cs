using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.Rendering.DebugManager;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UI;

public class FishingManager : MonoBehaviour
{
    // 値
    [SerializeField, Header("カーソルのスピード")] float Corsor_Speed;
    Vector3 CorsorPosition;
    float currentScale;
    float OriginalDistance;
    FishData FishData;
    float FishingTime_Hit;
    float FishingTime_Throw;
    float FishingTime_ToHitEnd;
    float FishingTime_SinceHit;
    float OKLineMin;
    float OKLineMax;
    // フラグ
    [HideInInspector] public bool CanFishing;
    [HideInInspector] public bool FishingMenu;
    bool FishingPlace_Collided;
    bool UpMove;
    bool RightMove;
    bool FloatLandingWater_Run;
    bool StartFishingReturn;
    bool MeterOperation;
    bool HitSuccess;
    bool HitFailure;
    bool FishImage_Move;
    bool FishImage_Move_Coming;
    // ゲームオブジェクト
    GameObject Corsor_Obj;
    GameObject FishingFloat_Obj;
    GameObject FishImage_Obj;
    [SerializeField, Header("釣れた時に表示する魚の画像。空のprefabを入れる")]
    GameObject FishImage_Prefab;
    GameObject FishingMeter_Obj;
    GameObject FishingMeterBar_Obj;
    // コンポーネント
    Animator PlayerAnime;
    CharaOperation charaOperation;
    Cam CamScript;
    FishingPlace FishingPlaceScript;
    Animator FloatAnime;
    [SerializeField] GameObject FishingMeter_Prefab;
    Reaction reaction;
    Transform FishingMeterBar_Transform;
    MainMenuContoller mainMenuContoller;
    // Audio
    [SerializeField, Header("SE")] AudioClip FloatLandingWater;
    [SerializeField] AudioClip FloatThrow;
    public enum Phase
    {
        StartFishing,
        StartFloat,
        Hit,
        Result,
        End,
    }
    [HideInInspector] public Phase phase = Phase.End;

    void Start()
    {
        // フラグ
        CanFishing = true;
        // ゲームオブジェクト
        Corsor_Obj = GameObject.Find("Corsor");
        Corsor_Obj.SetActive(false);
        FishingFloat_Obj = GameObject.Find("FishingFloat");
        FishingFloat_Obj.SetActive(false);
        // コンポーネント
        charaOperation = GetComponent<CharaOperation>();
        PlayerAnime = GetComponent<Animator>();
        CamScript = GameObject.Find("Main Camera").GetComponent<Cam>();
        FloatAnime = FishingFloat_Obj.GetComponent<Animator>();
        reaction = GameObject.Find("EventManager").GetComponent<Reaction>();
        mainMenuContoller = GameObject.Find("MainMenuContoller").GetComponent<MainMenuContoller>();
    }
    void Update()
    {
        // 釣りを開始
        if (FishingPlace_Collided && Input.GetKeyDown(KeyCode.Space) && phase == Phase.End && CanFishing)
        {
            StartCoroutine(Fishing());
        }
        // カーソルの操作
        if (phase == Phase.StartFishing && !FishingMenu)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) UpMove = true;
            if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) UpMove = false;
            if (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.S)) UpMove = true;
            if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.W)) UpMove = false;
            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) RightMove = true;
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) RightMove = false;
            if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A)) RightMove = true;
            if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D)) RightMove = false;
            if (FishingPlaceScript.direction == FishingPlace.Direction.Up)
            {
                if ((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) && UpMove && Corsor_Obj.transform.position.y - transform.position.y <= 8) Corsor_Obj.transform.position += new Vector3(0, Corsor_Speed, 0) * Time.deltaTime;
                if ((Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) && !UpMove && Corsor_Obj.transform.position.y >= transform.position.y) Corsor_Obj.transform.position += new Vector3(0, -Corsor_Speed, 0) * Time.deltaTime;
                if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) && RightMove && Corsor_Obj.transform.position.x - transform.position.x <= 9) Corsor_Obj.transform.position += new Vector3(Corsor_Speed, 0, 0) * Time.deltaTime;
                if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) && !RightMove && Corsor_Obj.transform.position.x - transform.position.x >= -9) Corsor_Obj.transform.position += new Vector3(-Corsor_Speed, 0, 0) * Time.deltaTime;
            }
            else if (FishingPlaceScript.direction == FishingPlace.Direction.Down)
            {
                if ((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) && UpMove && Corsor_Obj.transform.position.y <= transform.position.y) Corsor_Obj.transform.position += new Vector3(0, Corsor_Speed, 0) * Time.deltaTime;
                if ((Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) && !UpMove && Corsor_Obj.transform.position.y - transform.position.y >= -8) Corsor_Obj.transform.position += new Vector3(0, -Corsor_Speed, 0) * Time.deltaTime;
                if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) && RightMove && Corsor_Obj.transform.position.x - transform.position.x <= 9) Corsor_Obj.transform.position += new Vector3(Corsor_Speed, 0, 0) * Time.deltaTime;
                if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) && !RightMove && Corsor_Obj.transform.position.x - transform.position.x >= -9) Corsor_Obj.transform.position += new Vector3(-Corsor_Speed, 0, 0) * Time.deltaTime;
            }
            else if (FishingPlaceScript.direction == FishingPlace.Direction.Right)
            {
                if ((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) && UpMove && Corsor_Obj.transform.position.y - transform.position.y <= 4) Corsor_Obj.transform.position += new Vector3(0, Corsor_Speed, 0) * Time.deltaTime;
                if ((Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) && !UpMove && Corsor_Obj.transform.position.y - transform.position.y >= -4) Corsor_Obj.transform.position += new Vector3(0, -Corsor_Speed, 0) * Time.deltaTime;
                if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) && RightMove && Corsor_Obj.transform.position.x - transform.position.x <= 10) Corsor_Obj.transform.position += new Vector3(Corsor_Speed, 0, 0) * Time.deltaTime;
                if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) && !RightMove && Corsor_Obj.transform.position.x >= transform.position.x) Corsor_Obj.transform.position += new Vector3(-Corsor_Speed, 0, 0) * Time.deltaTime;
            }
            else if (FishingPlaceScript.direction == FishingPlace.Direction.Left)
            {
                if ((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) && UpMove && Corsor_Obj.transform.position.y - transform.position.y <= 4) Corsor_Obj.transform.position += new Vector3(0, Corsor_Speed, 0) * Time.deltaTime;
                if ((Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) && !UpMove && Corsor_Obj.transform.position.y - transform.position.y >= -4) Corsor_Obj.transform.position += new Vector3(0, -Corsor_Speed, 0) * Time.deltaTime;
                if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) && RightMove && Corsor_Obj.transform.position.x <= transform.position.x) Corsor_Obj.transform.position += new Vector3(Corsor_Speed, 0, 0) * Time.deltaTime;
                if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) && !RightMove && Corsor_Obj.transform.position.x - transform.position.x >= -10) Corsor_Obj.transform.position += new Vector3(-Corsor_Speed, 0, 0) * Time.deltaTime;
            }
            else if (FishingPlaceScript.direction == FishingPlace.Direction.UpRight)
            {
                if ((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) && UpMove && Corsor_Obj.transform.position.y - transform.position.y <= 8) Corsor_Obj.transform.position += new Vector3(0, Corsor_Speed, 0) * Time.deltaTime;
                if ((Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) && !UpMove && Corsor_Obj.transform.position.y >= transform.position.y) Corsor_Obj.transform.position += new Vector3(0, -Corsor_Speed, 0) * Time.deltaTime;
                if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) && RightMove && Corsor_Obj.transform.position.x - transform.position.x <= 10) Corsor_Obj.transform.position += new Vector3(Corsor_Speed, 0, 0) * Time.deltaTime;
                if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) && !RightMove && Corsor_Obj.transform.position.x >= transform.position.x) Corsor_Obj.transform.position += new Vector3(-Corsor_Speed, 0, 0) * Time.deltaTime;
            }
            else if (FishingPlaceScript.direction == FishingPlace.Direction.UpLeft)
            {
                if ((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) && UpMove && Corsor_Obj.transform.position.y - transform.position.y <= 8) Corsor_Obj.transform.position += new Vector3(0, Corsor_Speed, 0) * Time.deltaTime;
                if ((Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) && !UpMove && Corsor_Obj.transform.position.y >= transform.position.y) Corsor_Obj.transform.position += new Vector3(0, -Corsor_Speed, 0) * Time.deltaTime;
                if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) && RightMove && Corsor_Obj.transform.position.x <= transform.position.x) Corsor_Obj.transform.position += new Vector3(Corsor_Speed, 0, 0) * Time.deltaTime;
                if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) && !RightMove && Corsor_Obj.transform.position.x - transform.position.x >= -10) Corsor_Obj.transform.position += new Vector3(-Corsor_Speed, 0, 0) * Time.deltaTime;
            }
            else if (FishingPlaceScript.direction == FishingPlace.Direction.DownRight)
            {
                if ((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) && UpMove && Corsor_Obj.transform.position.y <= transform.position.y) Corsor_Obj.transform.position += new Vector3(0, Corsor_Speed, 0) * Time.deltaTime;
                if ((Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) && !UpMove && Corsor_Obj.transform.position.y - transform.position.y >= -8) Corsor_Obj.transform.position += new Vector3(0, -Corsor_Speed, 0) * Time.deltaTime;
                if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) && RightMove && Corsor_Obj.transform.position.x - transform.position.x <= 10) Corsor_Obj.transform.position += new Vector3(Corsor_Speed, 0, 0) * Time.deltaTime;
                if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) && !RightMove && Corsor_Obj.transform.position.x >= transform.position.x) Corsor_Obj.transform.position += new Vector3(-Corsor_Speed, 0, 0) * Time.deltaTime;
            }
            else if (FishingPlaceScript.direction == FishingPlace.Direction.DownLeft)
            {
                if ((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) && UpMove && Corsor_Obj.transform.position.y <= transform.position.y) Corsor_Obj.transform.position += new Vector3(0, Corsor_Speed, 0) * Time.deltaTime;
                if ((Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) && !UpMove && Corsor_Obj.transform.position.y - transform.position.y >= -8) Corsor_Obj.transform.position += new Vector3(0, -Corsor_Speed, 0) * Time.deltaTime;
                if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) && RightMove && Corsor_Obj.transform.position.x <= transform.position.x) Corsor_Obj.transform.position += new Vector3(Corsor_Speed, 0, 0) * Time.deltaTime;
                if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) && !RightMove && Corsor_Obj.transform.position.x - transform.position.x >= -10) Corsor_Obj.transform.position += new Vector3(-Corsor_Speed, 0, 0) * Time.deltaTime;
            }

        }
        // ウキが動く(アニメーション)
        if (phase == Phase.StartFloat)
        {
            FishingFloat_Obj.transform.position = Vector2.Lerp(FishingFloat_Obj.transform.position, CorsorPosition, 2 * Time.deltaTime);
            // 目標位置までの距離を計算
            float distanceToTarget = Vector3.Distance(FishingFloat_Obj.transform.position, CorsorPosition);
            // スケールを変化させる＆アニメーションを開始する
            if (distanceToTarget >= OriginalDistance / 2)
            {
                currentScale += Time.deltaTime * 2;
                currentScale = Mathf.Clamp(currentScale, 1, 3);
            }
            else
            {
                // 到達距離外ではスケールを小さくする
                currentScale -= Time.deltaTime * 2;
                currentScale = Mathf.Clamp(currentScale, 1, 3);
                if (!FloatLandingWater_Run && currentScale <= 1.1 && currentScale >= 1.01)
                {
                    FloatLandingWater_Run = true;
                    FloatAnime.SetBool("LandingWater", true);
                    GetComponent<AudioSource>().PlayOneShot(FloatLandingWater);
                }
            }
            // オブジェクトのスケールを更新
            FishingFloat_Obj.transform.localScale = new Vector3(currentScale, currentScale, currentScale);
        }
        // HIT時メーター操作
        if (MeterOperation)
        {
            // 結果判定-----------------------
            FishingTime_SinceHit += Time.deltaTime;
            float fishingMeterRotate;
            if (FishingMeterBar_Transform.eulerAngles.z <= 360 && FishingMeterBar_Transform.eulerAngles.z >= 180)
            {
                fishingMeterRotate = FishingMeterBar_Transform.eulerAngles.z - 180;
            }
            else
            {
                fishingMeterRotate = FishingMeterBar_Transform.eulerAngles.z + 180;
            }
            //　時間経過
            if (FishingTime_SinceHit >= FishingTime_ToHitEnd)
            {
                if (fishingMeterRotate > OKLineMin && fishingMeterRotate < OKLineMax)
                {
                    HitSuccess = true;
                    phase = Phase.Hit;
                }
                else
                {
                    HitFailure = true;
                    phase = Phase.Hit;
                }
            }
            // 右端に行ったら成功
            else if (fishingMeterRotate >= 301)
            {
                HitSuccess = true;
                phase = Phase.Hit;
            }
            // 左端に行ったら失敗
            else if (fishingMeterRotate <= 58)
            {
                HitFailure = true;
                phase = Phase.Hit;
            }
            // バーの操作---------------------
            // プレイヤーによる操作
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Z))
            {
                FishingMeterBar_Transform.eulerAngles += new Vector3(0, 0, 5);
            }
            // 魚の抵抗力による操作
            FishingMeterBar_Transform.eulerAngles -= new Vector3(0, 0, FishData.FishPower * Time.deltaTime);
        }
        else
        {
            FishingTime_SinceHit = 0;
            FishingTime_ToHitEnd = 0;
        }
        // 釣り上げた魚の影を動かす
        if (FishImage_Move)
        {
            if (FishImage_Obj.transform.position.y >= transform.position.y + 30)
            {
                if (!FishImage_Move_Coming)
                {
                    FishImage_Obj.transform.position = new Vector3(transform.position.x, FishImage_Obj.transform.position.y, FishImage_Obj.transform.position.z);
                    FishImage_Move_Coming = true;
                }
            }
            if (FishImage_Move_Coming && FishImage_Obj.transform.position.y <= transform.position.y + 1)
            {
                Destroy(FishImage_Obj);
                FishImage_Move = false;
                FishImage_Move_Coming = false;
            }
            // 魚の影がレイジに近づく
            if (FishImage_Move_Coming)
            {
                FishImage_Obj.transform.position -= new Vector3(0, 40 * Time.deltaTime, 0);
            }
            // 魚の影が釣りあげられる
            else if (!FishImage_Move_Coming)
            {
                FishImage_Obj.transform.position += new Vector3(0, 50 * Time.deltaTime, 0);
            }
        }

    }


    IEnumerator Fishing()
    {
        phase = Phase.StartFishing;
        while (phase != Phase.End)
        {
            yield return null;
            switch (phase)
            {
                case Phase.StartFishing:
                    Debug.Log("釣りを開始");
                    // アニメーションを修正
                    PlayerAnime.SetBool("FishingFloatEnd", false);
                    //カーソルを出す
                    Corsor_Obj.SetActive(true);
                    //カメラ移動＆方向を向く(一回目のみ動かす)
                    if (!StartFishingReturn)
                    {
                        FishingCamMove();
                    }
                    StartFishingReturn = true;
                    //プレイヤーキャラの伸び縮みをやめ操作を受け付けなくする
                    gameObject.GetComponent<Strech>().StrechCan = false;
                    charaOperation.CanRun = false;
                    //　メインメニューを開けなくする
                    mainMenuContoller.CanMainMenuOpen = false;
                    // Actionを消す
                    reaction.Action_FadeOut(0.5f);

                    yield return new WaitUntil(() => (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.X)) && !FishingMenu);
                    // リターンorZでウキを浮かべる
                    if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Z))
                    {
                        phase = Phase.StartFloat;
                    }
                    // スペースorXで釣りを終了
                    else if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.X))
                    {
                        FishingEnd();
                        phase = Phase.End;
                    }
                    break;
                case Phase.StartFloat:
                    // カーソルを消してウキを出す
                    Corsor_Obj.SetActive(false);
                    FishingFloat_Obj.SetActive(true);
                    // アニメーション
                    {
                        PlayerAnime.SetBool("Fishing", true);
                        PlayerAnime.SetBool("FishingFloatEnd", false);
                        if (FishingPlaceScript.direction == FishingPlace.Direction.Up)
                        {
                            PlayerAnime.SetBool("ThrowFloatBack", true);
                        }
                        else if (FishingPlaceScript.direction == FishingPlace.Direction.Down)
                        {
                            PlayerAnime.SetBool("ThrowFloatFlont", true);
                        }
                        else if (FishingPlaceScript.direction == FishingPlace.Direction.Right)
                        {
                            PlayerAnime.SetBool("ThrowFloatSide", true);
                        }
                        else if (FishingPlaceScript.direction == FishingPlace.Direction.Left)
                        {
                            PlayerAnime.SetBool("ThrowFloatSide", true);
                        }
                        else if (FishingPlaceScript.direction == FishingPlace.Direction.UpRight)
                        {

                        }
                        else if (FishingPlaceScript.direction == FishingPlace.Direction.UpLeft)
                        {

                        }
                        else if (FishingPlaceScript.direction == FishingPlace.Direction.DownRight)
                        {

                        }
                        else if (FishingPlaceScript.direction == FishingPlace.Direction.DownLeft)
                        {

                        }
                    }
                    // ウキを動かす準備
                    FishingFloat_Obj.transform.position = transform.position;
                    FishingFloat_Obj.transform.localScale = new Vector3(1, 1, 1);
                    CorsorPosition = Corsor_Obj.transform.position;
                    OriginalDistance = Vector3.Distance(FishingFloat_Obj.transform.position, CorsorPosition);
                    //SE
                    GetComponent<AudioSource>().PlayOneShot(FloatThrow);
                    // なんの魚が釣れる？
                    FishData = ChooseFishBasedOnRarity(FishingPlaceScript.FishBornList);
                    // 何秒後にHIT？
                    FishingTime_Hit = Random.Range(3.0f, 3.1f);//<------------ここは将来変える
                    FishingTime_Throw = Time.time;
                    Debug.Log("ウキを開始：" + FishingTime_Hit + "秒後にHIT");
                    yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.X) || FishingTime_Hit + FishingTime_Throw <= Time.time);
                    // リターンorZでカーソルに戻る
                    if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Z))
                    {
                        FishingFloatEnd();
                        phase = Phase.StartFishing;
                    }
                    // スペースorXで釣りを終了
                    else if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.X))
                    {
                        FishingEnd();
                        phase = Phase.End;
                    }
                    // 時間になったらHIT
                    else
                    {
                        phase = Phase.Hit;
                    }
                    break;
                case Phase.Hit:
                    // カメラ操作
                    CamScript.CamReset();
                    CamScript.CamMove(5, (FishingFloat_Obj.transform.position - transform.position) / 2);//(カメラのスピード, +移動する座標)
                    CamScript.CamZoom(3, 1.1f);
                    // マーク
                    GameObject.Find("EventManager").GetComponent<Reaction>().Suprise(transform.position + new Vector3(1, 1.5f, 0), 0.75f);
                    // レイジアニメーション
                    PlayerAnime.SetBool("Hit", true);
                    // 0.75秒待つ
                    yield return new WaitForSeconds(0.75f);
                    PlayerAnime.SetBool("HitNow", true);
                    // カメラ
                    CamScript.CamZoom(5, 1.2f);// カメラズーム(ズーム倍率, ズームスピード)
                    CamScript.CamShake(0.005f, 0.1f);// カメラ振動(振動の大きさ)
                    // SE(FishBuzzing)
                    GetComponent<AudioSource>().Play();
                    // 釣りの長さ
                    FishingTime_ToHitEnd = Random.Range(3.0f, 5.0f);
                    Debug.Log("HIT：" + FishingTime_ToHitEnd + "秒後にHIT終了");
                    // メーターを生成(Rotation(min)0～255)
                    FishingMeter_Obj = Instantiate(FishingMeter_Prefab, FishingFloat_Obj.transform.position, Quaternion.identity,GameObject.Find("CanvasWorld").transform);
                    Transform OKLineTrs = FishingMeter_Obj.transform.Find("OKLine");
                    OKLineMin = 240 * FishData.FishingMeterOKLevelMin + 60; // Minを計算
                    OKLineTrs.eulerAngles = new Vector3(0, 0, OKLineMin); // <--魚の値を代入FishingMeterOKLineMin
                    OKLineMax = 240 * FishData.FishingMeterOKLevelMax + 60;// Maxを計算
                    OKLineTrs.GetComponent<Image>().fillAmount = (FishData.FishingMeterOKLevelMax - FishData.FishingMeterOKLevelMin) * 0.668f; // <--魚の値を代入FishingMeterOKLineMax
                    FishingMeterBar_Obj = FishingMeter_Obj.transform.Find("FishingMeterBar").gameObject;
                    FishingMeterBar_Transform = FishingMeterBar_Obj.transform;
                    MeterOperation = true;
                    yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.X) || HitSuccess || HitFailure);
                    MeterOperation = false;
                    // SEを停止
                    GetComponent<AudioSource>().Stop();
                    GetComponents<AudioSource>()[1].Stop();
                    // スペースorXなら釣りを終了
                    if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.X))
                    {
                        PlayerAnime.SetBool("FishingEnd", true);
                        PlayerAnime.SetBool("FishingFloatEnd", true);
                        FishingEnd();
                        phase = Phase.End;
                    }
                    // 成功もしくは失敗していたらResultへ
                    else if (HitSuccess || HitFailure)
                    {
                        phase = Phase.Result;
                    }
                    break;
                case Phase.Result:
                    // アニメーション
                    PlayerAnime.SetBool("FishingFloatEnd", true);
                    PlayerAnime.SetBool("ThrowFloatBack", false);
                    PlayerAnime.SetBool("ThrowFloatFlont", false);
                    PlayerAnime.SetBool("ThrowFloatSide", false);
                    // メーターを消す
                    Destroy(FishingMeter_Obj);
					// ウキを消す
					FishingFloat_Obj.SetActive(false);
                    //カメラを動かす&カーソルを元の位置に戻す
                    CamScript.CamReset();
                    {
                        if (FishingPlaceScript.direction == FishingPlace.Direction.Up)
                        {
                            CamScript.CamMove(6, new Vector3(0, 3, 0));
                            Corsor_Obj.transform.position += new Vector3(0, 2, 0);
                        }
                        else if (FishingPlaceScript.direction == FishingPlace.Direction.Down)
                        {
                            CamScript.CamMove(6, new Vector3(0, -3, 0));
                            Corsor_Obj.transform.position += new Vector3(0, -2, 0);
                        }
                        else if (FishingPlaceScript.direction == FishingPlace.Direction.Right)
                        {
                            CamScript.CamMove(6, new Vector3(3, 0, 0));
                            Corsor_Obj.transform.position += new Vector3(2, 0, 0);
                        }
                        else if (FishingPlaceScript.direction == FishingPlace.Direction.Left)
                        {
                            CamScript.CamMove(6, new Vector3(-3, 0, 0));
                            Corsor_Obj.transform.position += new Vector3(-2, 0, 0);
                        }
                        else if (FishingPlaceScript.direction == FishingPlace.Direction.UpRight)
                        {
                            CamScript.CamMove(6, new Vector3(3, 3, 0));
                            Corsor_Obj.transform.position += new Vector3(2, 2, 0);
                        }
                        else if (FishingPlaceScript.direction == FishingPlace.Direction.UpLeft)
                        {
                            CamScript.CamMove(6, new Vector3(-3, 3, 0));
                            Corsor_Obj.transform.position += new Vector3(-2, 2, 0);
                        }
                        else if (FishingPlaceScript.direction == FishingPlace.Direction.DownRight)
                        {
                            CamScript.CamMove(6, new Vector3(3, -3, 0));
                            Corsor_Obj.transform.position += new Vector3(2, -2, 0);
                        }
                        else if (FishingPlaceScript.direction == FishingPlace.Direction.DownLeft)
                        {
                            CamScript.CamMove(6, new Vector3(-3, -3, 0));
                            Corsor_Obj.transform.position += new Vector3(-2, -2, 0);
                        }
                        else { Debug.Log("エラー：FishingPlaceの方向を入力して"); }
                    }
                    // 成功
                    if (HitSuccess)
                    {
                        // アイテムをポケットデータベースに追加
                        GameObject FishDataBaseManagerObj = GameObject.Find("DataBaseManager");
                        FishDataBeseManager FishDataBaseManagerScript = FishDataBaseManagerObj.GetComponent<FishDataBeseManager>();
                        FishDataBaseManagerScript.AddFishData(FishData);
                        // 魚を釣り上げる
                        FishImage_Obj = Instantiate(FishImage_Prefab, FishingFloat_Obj.gameObject.transform.position, Quaternion.identity);
                        FishImage_Obj.GetComponent<SpriteRenderer>().sprite = FishData.FishImage;
                        FishImage_Move = true;
                        // もし新種ならウィンドウを表示
                        if (FishData.NewFish)
                        {
                            // 魚の画像を黒くする
                            FishImage_Obj.GetComponent<SpriteRenderer>().color = Color.black;
                            yield return new WaitForSeconds(0.75f);
                            CamScript.CamReset();
                            CamScript.CamMove(2, new Vector3(0, 1.5f, 0));
                            CamScript.CamZoom(3, 1.5f);
                            yield return new WaitForSeconds(1);
                            // ウィンドウを生成する
                            WindowController window = GameObject.Find("WindowContoller").GetComponent<WindowController>();
                            window.NewFishWindow_Creat(FishData);
                            // キーが押すか完全に移動するのを待つ
                            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Z) || window.ShakeRun);
                            // もしいずれかのキーを押したらウィンドウが完全に移動する
                            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Z))
                            {
                                window.NewFishWindow_Click();
                                yield return new WaitForSeconds(0.1f);
                            }
                            // キーが押されるのを待つ
                            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Z));
                            window.NewFishWindow_Destroy();
                            CamScript.CamReset();
                            FishingCamMove();
                            
                        }
                        // 新種でないならアイテム獲得ポップアップを出す
                        else
                        {
                            yield return new WaitForSeconds(0.75f);
                            // ポップアップを表示をポップアップコントローラに指示
                            GameObject.Find("PopupController").GetComponent<PopupController>().SubmitPopup(FishData.FishName, FishData.FishImage);
                            FishingFloatEnd();
                        }
                    }
                    // フラグを戻す
                    HitSuccess = false;
                    HitFailure = false;
                    // カーソルへ戻る
                    FishingFloatEnd();
                    phase = Phase.StartFishing;
                    break;
                case Phase.End:
                    Debug.Log("Phaseがおかしいにょ");
                    break;
            }
        }
    }

    void FishingEnd()
    {
        // プレイヤーキャラが伸び縮みを再開操作を受付
        gameObject.GetComponent<Strech>().StrechCan = true;
        charaOperation.CanRun = true;
        //カーソルを戻す
        Corsor_Obj.SetActive(false);
        Corsor_Obj.transform.position = transform.position;
        // ウキを戻す
        FishingFloat_Obj.SetActive(false);
        // カメラを移動
        CamScript.CamReset();
        // 他フラグを戻す
        StartFishingReturn = false;
        FloatLandingWater_Run = false;
        mainMenuContoller.CanMainMenuOpen = true;
        // アニメーション
        PlayerAnime.SetBool("Fishing", false);
        PlayerAnime.SetBool("FishingFloatEnd", true);
        PlayerAnime.SetBool("ThrowFloatBack", false);
        PlayerAnime.SetBool("ThrowFloatFlont", false);
        PlayerAnime.SetBool("ThrowFloatSide", false);
        PlayerAnime.SetBool("Hit", false);
        PlayerAnime.SetBool("HitNow", false);
        // メーターを消す
        if (FishingMeter_Obj != null)
        {
            Destroy(FishingMeter_Obj);
        }
        Debug.Log("終了");
    }
    void FishingFloatEnd()
    {
        // カーソルを元の位置に戻す
        Corsor_Obj.transform.position = CorsorPosition;
        Corsor_Obj.SetActive(true);
        // ウキを非表示にする
        FishingFloat_Obj.SetActive(false);
        // フラグを戻す
        FloatLandingWater_Run = false;
        // アニメーション
        PlayerAnime.SetBool("FishingFloatEnd", true);
        PlayerAnime.SetBool("ThrowFloatBack", false);
        PlayerAnime.SetBool("ThrowFloatFlont", false);
        PlayerAnime.SetBool("ThrowFloatSide", false);
        PlayerAnime.SetBool("Hit", false);
        PlayerAnime.SetBool("HitNow", false);
    }
    void FishingCamMove()
    {
        if (FishingPlaceScript.direction == FishingPlace.Direction.Up)
        {
            CamScript.CamMove(6, new Vector3(0, 3, 0));
            charaOperation.CharaAnime(CharaOperation.Direction.Up);
            Corsor_Obj.transform.position = new Vector3(0, 2, 0) + transform.position;
        }
        else if (FishingPlaceScript.direction == FishingPlace.Direction.Down)
        {
            CamScript.CamMove(6, new Vector3(0, -3, 0));
            charaOperation.CharaAnime(CharaOperation.Direction.Down);
            Corsor_Obj.transform.position = new Vector3(0, -2, 0) + transform.position;
        }
        else if (FishingPlaceScript.direction == FishingPlace.Direction.Right)
        {
            CamScript.CamMove(6, new Vector3(3, 0, 0));
            charaOperation.CharaAnime(CharaOperation.Direction.Right);
            Corsor_Obj.transform.position = new Vector3(2, 0, 0) + transform.position;
        }
        else if (FishingPlaceScript.direction == FishingPlace.Direction.Left)
        {
            CamScript.CamMove(6, new Vector3(-3, 0, 0));
            charaOperation.CharaAnime(CharaOperation.Direction.Left);
            Corsor_Obj.transform.position = new Vector3(-2, 0, 0) + transform.position;
        }
        else if (FishingPlaceScript.direction == FishingPlace.Direction.UpRight)
        {
            CamScript.CamMove(6, new Vector3(3, 3, 0));
            Corsor_Obj.transform.position = new Vector3(2, 2, 0) + transform.position;
        }
        else if (FishingPlaceScript.direction == FishingPlace.Direction.UpLeft)
        {
            CamScript.CamMove(6, new Vector3(-3, 3, 0));
            Corsor_Obj.transform.position = new Vector3(-2, 2, 0) + transform.position;
        }
        else if (FishingPlaceScript.direction == FishingPlace.Direction.DownRight)
        {
            CamScript.CamMove(6, new Vector3(3, -3, 0));
            Corsor_Obj.transform.position = new Vector3(2, -2, 0) + transform.position;
        }
        else if (FishingPlaceScript.direction == FishingPlace.Direction.DownLeft)
        {
            CamScript.CamMove(6, new Vector3(-3, -3, 0));
            Corsor_Obj.transform.position = new Vector3(-2, -2, 0) + transform.position;
        }
        else { Debug.Log("エラー：FishingPlaceの方向を入力して"); }
    }
    public FishData ChooseFishBasedOnRarity(List<FishData> FishList)// 魚のレアリティに応じてランダムに抽選する
    {
        float total = 0;
        foreach (var fish in FishList)
        {
            if (fish.Rarity < 1 || fish.Rarity > 5) Debug.Log(fish + "のレアリティが規定値を外れています");
            total += 6.0f - fish.Rarity;
        }
        float randomPoint = Random.value * total;

        for (int i = 0; i < FishList.Count; i++)
        {
            if (randomPoint < 6.0f - FishList[i].Rarity)
            {
                return FishList[i];
            }
            else
                randomPoint -= 6.0f - FishList[i].Rarity;
        }

        return null; // ここに到達することはありません
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("FishingPlace"))
        {
            FishingPlaceScript = collision.gameObject.GetComponent<FishingPlace>();
            FishingPlace_Collided = true;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("FishingPlace"))
        {
            FishingPlace_Collided = false;
        }
    }
}