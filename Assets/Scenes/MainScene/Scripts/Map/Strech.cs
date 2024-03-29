using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strech : MonoBehaviour
{
    [Tooltip("�L�������L�яk�݂��邩�ǂ���")]
    public bool StrechCan = true;
    [SerializeField,Tooltip("�����Ă���Ƃ��ɓK�����邩�ǂ���")]
    bool Adaptwheninmotion;
    [SerializeField,Tooltip("���Ƃ̑傫����������l")]
    float MinusScale = 1;
    [SerializeField, Tooltip("�L�яk�݂���Ԋu(�b)")]
    float StrechInterval;
    [Tooltip("�����Ă��邩�ǂ������m�F���邽�߂̍Ō�̈ʒu")]
    Vector3 LastPosition;
    [Tooltip("�����Ă��邩�ǂ���")]
    bool Move = false;
    [Tooltip("StrechHeight�����s����Ă��邩�ǂ���")]
    bool StrechHeightRunning = false;
    [Tooltip("���Ƃ̑傫��")]
    Vector3 OriginalScale;

    void Start()
    {
        OriginalScale = transform.localScale;// ���Ƃ̑傫�������Ă���
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
                    Move = true;// �����Ă�����Move��true
                    if (transform.localScale != OriginalScale)
                    {
                        transform.localScale = OriginalScale;// ���̑傫���ɖ߂�
                        MinusScale = -MinusScale;
                    }
                }
                else
                {
                    Move = false; // �����Ă��Ȃ�������Move��false
                    if (!StrechHeightRunning)// �R���[�`�������s���łȂ���΃R���[�`�����J�n
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
        StrechHeightRunning = true; // �R���[�`�����J�n�����̂Ńt���O��true�ɐݒ�
        while (!Move)// �����Ă��Ȃ�������
        {
            transform.localScale -= new Vector3(0, MinusScale, 0);
            MinusScale = -MinusScale;
            yield return new WaitForSeconds(StrechInterval);
        }
        StrechHeightRunning = false;// �R���[�`�����I�������̂Ńt���O��false�ɐݒ�
    }
}