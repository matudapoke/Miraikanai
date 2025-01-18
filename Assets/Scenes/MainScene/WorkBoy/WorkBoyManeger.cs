using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorkBoyManeger : MonoBehaviour
{
    // ゲームオブジェクト
    GameObject Reizi_Obj;
    GameObject Camera_Obj;
    GameObject WorkBoyUI_Obj;
    GameObject MainUI_Obj;
    GameObject NoiseImage_Obj;
    // フラグ
    bool isOpenWorkBoy;
    // 値
    [SerializeField] Vector3 OpenPosition;
    Vector3 ClosePosition;
    [SerializeField] float OpenCloaseMoveSpeed;

    // カセット
    [Header("カセット")]
    [SerializeField] GameObject CassetteBaner_Prefab;
    public List<CassetteData> CassetteDataList = new List<CassetteData>();
    List<GameObject> CassetteBanerObjList = new List<GameObject>();
    [SerializeField] Vector3 CassetteBanerPosition;
    List<Vector3> CassetteBanerShiftPositionList = new List<Vector3>();
    [SerializeField] GameObject Selected_Prefab;
    GameObject Selected_Obj;
    int SelectedIndex;
    [SerializeField] AudioClip CassetteStartSound;

    List<AudioSource> OtherAudioSource = new List<AudioSource>();
    PerspecitveVolume WaveperspecitveVolume;
    int PlayingAudioIndex = 999;
    Coroutine Coroutine;

    // Image
    [SerializeField] GameObject NoiseImage_Prefab;
    Sprite OriginalImage;
    GameObject Image_Obj;

    void Start()
    {
        Reizi_Obj = GameObject.Find("Reizi");
        Camera_Obj = GameObject.Find("Main Camera");
        WorkBoyUI_Obj = GameObject.Find("WorkBoyUI");
        ClosePosition = WorkBoyUI_Obj.transform.position;
        MainUI_Obj = GameObject.Find("MainUI");
        Image_Obj = WorkBoyUI_Obj.transform.Find("Image").gameObject;
        OriginalImage = Image_Obj.GetComponent<Image>().sprite;

        WaveperspecitveVolume = GameObject.Find("SoundManager").GetComponent<PerspecitveVolume>();
        OtherAudioSource.Add(GameObject.Find("SoundManager").GetComponents<AudioSource>()[1]);
        OtherAudioSource.Add(Reizi_Obj.GetComponents<AudioSource>()[0]);
        OtherAudioSource.Add(Reizi_Obj.GetComponents<AudioSource>()[1]);
    }
    void Update()
    {
        // WorkBoyを開く・閉じる
        if (!isOpenWorkBoy && Input.GetKeyDown(KeyCode.Tab) && Reizi_Obj.GetComponent<ReiziValue>().ValueChack() && CassetteDataList.Count >= 1)
        {            StartWorkBoy();
        }
        else if (isOpenWorkBoy && Input.GetKeyDown(KeyCode.Tab))
        {
            EndWorkBoy();
        }

        // WorkBoyUIの移動
        if (isOpenWorkBoy)
        {
            if (WorkBoyUI_Obj.transform.position.x <= OpenPosition.x + 0.1f)
            {
                WorkBoyUI_Obj.transform.position = OpenPosition;
            }
            else
            {
                WorkBoyUI_Obj.transform.position = Vector3.Lerp(WorkBoyUI_Obj.transform.position, OpenPosition, OpenCloaseMoveSpeed);
            }
        }
        else
        {
            if (WorkBoyUI_Obj.transform.position.x >= ClosePosition.x - 0.1f)
            {
                WorkBoyUI_Obj.transform.position = ClosePosition;
                WorkBoyUI_Obj.SetActive(false);
            }
            else
            {
                WorkBoyUI_Obj.transform.position = Vector3.Lerp(WorkBoyUI_Obj.transform.position, ClosePosition, OpenCloaseMoveSpeed);
            }
        }

        // CassetteBanerの移動
        if (isOpenWorkBoy)
        {
            for (int i = 0; i < CassetteBanerObjList.Count; i++)
            {
                CassetteBanerObjList[i].transform.position = Vector3.Lerp(CassetteBanerObjList[i].transform.position, CassetteBanerPosition + CassetteBanerShiftPositionList[i], 5.0f * Time.deltaTime);
            }
        }
        else
        {
            if (WorkBoyUI_Obj.transform.position.x >= ClosePosition.x - 0.1f)
            {
                for (int i = 0; i < CassetteBanerObjList.Count; i++)
                {
                    Destroy(CassetteBanerObjList[i]);
                }
                CassetteBanerObjList.Clear();
                CassetteBanerShiftPositionList.Clear();
            }
            else
            {
                for (int i = 0; i < CassetteBanerObjList.Count; i++)
                {
                    CassetteBanerObjList[i].transform.position = Vector3.Lerp(CassetteBanerObjList[i].transform.position, new Vector3(3000, CassetteBanerPosition.y, 0) + CassetteBanerShiftPositionList[i], 5.0f * Time.deltaTime);
                }
            }
        }

        // カセットを選択
        if (isOpenWorkBoy)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) && SelectedIndex != 0)
            {
                SelectedIndex--;
                // 新しいセレクトオブジェクトを出す
                Destroy(Selected_Obj);
                Selected_Obj = Instantiate(Selected_Prefab, CassetteBanerObjList[SelectedIndex].transform.position, Quaternion.identity, CassetteBanerObjList[SelectedIndex].transform);
                // カセットバナーの位置をずらす
                for (int i = 0; i < CassetteBanerObjList.Count; i++)
                {
                    CassetteBanerShiftPositionList[i] += new Vector3 (i*10, -150, 0);
                }
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) && SelectedIndex != CassetteBanerObjList.Count - 1)
            {
                SelectedIndex++;
                // 新しいセレクトオブジェクトを出す
                Destroy(Selected_Obj);
                Selected_Obj = Instantiate(Selected_Prefab, CassetteBanerObjList[SelectedIndex].transform.position, Quaternion.identity, CassetteBanerObjList[SelectedIndex].transform);
                // カセットバナーの位置をずらす
                for (int i = 0; i < CassetteBanerObjList.Count; i++)
                {
                    CassetteBanerShiftPositionList[i] += new Vector3(i * -10, 150, 0);
                }
            }
        }

        // カセットを再生
        if (isOpenWorkBoy && Input.GetKeyDown(KeyCode.Return))
        {
            if (SelectedIndex == PlayingAudioIndex)
            {
                StopCassette();
            }
            else
            {
                Coroutine = StartCoroutine(StartCassette());
            }
        }
    }
    void StartWorkBoy()
    {
        // フラグを立てる
        isOpenWorkBoy = true;
        // レイジを動けなくする
        Reizi_Obj.GetComponent<CharaOperation>().CanRun = false;
        Reizi_Obj.GetComponent<ReiziValue>().isFishBook = true;
        // WorkBoyUIのCanvasを表示
        WorkBoyUI_Obj.SetActive(true);
        // カメラ移動
        Camera_Obj.GetComponent<Cam>().CamMove(5, new Vector3(4, 1.5f, 0));
        Camera_Obj.GetComponent<Cam>().CamZoom(5, 1.5f);
        // メインキャンバスを非表示にする
        MainUI_Obj.SetActive(false);
        // カセットバナー初期化
        for (int i = 0; i < CassetteBanerObjList.Count; i++)
        {
            Destroy(CassetteBanerObjList[i]);
        }
        CassetteBanerObjList.Clear();
        CassetteBanerShiftPositionList.Clear();
        // カセットのバナーを作成
        for (int i = 0; i < CassetteDataList.Count; i++)
        {
            CassetteBanerObjList.Add(Instantiate(CassetteBaner_Prefab, new Vector3(1500, CassetteBanerPosition.y, 0), Quaternion.identity, WorkBoyUI_Obj.transform.parent));
            CassetteBanerObjList[i].transform.Find("NameText").GetComponent<Text>().text = CassetteDataList[i].Name;
            CassetteBanerObjList[i].transform.Find("SubNameText").GetComponent<Text>().text = CassetteDataList[i].SubName;
            CassetteBanerShiftPositionList.Add(new Vector3(i * 10, i * -150, 0));
        }
        Selected_Obj = Instantiate(Selected_Prefab, CassetteBanerObjList[0].transform.position, Quaternion.identity, CassetteBanerObjList[0].transform);
        SelectedIndex = 0;
    }
    void EndWorkBoy()
    {
        // フラグを立てる
        isOpenWorkBoy = false;
        // レイジを動けるようにする
        Reizi_Obj.GetComponent<CharaOperation>().CanRun = true;
        Reizi_Obj.GetComponent<ReiziValue>().isFishBook = false;
        // カメラ移動
        Camera_Obj.GetComponent<Cam>().CamReset();
        // メインキャンバスを表示にする
        MainUI_Obj.SetActive(true);
    }
    IEnumerator StartCassette()
    {
        PlayingAudioIndex = SelectedIndex;
        // 効果音再生
        GetComponents<AudioSource>()[1].Stop();
        GetComponents<AudioSource>()[0].PlayOneShot(CassetteStartSound);
        GetComponents<AudioSource>()[0].Play();
        // WorkBoyNoiseのアニメーション
        NoiseImage_Obj = Instantiate(NoiseImage_Prefab, Image_Obj.transform.position, Quaternion.identity, WorkBoyUI_Obj.transform);
        // 周りの音を小さくする
        for (int i = 0; i < OtherAudioSource.Count; i++)
        {
            OtherAudioSource[i].volume = OtherAudioSource[i].volume /2;
        }
        WaveperspecitveVolume.Coeffcient = WaveperspecitveVolume.Coeffcient / 3;
        yield return new WaitForSeconds(1f);
        GetComponents<AudioSource>()[0].Stop();
        GetComponents<AudioSource>()[1].clip = CassetteDataList[SelectedIndex].Music;
        GetComponents<AudioSource>()[1].Play();
        // WorkBoyNoiseのアニメーション
        Destroy(NoiseImage_Obj);
        // 画像を差し替える
        Image_Obj.GetComponent<Image>().sprite = CassetteDataList[SelectedIndex].Image;
    }
    void StopCassette()
    {
        StopCoroutine(Coroutine);
        if (NoiseImage_Obj != null)
        {
            Destroy(NoiseImage_Obj);
        }
        Image_Obj.GetComponent<Image>().sprite = OriginalImage;
        GetComponents<AudioSource>()[1].Stop();
        GetComponents<AudioSource>()[0].Stop();
        GetComponent<AudioSource>().PlayOneShot(CassetteStartSound);
        PlayingAudioIndex = 999;
    }
}
