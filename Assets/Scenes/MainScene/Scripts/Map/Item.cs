using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField, Tooltip("�Ȃ�̋��H")]
    FishData FishData;

    [SerializeField, Tooltip("���Ƃ̑傫����������l")]
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

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (FishData != null)// �A�C�e��������������
            {
                // �A�C�e�����|�P�b�g�f�[�^�x�[�X�ɒǉ�
                GameObject FishDataBaseManagerObj = GameObject.Find("DataBaseManager");
                FishDataBeseManager FishDataBaseManagerScript = FishDataBaseManagerObj.GetComponent<FishDataBeseManager>();
                FishDataBaseManagerScript.AddFishData(FishData);
                
                // �|�b�v�A�b�v��\�����|�b�v�A�b�v�R���g���[���Ɏw��
                GameObject PopupControllerObj = GameObject.Find("PopupController");
                PopupController PopupControllerScript = PopupControllerObj.GetComponent<PopupController>();
                PopupControllerScript.SubmitPopup(FishData.FishName , FishData.FishImage);
            }

            Destroy(gameObject);// �Q�[���I�u�W�F�N�g�������Ƃ��̂��Ƃ̃R�[�h�͎��s����Ȃ�
        }
    }
}
