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
    [SerializeField, Header("釣れた時に表示する文字。空のprefabを入れる")]
    GameObject FishText_Prefab;
    GameObject FishImage;
    GameObject FishNameText;
    bool FishNameTextMove;
    RectTransform FishImage_RectTransform;
    [HideInInspector] public bool ShakeRun;
    [SerializeField] AudioClip NewFishSound;
    [SerializeField] float NewFishSoundVolume;
    FishData fishData;
    void Update()
    {
        if (NewFishWindow_Born)
        {
            // ウィンドウを移動
            //NewFishWindow_RectTransform.anchoredPosition = Vector2.Lerp(NewFishWindow_RectTransform.anchoredPosition, new Vector2(0, 30), 2.5f * Time.deltaTime);
            // 魚の画像を縮小＆回転
            if (FishImage.transform.localScale.x > 0.09f*fishData.FishImageSize)
            {
                FishImage.transform.localScale -= new Vector3(0.5f * fishData.FishImageSize * Time.deltaTime, 0.5f * fishData.FishImageSize * Time.deltaTime, 0);
                FishImage_RectTransform.eulerAngles += new Vector3(0, 0, 2000 * Time.deltaTime);
            }
            else
            {
                FishImage.transform.localScale = new Vector3(0.08f*fishData.FishImageSize, 0.08f*fishData.FishImageSize, 0);
                FishImage.transform.rotation = new Quaternion(0, 0, 0, 0);
                if (!ShakeRun)
                {
                    ShakeRun = true;
                    // 画面を振動
                    GameObject.Find("Main Camera").GetComponent<Cam>().CamOneShake(0.1f, 0.1f, 0.1f);
                    // 魚の名前を生成
                    FishNameText = Instantiate(FishText_Prefab, new Vector3(0, 300, 0), Quaternion.identity, transform);
                    FishNameText.GetComponent<RectTransform>().position = new Vector3(Screen.width/2, Screen.height/10*6, 0);
                    FishNameText.GetComponent<Text>().text = fishData.FishName;
                    FishNameTextMove = true;
                }
            }
            if (FishNameTextMove)
            {
                //FishNameText.transform
            }
        }
    }

    public void NewFishWindow_Creat(FishData FishData)
    {
        // ウィンドウのプレハブを生成
        //NewFishWindow_Obj = Instantiate(NewFishWindow_Prefab, new Vector3(0, 300, 0), Quaternion.identity, transform);
        // ウィンドウの位置を移動
        //NewFishWindow_RectTransform = NewFishWindow_Obj.GetComponent<RectTransform>();
        //NewFishWindow_RectTransform.anchoredPosition = new Vector3(0, 300, 0);
        fishData = FishData;
        // 魚の画像を生成
        FishImage = Instantiate(FishImage_Prefab, new Vector3(0, 0, 0), Quaternion.identity, transform);
        FishImage.GetComponent<Image>().sprite = FishData.FishImage;
        // 魚の画像を移動＆拡大
        FishImage_RectTransform = FishImage.GetComponent<RectTransform>();
        FishImage_RectTransform.anchoredPosition = new Vector3(0, 300, 0);
        FishImage.transform.localScale = new Vector3(0.5f*fishData.FishImageSize, 0.5f*fishData.FishImageSize, 0);
        // 音を出す
        GetComponent<AudioSource>().PlayOneShot(NewFishSound, NewFishSoundVolume);
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
        FishNameTextMove = false;
        ShakeRun = false;
        NewFishWindow_Born = false;
        // ウィンドウを消す
        Destroy(NewFishWindow_Obj);
        Destroy(FishImage);
        Destroy(FishNameText);
    }
}
