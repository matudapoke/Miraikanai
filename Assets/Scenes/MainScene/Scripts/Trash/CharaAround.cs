using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharaAround : MonoBehaviour
{
    [SerializeField, Tooltip("�L�����̃Z���t������z��")]
    string[] Texts;
    [Tooltip("�z��̐�")]
    int ArrayInt;
    [Tooltip("�z��𐔂���")]
    int ArrayCount;
    [Tooltip("���ݕ\�����Ă���e�L�X�g")]
    string CurrentText = "";
    [SerializeField, Tooltip("Text���\�������X�s�[�h(�b)")]
    float TextSpeed;
    [SerializeField, Tooltip("�����o��������")]
    GameObject TextFrameObj;
    [SerializeField, Tooltip("TextMeshPro������")]
    TextMeshProUGUI TextmeshPro;
    [Tooltip("Pulyer��CharaAroundni�ɐN�����Ă��邩�ǂ���")]
    bool PlayerCollision;

    [Tooltip("�v���C���[�̃I�u�W�F�N�g")]
    public GameObject PlayerObj;
    [Tooltip("�v���C���[�̃X�N���v�g")]
    CharaOperation PlayerSclipt;

    void Start()
    {
        TextFrameObj.SetActive(false);//�����o�����\���ɂ���
        TextmeshPro.gameObject.SetActive(false);//�e�L�X�g���\���ɂ���
        ArrayInt = Texts.Length;//�z��̐�������
        PlayerSclipt = PlayerObj.GetComponent<CharaOperation>();
    }

    void Update()
    {
        if(PlayerCollision && Input.GetKeyDown(KeyCode.Return))
        {
            if (ArrayCount == 0) TextFrameStart();
            else if (ArrayCount == ArrayInt) TextFrameEnd();
            else TextFrameSet();
        }
    }

    void TextFrameStart()
    {
        PlayerSclipt.CanRun = false;//�����Ȃ�����
        TextmeshPro.gameObject.SetActive(true);//TextmeshPro��\������
        TextFrameObj.SetActive(true);//TextFrame��\������
        TextFrameSet();

        PlayerObj.GetComponent<Animator>().SetBool("BackLook",false);
        PlayerObj.GetComponent<Animator>().SetBool("RunBack", false);
        Transform PlayerTrs = PlayerObj.GetComponent<Transform>();
        GameObject.Find("Main Camera").GetComponent<Cam>().CamMove(6, (transform.position - PlayerTrs.position)/2);//�v���C���[�Ɛ����o���̒��ԂɃJ�������ړ�������
    }

    void TextFrameEnd()
    {
        PlayerSclipt.CanRun = true;//������悤�ɂ���
        TextFrameObj.SetActive(false);//TextFrame���\���ɂ���
        TextmeshPro.gameObject.SetActive(false);//TextmeshPro���\���ɂ���
        ArrayCount = 0;
        GameObject.Find("Main Camera").GetComponent<Cam>().CamReset();
    }

    void TextFrameSet()
    {
        CurrentText = "";//�\�������e�L�X�g���J���ɂ���
        StartCoroutine(ShowText());
        ArrayCount += 1;//�z����̎��̃e�L�X�g��
    }

    IEnumerator ShowText()//�e�L�X�g���ꕶ�����\������
    {
        string Text = Texts[ArrayCount];
        for (int i = 0; i < Text.Length; i++)
        {
            CurrentText += Text[i];// �ꕶ�����ǉ����܂�
            TextmeshPro.text = CurrentText; // �e�L�X�g�I�u�W�F�N�g���X�V���܂�
            yield return new WaitForSeconds(TextSpeed);
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerCollision = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerCollision = false;
        }
    }
}
