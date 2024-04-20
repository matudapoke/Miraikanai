using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenuContoller : MonoBehaviour
{
    // 値
    [Tooltip("メニューがどの位置に移動してくるか")]
    public Vector3 MenuWindowMovePosition;
    [Tooltip("メニューがどの位置に戻るか")]
    public Vector3 MenuWindowRetrunPosition;
    [Tooltip("メニューが移動する速さ)")]
    public float MenuMoveSpeed;
    // コンポーネント
    MainMenu mainMenu;
    Cam CamScript;
    CharaOperation charaOperation;

    void Start()
    {
        mainMenu = transform.Find("MainMenuWindow").GetComponent<MainMenu>();
        CamScript = GameObject.Find("Main Camera").GetComponent<Cam>();
        charaOperation = GameObject.Find("Reizi").GetComponent<CharaOperation>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!mainMenu.MainMenuWindowMove)
            {
                mainMenu.MainMenuWindowMove = true;
                MainMenuStart();
            }
            else
            {
                mainMenu.MainMenuWindowMove = false;
                MainMenuEnd();
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
    }
    public void MainMenuEnd()
    {
        // カメラを戻す
        CamScript.CamReset();
        // キャラを操作できなくする
        charaOperation.CanRun = true;
    }
}
