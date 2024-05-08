using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    // フラグ
    [HideInInspector]public bool MenuWindowMove;
    // コンポーネント
    MainMenuContoller MenuContoller;
    void Start()
    {
        MenuContoller = transform.parent.gameObject.GetComponent<MainMenuContoller>();
    }

    void Update()
    {
        // MainMenuWindowを出す
        if (MenuWindowMove)
        {
            transform.position = Vector3.Lerp(transform.position, MenuContoller.MenuWindowMovePosition, MenuContoller.MenuMoveSpeed * Time.deltaTime);
        }
        // MainMenuWindowをしまう。
        else
        {
            transform.position = Vector3.Lerp(transform.position, MenuContoller.MenuWindowRetrunPosition, MenuContoller.MenuMoveSpeed * Time.deltaTime);
        }
    }
}
