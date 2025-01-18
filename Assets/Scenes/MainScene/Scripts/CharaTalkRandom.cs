using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharaTalkRandom : MonoBehaviour
{
    [SerializeField] GameObject TextFramePrefab;
    [SerializeField] Vector3 ShiftPosition;
    Text Text_Component;
    GameObject TextFrame_Obj;
    [SerializeField, Tooltip("キャラのセリフの配列")] List<TextsList> Texts;
    [Tooltip("配列を数える")] int ArrayCount;
    [Tooltip("現在表示しているテキスト")] string CurrentText = "";
    [SerializeField, Tooltip("Textが表示されるスピード(秒)")] float TextSpeed;

    float Distance;
    [SerializeField, Tooltip("話すことができる距離")] float CanTalkDistance;

    GameObject PlayerObj;
    Transform PlayerTrs;
    CharaOperation PlayerSclipt;
    Reaction reaction;

    AudioSource audioSource;
    [SerializeField] AudioClip Voice;
    [SerializeField] float VoiceVolume;

    bool CanTalk;

    Coroutine coroutine;
    [SerializeField] int GiveMoney;
    [SerializeField] Vector3 ReactionEfectPosition;

    int RandomIndex;
    void Start()
    {
        PlayerObj = GameObject.FindWithTag("Player");
        PlayerTrs = PlayerObj.GetComponent<Transform>();
        PlayerSclipt = PlayerObj.GetComponent<CharaOperation>();
        reaction = GameObject.Find("EventManager").GetComponent<Reaction>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (CanTalk && Input.GetKeyDown(KeyCode.Return))
        {
            if (ArrayCount == 0) TextFrameStart();
            else if (ArrayCount == Texts[RandomIndex].Texts.Count) TextFrameEnd();
            else TextFrameSet();
        }
        Distance = Vector3.Distance(PlayerTrs.position, transform.position);
        if (Distance <= CanTalkDistance)
        {
            if (!CanTalk)
            {
                reaction.Action_Create(transform, ReactionEfectPosition);
            }
            CanTalk = true;
        }
        else
        {
            if (CanTalk)
            {
                reaction.Action_FadeOut(0.5f);
            }
            CanTalk = false;
        }
    }

    void TextFrameStart()
    {
        RandomIndex = Random.Range(0, Texts.Count);
        PlayerTrs.GetComponent<ReiziValue>().isSpeak = true;
        PlayerSclipt.CanRun = false;//歩けなくする
        reaction.Action_FadeOut(0.1f);//アクションマークを消す
        Transform Canvas_Trs = GameObject.FindWithTag("CanvasWorld").transform;
        TextFrame_Obj = Instantiate(TextFramePrefab, transform.position + ShiftPosition, Quaternion.identity, Canvas_Trs);
        Text_Component = TextFrame_Obj.transform.Find("Text").GetComponent<Text>();
        TextFrameSet();
        PlayerObj.GetComponent<Animator>().SetBool("BackLook", false);
        PlayerObj.GetComponent<Animator>().SetBool("RunBack", false);
        GameObject.Find("Main Camera").GetComponent<Cam>().CamMove(6, (transform.position - PlayerTrs.position) / 2);//プレイヤーと吹き出しの中間にカメラを移動させる
    }

    void TextFrameEnd()
    {
        PlayerTrs.GetComponent<ReiziValue>().isSpeak = false;
        PlayerSclipt.CanRun = true;//歩けるようにする
        StopAllCoroutines();
        Destroy(TextFrame_Obj);
        ArrayCount = 0;
        GameObject.Find("Main Camera").GetComponent<Cam>().CamReset();
        GameObject.Find("Money").GetComponent<Money>().AddMoney(GiveMoney);
    }

    void TextFrameSet()
    {
        CurrentText = "";//表示されるテキストをカラにする
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
        coroutine = StartCoroutine(ShowText());
        ArrayCount += 1;//配列内の次のテキストへ
    }

    IEnumerator ShowText()//テキストを一文字ずつ表示する
    {
        string Text = Texts[RandomIndex].Texts[ArrayCount];
        for (int i = 0; i < Text.Length; i++)
        {
            CurrentText += Text[i];// 一文字ずつ追加します
            Text_Component.text = CurrentText; // テキストオブジェクトを更新します
            audioSource.PlayOneShot(Voice, VoiceVolume);
            yield return new WaitForSeconds(TextSpeed);
        }
        coroutine = null;
    }
}

[System.Serializable]
public class TextsList
{
    public List<string> Texts; 
}
