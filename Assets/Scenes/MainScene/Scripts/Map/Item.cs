using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField, Tooltip("なんの魚？"), Header("種類")]
    FishData FishData;
    [SerializeField] bool Gomi;
    GomiCounter gomiCounter;

    [SerializeField, Header("大きさを変える")] bool ChangeScale;
    [SerializeField, Tooltip("もとの大きさから引く値")] float MinusScale = 1;
    [SerializeField, Tooltip("伸び縮みする間隔(秒)")] float StrechInterval;
    [Tooltip("動いているかどうかを確認するための最後の位置")] Vector3 LastPosition;
    [Tooltip("動いているかどうか")] bool Move = false;
    [Tooltip("StrechHeightが実行されているかどうか")] bool StrechHeightRunning = false;
    [Tooltip("もとの大きさ")] Vector3 OriginalScale;

    void Start()
    {
        if (ChangeScale)
        {
            OriginalScale = transform.localScale;// もとの大きさを入れておく
            StartCoroutine(StrechHeight());
        }
        if (Gomi)
        {
            gomiCounter = GameObject.FindWithTag("GomiCounter").GetComponent<GomiCounter>();
        }
    }

    void Update()
    {
        if (ChangeScale)
        {
            if (transform.position != LastPosition)
            {
                Move = true;// 動いていたらMoveがtrue
                if (transform.localScale != OriginalScale)
                {
                    transform.localScale = OriginalScale;// 元の大きさに戻す
                    MinusScale = -MinusScale;
                }
            }
            else
            {
                Move = false; // 動いていなかったらMoveがfalse
                if (!StrechHeightRunning)// コルーチンが実行中でなければコルーチンを開始
                {
                    StartCoroutine(StrechHeight());
                }
            }
            LastPosition = transform.position;
        }
    }

    IEnumerator StrechHeight()
    {
        StrechHeightRunning = true; // コルーチンが開始したのでフラグをtrueに設定
        while (!Move)// 動いていなかったら
        {
            transform.localScale -= new Vector3(0, MinusScale, 0);
            MinusScale = -MinusScale;
            yield return new WaitForSeconds(StrechInterval);
        }
        StrechHeightRunning = false;// コルーチンが終了したのでフラグをfalseに設定
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (FishData != null)// アイテムが魚だったら
            {
                // アイテムをポケットデータベースに追加
                GameObject FishDataBaseManagerObj = GameObject.Find("DataBaseManager");
                FishDataBeseManager FishDataBaseManagerScript = FishDataBaseManagerObj.GetComponent<FishDataBeseManager>();
                FishDataBaseManagerScript.AddFishData(FishData);
                
                // ポップアップを表示をポップアップコントローラに指示
                GameObject PopupControllerObj = GameObject.Find("PopupController");
                PopupController PopupControllerScript = PopupControllerObj.GetComponent<PopupController>();
                PopupControllerScript.SubmitPopup(FishData.FishName , FishData.FishImage);
            }
            if (Gomi)// ゴミだったら
            {
                gomiCounter.AddGomi();
            }
            Destroy(gameObject);// ゲームオブジェクトを消すとそのあとのコードは実行されない
        }
    }
}
