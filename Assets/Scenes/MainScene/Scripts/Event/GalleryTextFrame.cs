using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class GalleryTextFrame : MonoBehaviour
{
    [Tooltip("�L�����̃Z���t������z��")]
    public string[] Texts;
    [Tooltip("Text���\�������X�s�[�h(�b)")]
    public float TextSpeed;

    [Tooltip("�z��𐔂���")]
    int ArrayCount;
    [Tooltip("���ݕ\�����Ă���e�L�X�g")]
    string CurrentText = "";
    [SerializeField, Tooltip("�t���[�����傫���Ȃ�X�s�[�h")]
    float GrowSpeed;

    [Tooltip("TextMeshPro������")]
    public TextMeshProUGUI TextmeshPro;

    public void TextFrameStart()
    {
        TextmeshPro.gameObject.SetActive(true);//TextmeshPro��\������
        StartCoroutine(ZoomFrame());
    }

    IEnumerator ZoomFrame()
    {
        Vector3 DefaultScale = transform.localScale;
        float VerticalMagnification = transform.localScale.x / transform.localScale.y;
        transform.localScale = new Vector3(0, 0, 0);
        while (transform.localScale.x <= DefaultScale.x || transform.localScale.y <= DefaultScale.y)
        {
            transform.localScale += new Vector3(GrowSpeed * VerticalMagnification, GrowSpeed, GrowSpeed) * Time.deltaTime;
            yield return null;
        }
        TextFrameSet();
    }

    void TextFrameSet()
    {
        CurrentText = "";//�\�������e�L�X�g���J���ɂ���
        if (Texts.Length <= 0) Debug.Log("�e�L�X�g�t���[���G���[�F" + gameObject + "�̓e�L�X�g�������Ă��܂���");
        else StartCoroutine(ShowText());
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


    public void TextFrameEnd()
    {
        TextmeshPro.gameObject.SetActive(false);//TextmeshPro���\���ɂ���
        ArrayCount = 0;
        StartCoroutine(ZoomOutFrame());
    }

    IEnumerator ZoomOutFrame()
    {
        while (transform.localScale.x >= 0 && transform.localScale.y >= 0)
        {
            transform.localScale -= new Vector3(GrowSpeed * 4, GrowSpeed * 2, GrowSpeed) * Time.deltaTime;
            yield return null;
        }
        gameObject.SetActive(false);//TextFrame���\���ɂ���
    }
}
