using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;


[System.Serializable]
public class GalleryTextArray
{
    [Tooltip("キャラのセリフを入れる配列")]
    public string[] Text;
    [Tooltip("テキストフレームの位置")]
    public Vector2 TextFramePosition;
    [Tooltip("フラグがTrueになってから何秒後に表示")]
    public float CreatWaitTime;
    [Tooltip("表示されてから何秒後に非表示")]
    public float DestroyWaitTime;
}



public class GalleryTextManager : MonoBehaviour
{
    public GalleryTextArray[] GalleryTextArray;
    [SerializeField, Tooltip("生成するテキストフレーム")]
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
        yield return new WaitForSeconds(CreatWaitTime);//CreatWaitTime秒待つ
        GameObject TextFrameObj = Instantiate(TextFrame, transform);//プレハブをこのオブジェクトの子として生成
        TextFrameObj.transform.position = TextFramePosition;//TextFramePositionに移動
        GalleryTextFrame GalleryTextFrameScript = TextFrameObj.GetComponent<GalleryTextFrame>();
        GalleryTextFrameScript.Texts = Text;
        GalleryTextFrameScript.TextFrameStart();
        //DestrtoyWaitTime秒待って非表示にする
        yield return new WaitForSeconds(DestroyWaitTime);
        GalleryTextFrameScript.TextFrameEnd();
    }
}
