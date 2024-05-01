using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;
using UnityEngine.Device;

public class BallAround : MonoBehaviour
{
    [SerializeField] float Power;
    Transform Player_Trs;
    Transform Ball_Trs;
    Rigidbody2D Ball_Rb;
    bool BallAroundCollited;
    MainMenuContoller MainMenuContoller;

    void Start()
    {
        Player_Trs = GameObject.FindWithTag("Player").transform;
        Ball_Trs = transform.parent.transform;
        Ball_Rb = transform.parent.gameObject.GetComponent<Rigidbody2D>();
        MainMenuContoller = GameObject.Find("MainMenuContoller").GetComponent<MainMenuContoller>();
    }

    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Z)) && BallAroundCollited && !MainMenuContoller.MenuNow)
        {
            ApplyForceWithAngle();
        }
    }
    private void ApplyForceWithAngle()
    {
        // 角度をラジアンに変換
        float angleRadians = Mathf.Atan2(Ball_Trs.position.y - Player_Trs.position.y, Ball_Trs.position.x - Player_Trs.position.x);

        // X成分とY成分を計算
        float forceX = Mathf.Cos(angleRadians);
        float forceY = Mathf.Sin(angleRadians);

        // 力のベクトルを作成
        Vector2 forceVector = new Vector3(forceX, forceY);

        // 力を加える
        Ball_Rb.AddForce(forceVector * Power);
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !BallAroundCollited)
        {
            BallAroundCollited = true;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && BallAroundCollited)
        {
            BallAroundCollited = false;
        }
    }
}
