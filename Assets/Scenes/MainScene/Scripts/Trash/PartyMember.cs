using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PartyMember : MonoBehaviour
{
    [Tooltip("動くスピード")]
    float MoveSpeedPlayer;
    [SerializeField, Tooltip("プレイヤーの動くスピードの何倍か")]
    float MoveSpeedRatio;
    [SerializeField, Tooltip("追尾するかどうか(離れる距離を表すオブジェクトに侵入しているかどうか)")]
    bool Follow;
    [SerializeField, Tooltip("離れる距離を表すゲームオブジェクト")]
    GameObject DistanceObj;
    [SerializeField, Tooltip("追尾する対象のゲームオブジェクト")]
    GameObject TargetObj;
    [Tooltip("追尾するオブジェクトのトランスフォーム")]
    Transform TargetTrs;

    //瞬間移動
    [SerializeField, Tooltip("瞬間移動する距離")]
    float DistanceMax;
    [SerializeField, Tooltip("スピードが1.5倍になる距離")]
    float DistanceA;
    [SerializeField, Tooltip("スピードが2倍になる距離")]
    float DistanceB;
    [Tooltip("MoveSpeedRatioを仮に入れておく")]
    float MoveSpeedRatioTem;

    //Listに座標を入れてプレイヤーを追尾
    [Tooltip("プレイヤーの座標を入れておくリスト")]
    List<Vector3> PlayerPositions = new List<Vector3>();
    [SerializeField, Tooltip("プレイヤーの座標を再取得するまでの時間")]
    float RecordInterval;
    [Tooltip("プレイヤーの座標を数える")]
    int PlayerPositionsCount = 0;


    void Start()
    {
        TargetTrs = TargetObj.GetComponent<Transform>();
        PlayerPositions.Add(TargetTrs.position);//最初にリストにプレイヤーの座標を入れる

        //PlayerのMoveSpeedを取得
        //Player PlayerScript;
        //PlayerScript = TargetObj.GetComponent<Player>(); //付いているスクリプトを取得
        //MoveSpeedPlayer = PlayerScript.MoveSpeed;
        
        Follow = true;//最初はついていく
        MoveSpeedRatioTem = MoveSpeedRatio;
    }

    void Update()
    {
        if(Follow)
        {
            transform.position = Vector3.MoveTowards(transform.position, PlayerPositions[PlayerPositionsCount], MoveSpeedPlayer * MoveSpeedRatio * Time.deltaTime);
            if (PlayerPositions[PlayerPositionsCount].x + 0.5 >= transform.position.x && PlayerPositions[PlayerPositionsCount].x - 0.5 <= transform.position.x && PlayerPositions[PlayerPositionsCount].y + 0.5 >= transform.position.y && PlayerPositions[PlayerPositionsCount].y - 0.5 <= transform.position.y)
                {
                    //timer = 0f;
                    PlayerPositions.Add(TargetTrs.position);
                    PlayerPositionsCount++;
                }
        }

        var Heading = TargetTrs.position - transform.position;//ターゲットとの距離
        if (Heading.x >= DistanceA || Heading.x <= -DistanceA || Heading.y >= DistanceA || Heading.y <= -DistanceA)
        {
            MoveSpeedRatio = 3;
            //Debug.Log("1.5");
        }
        if (Heading.x >= DistanceB || Heading.x <= -DistanceB || Heading.y >= DistanceB || Heading.y <= -DistanceB)
        {
            MoveSpeedRatio = 5;
            //Debug.Log("2");
        }
        if (Heading.x >= DistanceMax || Heading.x <= -DistanceMax || Heading.y >= DistanceMax || Heading.y <= -DistanceMax)
        {
            MoveSpeedRatio = 100;
            //Debug.Log("100");
        }
        if (Heading.x <= 7 && Heading.x >= -7 || Heading.y <= 7 && Heading.y >-7)
        {
            MoveSpeedRatio = MoveSpeedRatioTem;
            //Debug.Log("1");
        }

        //
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject==DistanceObj)Follow = false;
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject==DistanceObj)Follow = true;
    }
}
