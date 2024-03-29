using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Popup : MonoBehaviour
{
    [Tooltip("ポップアップのRectTransfrom")]
    RectTransform rectTransform;
    [SerializeField, Tooltip("名前のテキスト")]
    TextMeshProUGUI NameTMP;
    [SerializeField, Tooltip("表示する画像のゲームオブジェクト")]
    Image Icon; 

    [SerializeField, Tooltip("ポップアップが表示される時間")]
    float SubmitPopupTime;
    [SerializeField, Tooltip("ポップアップが戻る座標")]
    Vector2 ReturnPostion;
    [SerializeField, Tooltip("ポップアップが移動する座標")]
    Vector2 MovePopupPosition;
    [SerializeField, Tooltip("ポップアップのスピード")]
    float MoveSpeed;
    [Tooltip("MovePopupのフラグ(0=実行していない 1=ポップアップが出ている間 2=ポップアップが戻る)")]
    int MovePopupRunning = 0;
    [Tooltip("ポップアップが２つ以上表示されたときに実行されるSweepPopupのフラグ")]
    int SweepPopupRunning = 0;

    void Start()
    {
        rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = ReturnPostion;//最初の座標に移動
    }

    void Update()
    {
        if (MovePopupRunning == 1 || MovePopupRunning == 2)
        {
            MovePopup();
        }
    }

    void MovePopup()
    {
        if (MovePopupRunning == 1) rectTransform.anchoredPosition = Vector2.Lerp(rectTransform.anchoredPosition, MovePopupPosition, MoveSpeed * Time.deltaTime);//ポップアップを表示
        else if (MovePopupRunning == 2) rectTransform.anchoredPosition = Vector2.Lerp(rectTransform.anchoredPosition, ReturnPostion, MoveSpeed * Time.deltaTime);//ポップアップを戻す
        
        if (SweepPopupRunning == 1 && MovePopupRunning == 1)
        {
            Vector2 SweepPopupPosition = MovePopupPosition + new Vector2(0, 30);
            rectTransform.anchoredPosition = Vector2.Lerp(rectTransform.anchoredPosition, SweepPopupPosition, MoveSpeed * Time.deltaTime);
        }
    }

    public IEnumerator MovePopupRun(string Name, Sprite Image)
    {
        NameTMP.text = Name;
        Icon.sprite = Image;

        MovePopupRunning = 1;
        yield return new WaitForSeconds(SubmitPopupTime);
        MovePopupRunning = 2;
        yield return new WaitForSeconds(SubmitPopupTime);
        Destroy(gameObject);
    }

    public void SweepPopup(int PopupCut)
    {
        Debug.Log("xx"); ;
        SweepPopupRunning = 1;
        ReturnPostion += new Vector2(0, 30*PopupCut);
    }
}