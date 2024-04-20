using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    // �t���O
    [HideInInspector]
    public bool MainMenuWindowMove;
    // �R���|�[�l���g
    MainMenuContoller MainMenuContoller;
    void Start()
    {
        MainMenuContoller = transform.parent.gameObject.GetComponent<MainMenuContoller>();
    }

    void Update()
    {
        // MainMenuWindow���o��
        if (MainMenuWindowMove)
        {
            transform.position = Vector3.Lerp(transform.position, MainMenuContoller.MenuWindowMovePosition, MainMenuContoller.MenuMoveSpeed * Time.deltaTime);
        }
        // MainMenuWindow�����܂��B
        else
        {
            transform.position = Vector3.Lerp(transform.position, MainMenuContoller.MenuWindowRetrunPosition, MainMenuContoller.MenuMoveSpeed * Time.deltaTime);
        }
    }
}
