using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    // フラグ
    [HideInInspector]
    public bool MainMenuWindowMove;
    // コンポーネント
    MainMenuContoller MainMenuContoller;
    void Start()
    {
        MainMenuContoller = transform.parent.gameObject.GetComponent<MainMenuContoller>();
    }

    void Update()
    {
        // MainMenuWindowを出す
        if (MainMenuWindowMove)
        {
            transform.position = Vector3.Lerp(transform.position, MainMenuContoller.MenuWindowMovePosition, MainMenuContoller.MenuMoveSpeed * Time.deltaTime);
        }
        // MainMenuWindowをしまう。
        else
        {
            transform.position = Vector3.Lerp(transform.position, MainMenuContoller.MenuWindowRetrunPosition, MainMenuContoller.MenuMoveSpeed * Time.deltaTime);
        }
    }
}
