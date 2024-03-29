using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;


[System.Serializable]
public class GalleryTextArray
{
    [Tooltip("�L�����̃Z���t������z��")]
    public string[] Text;
    [Tooltip("�e�L�X�g�t���[���̈ʒu")]
    public Vector2 TextFramePosition;
    [Tooltip("�t���O��True�ɂȂ��Ă��牽�b��ɕ\��")]
    public float CreatWaitTime;
    [Tooltip("�\������Ă��牽�b��ɔ�\��")]
    public float DestroyWaitTime;
}



public class GalleryTextManager : MonoBehaviour
{
    public GalleryTextArray[] GalleryTextArray;
    [SerializeField, Tooltip("��������e�L�X�g�t���[��")]
    GameObject TextFrame;
    public bool Flag1;

    void Update()
    {
        if (Flag1)
        {
            for (int i = 0; i < GalleryTextArray.Length; i++)
            {
                StartCoroutine(GalleryTextCreat(GalleryTextArray[i].CreatWaitTime, GalleryTextArray[i].TextFramePosition, GalleryTextArray[i].Text, GalleryTextArray[i].DestroyWaitTime));
            }
            Flag1 = false;
        }
    }

    IEnumerator GalleryTextCreat(float CreatWaitTime, Vector2 TextFramePosition, string[] Text, float DestroyWaitTime)
    {
        yield return new WaitForSeconds(CreatWaitTime);//CreatWaitTime�b�҂�
        GameObject TextFrameObj = Instantiate(TextFrame, transform);//�v���n�u�����̃I�u�W�F�N�g�̎q�Ƃ��Đ���
        TextFrameObj.transform.position = TextFramePosition;//TextFramePosition�Ɉړ�
        GalleryTextFrame GalleryTextFrameScript = TextFrameObj.GetComponent<GalleryTextFrame>();
        GalleryTextFrameScript.Texts = Text;
        GalleryTextFrameScript.TextFrameStart();
        //DestrtoyWaitTime�b�҂��Ĕ�\���ɂ���
        yield return new WaitForSeconds(DestroyWaitTime);
        GalleryTextFrameScript.TextFrameEnd();
    }
}
