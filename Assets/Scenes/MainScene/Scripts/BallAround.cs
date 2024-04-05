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

    void Start()
    {
        Player_Trs = GameObject.FindWithTag("Player").transform;
        Ball_Trs = transform.parent.transform;
        Ball_Rb = transform.parent.gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Z)) && BallAroundCollited)
        {
            ApplyForceWithAngle();
        }
    }
    private void ApplyForceWithAngle()
    {
        // �p�x�����W�A���ɕϊ�
        float angleRadians = Mathf.Atan2(Ball_Trs.position.y - Player_Trs.position.y, Ball_Trs.position.x - Player_Trs.position.x);

        // X������Y�������v�Z
        float forceX = Mathf.Cos(angleRadians);
        float forceY = Mathf.Sin(angleRadians);

        // �͂̃x�N�g�����쐬
        Vector2 forceVector = new Vector3(forceX, forceY);

        // �͂�������
        Ball_Rb.AddForce(forceVector * Power);
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            BallAroundCollited = true;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            BallAroundCollited = false;
        }
    }
}
