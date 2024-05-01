using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharaAround : MonoBehaviour
{
    [SerializeField, Tooltip("キャラのセリフを入れる配列")]
    string[] Texts;
    [Tooltip("配列の数")]
    int ArrayInt;
    [Tooltip("配列を数える")]
    int ArrayCount;
    [Tooltip("現在表示しているテキスト")]
    string CurrentText = "";
    [SerializeField, Tooltip("Textが表示されるスピード(秒)")]
    float TextSpeed;
    [SerializeField, Tooltip("吹き出しを入れる")]
    GameObject TextFrameObj;
    [SerializeField, Tooltip("TextMeshProを入れる")]
    TextMeshProUGUI TextmeshPro;
    [Tooltip("PulyerがCharaAroundniに侵入しているかどうか")]
    bool PlayerCollision;

    [Tooltip("プレイヤーのオブジェクト")]
    public GameObject PlayerObj;
    [Tooltip("プレイヤーのスクリプト")]
    CharaOperation PlayerSclipt;

    void Start()
    {
        TextFrameObj.SetActive(false);//吹き出しを非表示にする
        TextmeshPro.gameObject.SetActive(false);//テキストを非表示にする
        ArrayInt = Texts.Length;//配列の数を入れる
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
        PlayerSclipt.CanRun = false;//歩けなくする
        TextmeshPro.gameObject.SetActive(true);//TextmeshProを表示する
        TextFrameObj.SetActive(true);//TextFrameを表示する
        TextFrameSet();

        PlayerObj.GetComponent<Animator>().SetBool("BackLook",false);
        PlayerObj.GetComponent<Animator>().SetBool("RunBack", false);
        Transform PlayerTrs = PlayerObj.GetComponent<Transform>();
        GameObject.Find("Main Camera").GetComponent<Cam>().CamMove(6, (transform.position - PlayerTrs.position)/2);//プレイヤーと吹き出しの中間にカメラを移動させる
    }

    void TextFrameEnd()
    {
        PlayerSclipt.CanRun = true;//歩けるようにする
        TextFrameObj.SetActive(false);//TextFrameを非表示にする
        TextmeshPro.gameObject.SetActive(false);//TextmeshProを非表示にする
        ArrayCount = 0;
        GameObject.Find("Main Camera").GetComponent<Cam>().CamReset();
    }

    void TextFrameSet()
    {
        CurrentText = "";//表示されるテキストをカラにする
        StartCoroutine(ShowText());
        ArrayCount += 1;//配列内の次のテキストへ
    }

    IEnumerator ShowText()//テキストを一文字ずつ表示する
    {
        string Text = Texts[ArrayCount];
        for (int i = 0; i < Text.Length; i++)
        {
            CurrentText += Text[i];// 一文字ずつ追加します
            TextmeshPro.text = CurrentText; // テキストオブジェクトを更新します
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
