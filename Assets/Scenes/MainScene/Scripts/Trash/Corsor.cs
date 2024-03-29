using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corsor : MonoBehaviour
{
    [Tooltip("�����q�b�g�������ǂ���")]
    public bool Hit;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Fish"))
        {
            Hit = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Fish"))
        {
            Hit = false;
        }
    }
}

//����Corsor�ɓ�����
//���삪�ł��Ȃ��Ȃ�
//FishCatchBar���o��
//�G���^�[�������ƃo�[���i��