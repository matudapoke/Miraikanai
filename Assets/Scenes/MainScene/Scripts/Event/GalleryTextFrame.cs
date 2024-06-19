using System.Collections;
using TMPro;
using UnityEngine;

public class GalleryTextFrame : MonoBehaviour
{
    [Tooltip("キャラのセリフを入れる配列")]
    public string[] Texts;
    [Tooltip("Textが表示されるスピード(秒)")]
    public float TextSpeed;

    [Tooltip("配列を数える")]
    int ArrayCount;
    [Tooltip("現在表示しているテキスト")]
    string CurrentText = "";
    [SerializeField, Tooltip("フレームが大きくなるスピード")]
    float GrowSpeed;

    [Tooltip("TextMeshProを入れる")]
    public TextMeshProUGUI TextmeshPro;

    public void TextFrameStart()
    {
        TextmeshPro.gameObject.SetActive(true);//TextmeshProを表示する
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
        CurrentText = "";//表示されるテキストをカラにする
        if (Texts.Length <= 0) Debug.Log("テキストフレームエラー：" + gameObject + "はテキストが入っていません");
        else StartCoroutine(ShowText());
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


    public void TextFrameEnd()
    {
        TextmeshPro.gameObject.SetActive(false);//TextmeshProを非表示にする
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
        gameObject.SetActive(false);//TextFrameを非表示にする
    }
}
