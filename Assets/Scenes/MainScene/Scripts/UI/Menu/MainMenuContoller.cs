using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenuContoller : MonoBehaviour
{
    // �l
    [Tooltip("���j���[���ǂ̈ʒu�Ɉړ����Ă��邩")]
    public Vector3 MenuWindowMovePosition;
    [Tooltip("���j���[���ǂ̈ʒu�ɖ߂邩")]
    public Vector3 MenuWindowRetrunPosition;
    [Tooltip("���j���[���ړ����鑬��)")]
    public float MenuMoveSpeed;
    // �R���|�[�l���g
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
        // �J�����𓮂���
        CamScript.CamMove(10, new Vector3(1.4f, 0, 0));
        CamScript.CamZoom(10, 2.5f);
        // �L�����𑀍�ł��Ȃ�����
        charaOperation.CanRun = false;
    }
    public void MainMenuEnd()
    {
        // �J������߂�
        CamScript.CamReset();
        // �L�����𑀍�ł��Ȃ�����
        charaOperation.CanRun = true;
    }
}
