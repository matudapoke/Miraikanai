using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenuContoller : MonoBehaviour
{
    // フラグ
    public bool CanMainMenuOpen;
    [HideInInspector]
    public bool MenuNow;
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
    MainMenu mainMenu;
    Cam CamScript;
    CharaOperation charaOperation;
    FishingManager fishingManager;
    AudioSource audioSource;

    void Start()
    {
        mainMenu = transform.Find("MainMenuWindow").GetComponent<MainMenu>();
        CamScript = GameObject.Find("Main Camera").GetComponent<Cam>();
        charaOperation = GameObject.Find("Reizi").GetComponent<CharaOperation>();
        fishingManager = GameObject.Find("Reizi").GetComponent<FishingManager>();
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && CanMainMenuOpen)
        {
            if (!mainMenu.MenuWindowMove1)
            {
                mainMenu.MenuWindowMove1 = true;
                MainMenuStart();
            }
            else
            {
                mainMenu.MenuWindowMove1 = false;
                MainMenuEnd();
            }
        }
        if (Input.GetKeyDown(KeyCode.Tab) && fishingManager.phase == FishingManager.Phase.StartFishing)
        {
            // ↓魚メニューにする
            if (!mainMenu.MenuWindowMove1)
            {
                mainMenu.MenuWindowMove1 = true;
                fishingManager.FishingMenu = true;
            }
            else
            {
                mainMenu.MenuWindowMove1 = false;
                fishingManager.FishingMenu = false;
            }
        }
    }
    public void MainMenuStart()
    {
        // カメラを動かす
        CamScript.CamMove(10, new Vector3(1.4f, 0, 0));
        CamScript.CamZoom(10, 2.5f);
        // キャラを操作できなくする
        charaOperation.CanRun = false;
        // フラグ
        MenuNow = true;
        transform.Find("MainMenuWindow").transform.Find("1").GetComponent<MenuSelect>().Select();
        audioSource.Play();
    }
    public void MainMenuEnd()
    {
        // カメラを戻す
        CamScript.CamReset();
        // キャラを操作できなくする
        charaOperation.CanRun = true;
        // フラグ
        MenuNow = false;
    }
}
