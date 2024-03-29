using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class CharaOperation : MonoBehaviour
{
    [Header("���삵�ĕ����邩�ǂ���"),Tooltip("���삵�ĕ����邩�ǂ���")]
    public bool CanRun = true;
    [Header("��������"), Tooltip("�v���C���[����������")]
    public float MoveSpeed;
    [SerializeField, Tooltip("�΂߈ړ��ɂ�����{���i�P���ƒʏ푬�x�̂Q�{�j")]
    float MoveDiagonal;
    [Tooltip("�E�ɓ����Ă邩�ǂ���")]
    bool RightMove;
    [Tooltip("��ɓ����Ă邩�ǂ���")]
    bool UpMove;
    [Tooltip("�E�����Ă邩�ǂ���"), HideInInspector]
    public bool RightLook;
    [Tooltip("���]���Ă邩�ǂ���")]
    bool Reversal;
    [Tooltip("�A�j���[�^�[")]
    Animator Anim;

    void Start()
    {
        Anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (CanRun)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                UpMove = true;
            }
            if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            {
                UpMove = false;
            }
            if (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.S))
            {
                UpMove = true;
            }
            if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.W))
            {
                UpMove = false;
            }
            if ((Input.GetKey(KeyCode.UpArrow) || (Input.GetKey(KeyCode.W))) && UpMove)
            {
                if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow) || ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))))
                {
                    transform.position += new Vector3(0, MoveSpeed * MoveDiagonal, 0) * Time.deltaTime;
                }
                else transform.position += new Vector3(0, MoveSpeed, 0) * Time.deltaTime;
                if (Anim != null)
                {
                    Anim.SetBool("BackLook", true);
                    Anim.SetBool("RunBack", true);
                }
            }
            else
            {
                if (Anim != null)
                {
                    Anim.SetBool("RunBack", false);
                }
            }
            if ((Input.GetKey(KeyCode.DownArrow) || (Input.GetKey(KeyCode.S))) && !UpMove)
            {
                if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow)) || ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))))
                {
                    transform.position += new Vector3(0, -MoveSpeed * MoveDiagonal, 0) * Time.deltaTime;
                }
                else
                {
                    transform.position += new Vector3(0, -MoveSpeed, 0) * Time.deltaTime;
                }
                if (Anim != null)
                {
                    Anim.SetBool("BackLook", false);
                    Anim.SetBool("RunFlont", true);
                }
            }
            else
            {
                if (Anim != null)
                {
                    Anim.SetBool("RunFlont", false);
                }
            }
            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                RightMove = true;
            }
            if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D))
            {
                RightMove = false;
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                RightMove = false;
            }
            if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A))
            {
                RightMove = true;
            }
            if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) && RightMove)
            {
                transform.position += new Vector3(MoveSpeed, 0, 0) * Time.deltaTime;
                if (Anim != null)
                {
                    if (Anim.GetBool("BackLook"))
                    {
                        Anim.SetBool("RunBack", true);
                        Anim.SetBool("RunFlont", false);
                    }
                    else
                    {
                        Anim.SetBool("RunBack", false);
                    }
                    Anim.SetBool("RunFlont", true);
                }
                RightLook = true;
            }
            if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) && !RightMove)
            {
                transform.position += new Vector3(-MoveSpeed, 0, 0) * Time.deltaTime;
                if (Anim != null)
                {
                    if (Anim.GetBool("BackLook"))
                    {
                        Anim.SetBool("RunBack", true);
                        Anim.SetBool("RunFlont", false);
                    }
                    else
                    {
                        Anim.SetBool("RunBack", false);
                        Anim.SetBool("RunFlont", true);
                    }
                    RightLook = false;
                }
            }

            if (RightLook && !Reversal)
            {
                transform.Rotate(0, 180, 0);
                Reversal = true;
            }
            if (!RightLook && Reversal)
            {
                transform.Rotate(0, 180, 0);
                Reversal = false;
            }
        }
    }

    public enum Direction
    {
        Up,
        Down,
        Right,
        Left,
    }

    public void CharaAnime(Direction direction)
    {
        Anim.SetBool("RunBack", false);
        Anim.SetBool("RunFlont", false);
        if (direction == Direction.Up)
        {
            Anim.SetBool("BackLook", true);
        }
        else if (direction == Direction.Down)
        {
            Anim.SetBool("BackLook", false);
        }
        else if (direction == Direction.Right)
        {
            RightLook = true;
            if (!Reversal)
            {
                transform.Rotate(0, 180, 0);
                Reversal = true;
            }
        }
        else if (direction == Direction.Left)
        {
            RightLook = false;
            if (Reversal)
            {
                transform.Rotate(0, 180, 0);
                Reversal = false;
            }
        }
    }
}
