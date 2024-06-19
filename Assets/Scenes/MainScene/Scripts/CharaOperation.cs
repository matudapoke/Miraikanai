using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class CharaOperation : MonoBehaviour
{
    [Header("操作して歩けるかどうか"),Tooltip("操作して歩けるかどうか")]
    public bool CanRun = true;
    [Header("動く速さ"), Tooltip("プレイヤーが動く速さ")]
    public float MoveSpeed;
    [SerializeField, Tooltip("斜め移動にかける倍率（１だと通常速度の２倍）")]
    float MoveDiagonal;
    [Tooltip("右に動いてるかどうか")]
    bool RightMove;
    [Tooltip("上に動いてるかどうか")]
    bool UpMove;
    [Tooltip("右向いてるかどうか"), HideInInspector]
    public bool RightLook;
    [Tooltip("反転してるかどうか")]
    bool Reversal;
    [HideInInspector] public bool CanUpMove;
    [HideInInspector] public bool CanDownMove;
    [HideInInspector] public bool CanRightMove;
    [HideInInspector] public bool CanLeftMove;
    [Tooltip("アニメーター")]
    Animator Anim;

    AudioSource audioSource;
    [SerializeField] AudioClip StepSound;
    [SerializeField] float StepSoundVolume;

    void Start()
    {
        Anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        CanUpMove = true;
        CanDownMove = true;
        CanRightMove = true;
        CanLeftMove = true;
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
                if (CanUpMove)
                {
                    if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow) || ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))))
                    {
                        transform.position += new Vector3(0, MoveSpeed * MoveDiagonal, 0) * Time.deltaTime;
                    }
                    else transform.position += new Vector3(0, MoveSpeed, 0) * Time.deltaTime;
                }
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
                if (CanDownMove)
                {
                    if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow)) || ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))))
                    {
                        transform.position += new Vector3(0, -MoveSpeed * MoveDiagonal, 0) * Time.deltaTime;
                    }
                    else
                    {
                        transform.position += new Vector3(0, -MoveSpeed, 0) * Time.deltaTime;
                    }
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
                if (CanRightMove)
                {
                    transform.position += new Vector3(MoveSpeed, 0, 0) * Time.deltaTime;
                }
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
                if (CanLeftMove)
                {
                    transform.position += new Vector3(-MoveSpeed, 0, 0) * Time.deltaTime;
                }
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

    public Direction GetDirection()
    {
        if (Anim.GetBool("BackLook"))
        {
            return Direction.Up;
        }
        else if (!Anim.GetBool("BackLook"))
        {
            return Direction.Down;
        }
        return Direction.Up;
    }

    public void StepSoundPlay()
    {
        audioSource.PlayOneShot(StepSound,StepSoundVolume);
    }
}
