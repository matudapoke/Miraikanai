using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindowController : MonoBehaviour
{
    [SerializeField, Header("新種の魚")]
    GameObject NewFishWindow_Prefab;
    GameObject NewFishWindow_Obj;
    RectTransform NewFishWindow_RectTransform;
    bool NewFishWindow_Born;
    [SerializeField, Header("釣れた時に表示する魚の画像。空のprefabを入れる")]
    GameObject FishImage_Prefab;
    GameObject FishImage;
    RectTransform FishImage_RectTransform;
    [HideInInspector] public bool ShakeRun;
    void Update()
    {
        if (NewFishWindow_Born)
        {
            // ウィンドウを移動
            NewFishWindow_RectTransform.anchoredPosition = Vector2.Lerp(NewFishWindow_RectTransform.anchoredPosition, new Vector2(0, 30), 2.5f * Time.deltaTime);
            // 魚の画像を移動
            FishImage_RectTransform.anchoredPosition = Vector2.Lerp(FishImage_RectTransform.anchoredPosition, new Vector2(0, 30), 2.5f * Time.deltaTime);
            // 一度だけカメラを振動
            if (NewFishWindow_RectTransform.anchoredPosition.y >= FishImage_RectTransform.anchoredPosition.y - 3 && !ShakeRun)
            {
                ShakeRun = true;
                GameObject.Find("Main Camera").GetComponent<Cam>().CamOneShake(0.1f, 0.1f, 0.1f);
            }
        }
    }

    public void NewFishWindow_Creat(FishData FishData)
    {
        // ウィンドウのプレハブを生成
        NewFishWindow_Obj = Instantiate(NewFishWindow_Prefab, new Vector3(0, 300, 0), Quaternion.identity, transform);
        // ウィンドウの位置を移動
        NewFishWindow_RectTransform = NewFishWindow_Obj.GetComponent<RectTransform>();
        NewFishWindow_RectTransform.anchoredPosition = new Vector3(0, 300, 0);
        // 魚の画像を生成
        FishImage = Instantiate(FishImage_Prefab, new Vector3(0, 300, 0), Quaternion.identity, transform);
        FishImage.GetComponent<Image>().sprite = FishData.FishImage;
        // 魚の画像を移動
        FishImage_RectTransform = FishImage.GetComponent<RectTransform>();
        FishImage_RectTransform.anchoredPosition = new Vector3(0, 600, 0);
        // フラグを立てる
        NewFishWindow_Born = true;
        // 新種の魚じゃなくする
        FishData.NewFish = false;
        // ウィンドウのゲームオブジェクトを返す
    }
    public void NewFishWindow_Click()
    {
        // ウィンドウを移動
        NewFishWindow_RectTransform.anchoredPosition = new Vector2(0, 30);
        // 魚の画像を移動
        FishImage_RectTransform.anchoredPosition = new Vector2(0, 30);
    }
    public void NewFishWindow_Destroy()
    {
        // フラグを戻す
        ShakeRun = false;
        NewFishWindow_Born = false;
        // ウィンドウを消す
        Destroy(NewFishWindow_Obj);
        Destroy(FishImage);
    }
}
