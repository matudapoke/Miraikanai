using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharaTextFrame : MonoBehaviour
{
    [SerializeField] GameObject TextFramePrefab;
    Text Text_Component;
    GameObject TextFrame_Obj;
    [SerializeField, Tooltip("キャラのセリフの配列")]string[] Texts;
    [Tooltip("配列の数")]int ArrayInt;
    [Tooltip("配列を数える")]int ArrayCount;
    [Tooltip("現在表示しているテキスト")]string CurrentText = "";
    [SerializeField, Tooltip("Textが表示されるスピード(秒)")]float TextSpeed;
    
    GameObject PlayerObj;
    CharaOperation PlayerSclipt;

    bool PlayerCollision;

    void Start()
    {
        ArrayInt = Texts.Length;//配列の数を入れる
        PlayerObj = GameObject.FindWithTag("Player");
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
        Transform Canvas_Trs = GameObject.FindWithTag("CanvasWorld").transform;
        TextFrame_Obj = Instantiate(TextFramePrefab, transform.position, Quaternion.identity, Canvas_Trs);
        Text_Component = TextFrame_Obj.transform.Find("Text").GetComponent<Text>();
        TextFrameSet();
        PlayerObj.GetComponent<Animator>().SetBool("BackLook",false);
        PlayerObj.GetComponent<Animator>().SetBool("RunBack", false);
        Transform PlayerTrs = PlayerObj.GetComponent<Transform>();
        GameObject.Find("Main Camera").GetComponent<Cam>().CamMove(6, (transform.position - PlayerTrs.position)/2);//プレイヤーと吹き出しの中間にカメラを移動させる
    }

    void TextFrameEnd()
    {
        PlayerSclipt.CanRun = true;//歩けるようにする
        Destroy(TextFrame_Obj);
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
            Text_Component.text = CurrentText; // テキストオブジェクトを更新します
            yield return new WaitForSeconds(TextSpeed);
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag=="Player")
        {
            PlayerCollision = true;
            Debug.Log("Aaa");
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerCollision = false;
        }
    }
}
