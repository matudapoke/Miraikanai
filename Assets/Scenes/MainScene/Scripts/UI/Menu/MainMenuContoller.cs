using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenuContoller : MonoBehaviour
{
    // 値
    [SerializeField] Vector3 WindowMovePosition;
    // ゲームオブジェクト
    [SerializeField] GameObject Selected_Prefab;
    [HideInInspector] public GameObject Selected_Obj;
    // フラグ
    bool WindowOpen;
    [HideInInspector] public bool CanMainMenu;
    // コンポーネント
    CharaOperation charaOperation;
    Cam CamScript;
    FishingManager fishingManager;
    // MainMenu
    GameObject MainMenuWindow_Obj;
    bool MainMenuWindow_Move;
    Vector3 MainMenuWindow_MovePosition;
    // EquipmentMwnu
    bool EquipmentMenuWindow_Open = false;
    GameObject EquipmentMenuWindow_Obj;
    bool EquipmentMenuWindow_Move;
    Vector3 EquipmentMenuWindow_MovePosition;
    // FishingMwnu
    bool FishingMenuWindow_Open = false;
    GameObject FishingMenuWindow_Obj;
    bool FishingMenuWindow_Move;
    Vector3 FishingMenuWindow_MovePosition;

    void Start()
    {
        // フラグ
        CanMainMenu = true;
        // ゲームオブジェクト
        MainMenuWindow_Obj = transform.Find("MainMenuWindow").gameObject;
        EquipmentMenuWindow_Obj = transform.Find("EquipmentMenuWindow").gameObject;
        FishingMenuWindow_Obj = transform.Find("FishingMenuWindow").gameObject;
        // コンポーネント
        charaOperation = GameObject.Find("Reizi").GetComponent<CharaOperation>();
        CamScript = GameObject.Find("Main Camera").GetComponent<Cam>();
        fishingManager = GameObject.Find("Reizi").GetComponent<FishingManager>();
        // 最初は選択不可にする（コンポーネントをオフにする）
        MenuSelectSet(MainMenuWindow_Obj, false);
        MenuSelectSet(EquipmentMenuWindow_Obj, false);
        MenuSelectSet(FishingMenuWindow_Obj, false);
    }

    void Update()
    {
        // MainMenuWindowを出す
        if (Input.GetKeyDown(KeyCode.Tab) && CanMainMenu)
        {
            if (!WindowOpen)
            {
                MainMenuWindowOpen();
            }
            else
            {
                WindowClose();
            }
        }
        // Windowを一つ戻す
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.X))  && WindowOpen && CanMainMenu)
        {
            if (EquipmentMenuWindow_Open)
            {
                EquipmentMenuWindowClose();
            }
            else if (FishingMenuWindow_Open)
            {
                FishingMenuWindowClose();
            }
            else
            {
                WindowClose();
            }
        }
        // MenuWindowを動かす
        if (MainMenuWindow_Move)
        {
            MainMenuWindow_Obj.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(MainMenuWindow_Obj.GetComponent<RectTransform>().anchoredPosition, MainMenuWindow_MovePosition, 7.5f * Time.deltaTime);
        }
        // EquipmentWindowを動かす
        if (EquipmentMenuWindow_Move)
        {
            EquipmentMenuWindow_Obj.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(EquipmentMenuWindow_Obj.GetComponent<RectTransform>().anchoredPosition, EquipmentMenuWindow_MovePosition, 7.5f * Time.deltaTime);
        }
        // FishingWindowを動かす
        if (FishingMenuWindow_Move)
        {
            FishingMenuWindow_Obj.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(FishingMenuWindow_Obj.GetComponent<RectTransform>().anchoredPosition, FishingMenuWindow_MovePosition, 7.5f * Time.deltaTime);
        }
    }

    void MainMenuWindowOpen()
    {
        if (!WindowOpen)
        {
            WindowOpenStart();
        }
        // MainMenuWindowを出す
        MainMenuWindow_Move = true;
        MainMenuWindow_MovePosition = WindowMovePosition;
        // コンポーネントをオンにする
        MenuSelectSet(MainMenuWindow_Obj, true);
        // 最初の子を選択する
        EventSystem.current.SetSelectedGameObject(MainMenuWindow_Obj.transform.GetChild(0).gameObject);
        Selected_Obj.transform.SetParent(MainMenuWindow_Obj.transform.GetChild(0));
    }
    public void EquipmentMenuWindowOpen()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (!WindowOpen)
            {
                WindowOpenStart();
            }
            // EquipmentMenuWindowを出す
            EquipmentMenuWindow_Open = true;
            EquipmentMenuWindow_Move = true;
            EquipmentMenuWindow_MovePosition = WindowMovePosition;
            // コンポーネントをオンにする
            MenuSelectSet(EquipmentMenuWindow_Obj, true);
            // 最初の子を選択する
            EventSystem.current.SetSelectedGameObject(EquipmentMenuWindow_Obj.transform.GetChild(0).gameObject);
            Selected_Obj.transform.SetParent(EquipmentMenuWindow_Obj.transform.GetChild(0));
            // MainMenuWindowを移動
            MainMenuWindow_MovePosition -= new Vector3(100, 0, 0);
            MenuSelectSet(MainMenuWindow_Obj, false);
        }
    }
    void EquipmentMenuWindowClose()
    {
        // Windowを戻す
        EquipmentMenuWindow_Open = false;
        EquipmentMenuWindow_MovePosition = new Vector3(1500, 0, 0);
        MenuSelectSet(EquipmentMenuWindow_Obj, false);
        // MainMenuWindowを出す
        MainMenuWindow_Obj.SetActive(true);
        MenuSelectSet(MainMenuWindow_Obj, true);
        MainMenuWindowOpen();
    }
    public void FishingMenuWindowOpen()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (!WindowOpen)
            {
                WindowOpenStart();
            }
            // FishingMenuWindowを出す
            FishingMenuWindow_Open = true;
            FishingMenuWindow_Move = true;
            FishingMenuWindow_MovePosition = WindowMovePosition;
            // コンポーネントをオンにする
            MenuSelectSet(FishingMenuWindow_Obj, true);
            // 最初の子を選択する
            EventSystem.current.SetSelectedGameObject(FishingMenuWindow_Obj.transform.GetChild(0).gameObject);
            Selected_Obj.transform.SetParent(FishingMenuWindow_Obj.transform.GetChild(0));
            // MainMenuWindowを移動
            MainMenuWindow_MovePosition -= new Vector3(100, 0, 0);
            MenuSelectSet(MainMenuWindow_Obj, false);
        }
        if (!CanMainMenu)
        {
            if (!WindowOpen)
            {
                WindowOpenStart();
            }
            // FishingMenuWindowを出す
            FishingMenuWindow_Open = true;
            FishingMenuWindow_Move = true;
            FishingMenuWindow_MovePosition = WindowMovePosition;
            // コンポーネントをオンにする
            MenuSelectSet(FishingMenuWindow_Obj, true);
            // 最初の子を選択する
            EventSystem.current.SetSelectedGameObject(FishingMenuWindow_Obj.transform.GetChild(0).gameObject);
            Selected_Obj.transform.SetParent(FishingMenuWindow_Obj.transform.GetChild(0));
        }
    }
    public void FishingMenuWindowClose()
    {
        // Windowを戻す
        FishingMenuWindow_Open = false;
        FishingMenuWindow_MovePosition = new Vector3(1500, 0, 0);
        MenuSelectSet(FishingMenuWindow_Obj, false);
        if (CanMainMenu)
        {
            // MainMenuWindowを出す
            MainMenuWindow_Obj.SetActive(true);
            MenuSelectSet(MainMenuWindow_Obj, true);
            MainMenuWindowOpen();
        }
    }
    void WindowOpenStart()
    {
        if (fishingManager.phase != FishingManager.Phase.StartFishing)
        {
            // カメラを動かす
            CamScript.CamMove(10, new Vector3(1.1f, 0, 0));
            CamScript.CamZoom(10, 2.5f);
            // キャラを操作できなくする
            charaOperation.CanRun = false;
        } 
        // Selectedを出す
        Selected_Obj = Instantiate(Selected_Prefab, transform.position, Quaternion.identity);
        // フラグ
        WindowOpen = true;
        fishingManager.CanFishing = false;
    }
    void WindowClose()
    {
        // Windowを戻す
        EquipmentMenuWindowClose();
        FishingMenuWindowClose();
        MainMenuWindow_MovePosition = new Vector3(1500, 0, 0);
        MenuSelectSet(MainMenuWindow_Obj, false);
        // カメラを戻す
        CamScript.CamReset();
        // キャラを操作できるようにする
        charaOperation.CanRun = true;
        // Selectedを消す
        Destroy(Selected_Obj);
        // フラグを戻す
        WindowOpen = false;
        fishingManager.CanFishing = true;
    }
    void MenuSelectSet(GameObject obj, bool value)
    {
        Transform children = obj.GetComponentInChildren<Transform>();
        //子要素がいなければ終了
        if (children.childCount == 0)
        {
            return;
        }
        foreach (Transform ob in children)
        {
            if (ob.GetComponent<MenuSelect>() != null)
            {
                ob.GetComponent<MenuSelect>().enabled = value;
            }
        }
    }
}
