using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    // フラグ
    [HideInInspector]public bool MenuWindowMove1;
    [HideInInspector]public bool MenuWindowMove2;
    [HideInInspector]public bool MenuWindowMove3;
    // コンポーネント
    MainMenuContoller MenuContoller;
    void Start()
    {
        MenuContoller = transform.parent.gameObject.GetComponent<MainMenuContoller>();
    }

    void Update()
    {
        // MainMenuWindowを出す
        if (MenuWindowMove3)
        {
            transform.position = Vector3.Lerp(transform.position, MenuContoller.MenuWindowMovePosition3, MenuContoller.MenuMoveSpeed * Time.deltaTime);
        }
        if (MenuWindowMove2)
        {
            transform.position = Vector3.Lerp(transform.position, MenuContoller.MenuWindowMovePosition2, MenuContoller.MenuMoveSpeed * Time.deltaTime);
        }
        if (MenuWindowMove1)
        {
            transform.position = Vector3.Lerp(transform.position, MenuContoller.MenuWindowMovePosition1, MenuContoller.MenuMoveSpeed * Time.deltaTime);
        }
        // MainMenuWindowをしまう。
        if (!MenuWindowMove1 && !MenuWindowMove2 && !MenuWindowMove3)
        {
            transform.position = Vector3.Lerp(transform.position, MenuContoller.MenuWindowRetrunPosition, MenuContoller.MenuMoveSpeed * Time.deltaTime);
        }
        if (Input.GetKeyDown(KeyCode.Tab) && (MenuWindowMove1 || MenuWindowMove2 || MenuWindowMove3))
        {
            MenuWindowColse();
        }
    }

    public void MenuWindowMoveNextPosition()
    {
        if (MenuWindowMove2)
        {
            MenuWindowMove3 = true;
        }
        else if (MenuWindowMove1)
        {
            MenuWindowMove2 = true;
        }
        else
        {
            MenuWindowMove1 = true;
        }
    }

    public void MenuWindowColse()
    {
        MenuWindowMove1 = false;
        MenuWindowMove2 = false;
        MenuWindowMove3 = false;
    }
}
