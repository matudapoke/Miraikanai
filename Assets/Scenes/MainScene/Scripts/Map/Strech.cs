using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strech : MonoBehaviour
{
    [Tooltip("キャラが伸び縮みするかどうか")]
    public bool StrechCan = true;
    [SerializeField,Tooltip("動いているときに適応するかどうか")]
    bool Adaptwheninmotion;
    [SerializeField,Tooltip("もとの大きさから引く値")]
    float MinusScale = 1;
    [SerializeField, Tooltip("伸び縮みする間隔(秒)")]
    float StrechInterval;
    [Tooltip("動いているかどうかを確認するための最後の位置")]
    Vector3 LastPosition;
    [Tooltip("動いているかどうか")]
    bool Move = false;
    [Tooltip("StrechHeightが実行されているかどうか")]
    bool StrechHeightRunning = false;
    [Tooltip("もとの大きさ")]
    Vector3 OriginalScale;

    void Start()
    {
        OriginalScale = transform.localScale;// もとの大きさを入れておく
        StartCoroutine(StrechHeight());
    }

    void Update()
    {
        if (StrechCan)
        {
            if (!Adaptwheninmotion)
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
            else Move = false;
        }
        else Move = true;
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
}