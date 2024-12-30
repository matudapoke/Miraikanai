using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishBookManager : MonoBehaviour
{
    // フラグ
    bool isOpenFishBook;
    bool isOpenFishInfo;
    bool isChangeValue;
    // オブジェクト
    [SerializeField] GameObject FishBookCursor_Obj;
    [SerializeField] GameObject FishBookUI_Obj;
    [HideInInspector] public GameObject SelectedFishImage_Obj;
    GameObject Reizi_Obj;
    GameObject MainCanvas_Obj;
    GameObject MainCamera_Obj;
    GameObject CameraFade_Obj;
    GameObject FishBookFilter_Obj;
    // 値
    Vector3 CameraChift_tmp;

    private void Start()
    {
        Reizi_Obj = GameObject.FindWithTag("Player");
        GameObject CameraParent_Obj = GameObject.Find("Camera");
        MainCanvas_Obj = GameObject.Find("CanvasUI");
        MainCamera_Obj = CameraParent_Obj.transform.Find("Main Camera").gameObject;
        CameraFade_Obj= CameraParent_Obj.transform.Find("CameraFade").gameObject;
        FishBookFilter_Obj = CameraParent_Obj.transform.Find("FishBookFilter").gameObject;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && !isChangeValue)
        {
            if (!isOpenFishBook && Reizi_Obj.GetComponent<ReiziValue>().ValueChack())
            {
                StartCoroutine(StartFishBook());
            }
            else
            {
                StartCoroutine(EndFishBook());
            }
        }
        /*
        if (!isChangeValue && Reizi_Obj.GetComponent<ReiziValue>().ValueChack() && Input.GetKeyDown(KeyCode.Q))
        {
            // 魚図鑑を開ける
            if (!isOpenFishBook)
            {
                StartCoroutine(StartFishBook());
            }
            // 魚図鑑を閉める
            else
            {
                StartCoroutine(EndFishBook());
            }
        }
        */
        else if (!isChangeValue )
        if (SelectedFishImage_Obj != null && Input.GetKeyDown(KeyCode.Return))
        {
            // 情報を開く
            if (!isOpenFishBook && SelectedFishImage_Obj.GetComponent<SpriteRenderer>().color == Color.white)
            {
                FishImage fishImage = SelectedFishImage_Obj.GetComponent<FishImage>();
                Cam cam = MainCamera_Obj.GetComponent<Cam>();
                // フラグを立てる
                isOpenFishInfo = true;
                // FishBookCorsorを動けなくする
                FishBookCursor_Obj.GetComponent<CharaOperation>().CanRun = false;
                // 魚の名前を表示
                FishBookUI_Obj.transform.Find("FishName").GetComponent<Text>().text = fishImage.fishData.FishName;
                // カメラ操作
                cam.ChangeTarget(gameObject.transform);
                cam.CamZoom(5, 90.0f / fishImage.fishData.FishImageSize);
                cam.ShiftPos = new Vector3(fishImage.fishData.FishImageSize / 32, 0, 0);
            }
            // 情報を閉じる
            else if (isOpenFishBook)
            {
                FishImage fishImage = SelectedFishImage_Obj.GetComponent<FishImage>();
                Cam cam = MainCamera_Obj.GetComponent<Cam>();
                // フラグを立てる
                isOpenFishBook = false;
                // 名前を消す
                FishBookUI_Obj.transform.Find("FishName").GetComponent<Text>().text = "";
                // FishBookCorsorを動かせるようにする
                FishBookCursor_Obj.GetComponent<CharaOperation>().CanRun = true;
                // カメラ操作
                cam.ChangeTarget(FishBookCursor_Obj.transform);
                cam.CamZoom(5, fishImage.fishData.FishImageSize /90);
                cam.ShiftPos = new Vector3(0, 0, 0);
            }

        }
    }
    IEnumerator StartFishBook()
    {
        // フラグを立てる
        isOpenFishBook = true;
        isChangeValue = true;
        // レイジを動けなくする&レイジアニメーション開始
        Reizi_Obj.GetComponent<ReiziValue>().isFishBook = true;
        Reizi_Obj.GetComponent<Animator>().SetBool("IdolA", true);
        Reizi_Obj.GetComponent<CharaOperation>().CanRun = false;
        // FishBookCursorを表示
        FishBookCursor_Obj.transform.position = transform.position;
        FishBookCursor_Obj.SetActive(true);
        // メインUIを非表示にする
        MainCanvas_Obj.SetActive(false);
        // フェードアウト
        CameraFade_Obj.GetComponent<Fade>().FadeStart(0.1f);
        yield return new WaitForSeconds(0.5f);
        // カメラ操作
        CameraChift_tmp = MainCamera_Obj.GetComponent<Cam>().ShiftPos;
        MainCamera_Obj.GetComponent<Cam>().ShiftPos = new Vector3(0, 0, 0);
        MainCamera_Obj.GetComponent<Cam>().ChangeTarget(FishBookCursor_Obj.transform);
        yield return new WaitForSeconds(0.5f);
        // フェードイン
        FishBookFilter_Obj.GetComponent<Fade>().FadeStart(0.5f);
        CameraFade_Obj.GetComponent<Fade>().FadeEnd(0.5f);
        yield return new WaitForSeconds(0.3f);
        // レイジアニメーション終了
        Reizi_Obj.GetComponent<Animator>().SetBool("IdolA", false);
        // FishBookCorsorを動かせるようにする
        FishBookCursor_Obj.GetComponent<CharaOperation>().CanRun = true;
        // フラグを立てる
        isChangeValue = false;
    }
    IEnumerator EndFishBook()
    {
        isOpenFishBook = false;
        isChangeValue = true;
        // FishBookCursorを非表示にする
        FishBookCursor_Obj.SetActive(false);
        // FishBookCorsorを動けなくする
        FishBookCursor_Obj.GetComponent<CharaOperation>().CanRun = false;
        // レイジを動けるようにする&レイジアニメーション
        Reizi_Obj.GetComponent<ReiziValue>().isFishBook = false;
        Reizi_Obj.GetComponent<Animator>().SetBool("IdolA", true);
        // フェードアウト
        CameraFade_Obj.GetComponent<Fade>().FadeStart(0.5f);
        yield return new WaitForSeconds(0.5f);
        // カメラ操作
        MainCamera_Obj.GetComponent<Cam>().ShiftPos = CameraChift_tmp;
        MainCamera_Obj.GetComponent<Cam>().ChangeTarget(Reizi_Obj.transform);
        yield return new WaitForSeconds(0.5f);
        // フェードイン
        FishBookFilter_Obj.GetComponent<Fade>().FadeEnd(0.5f);
        CameraFade_Obj.GetComponent<Fade>().FadeEnd(0.1f);
        // メインUIを表示
        MainCanvas_Obj.SetActive(true);
        // レイジを動けるようにする
        Reizi_Obj.GetComponent<Animator>().SetBool("IdolA", false);
        Reizi_Obj.GetComponent<CharaOperation>().CanRun = true;
        // フラグを立てる
        isChangeValue = false;
    }
}
