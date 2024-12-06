using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenuContoller : MonoBehaviour
{
    [SerializeField] GameObject HomeMenu_Obj;
    // フラグ
    public bool CanMainMenuOpen;
    [HideInInspector]public bool MenuOpenNow;
    // 値
    [Tooltip("メニューがどの位置に移動してくるか")]
    public Vector3 MenuWindowMovePosition1;
    [Tooltip("メニューが1つ重なっているときメニューがどの位置に移動してくるか")]
    public Vector3 MenuWindowMovePosition2;
    [Tooltip("メニューが2つ重なっているときメニューがどの位置に移動してくるか")]
    public Vector3 MenuWindowMovePosition3;
    [Tooltip("メニューがどの位置に戻るか")]
    public Vector3 MenuWindowRetrunPosition;
    [Tooltip("メニューが移動する速さ)")]
    public float MenuMoveSpeed;
    // コンポーネント
    MainMenu HomeMenu_Script;
    Cam CamScript;
    CharaOperation charaOperation;
    FishingManager fishingManager;
    AudioSource audioSource;

    void Start()
    {
        HomeMenu_Script = HomeMenu_Obj.GetComponent<MainMenu>();
        CamScript = GameObject.Find("Main Camera").GetComponent<Cam>();
        charaOperation = GameObject.Find("Reizi").GetComponent<CharaOperation>();
        fishingManager = GameObject.Find("Reizi").GetComponent<FishingManager>();
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && CanMainMenuOpen)
        {
            if (!MenuOpenNow)
            {
                HomeMenu_Script.MenuWindowMoveNextPosition();
                MenuStart();
            }
            else
            {
                MenuEnd();
            }
        }
        /*
        if (Input.GetKeyDown(KeyCode.Tab) && fishingManager.phase == FishingManager.Phase.StartFishing)
        {
            // ↓魚メニューにする
            if (!FirstMenu.MenuWindowMove1)
            {
                FirstMenu.MenuWindowMove1 = true;
                fishingManager.FishingMenu = true;
            }
            else
            {
                FirstMenu.MenuWindowMove1 = false;
                fishingManager.FishingMenu = false;
            }
        }
        */
    }
    public void MenuStart()
    {
        // カメラを動かす
        CamScript.CamMove(10, new Vector3(1.4f, 0, 0));
        CamScript.CamZoom(10, 2.5f);
        // キャラを操作できなくする
        charaOperation.CanRun = false;
        // フラグ
        MenuOpenNow = true;

        transform.Find("MainMenuWindow").transform.Find("1").GetComponent<MenuSelect>().Select();
        audioSource.Play();
    }
    public void MenuEnd()
    {
        // カメラを戻す
        CamScript.CamReset();
        // キャラを操作できなくする
        charaOperation.CanRun = true;
        // フラグ
        MenuOpenNow = false;

        audioSource.Play();
    }
}
