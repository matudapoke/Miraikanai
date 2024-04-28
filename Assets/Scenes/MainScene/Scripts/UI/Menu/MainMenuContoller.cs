using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenuContoller : MonoBehaviour
{
    // �t���O
    public bool CanMainMenuOpen;
    [HideInInspector]
    public bool MenuNow;
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
    FishingManager fishingManager;

    void Start()
    {
        mainMenu = transform.Find("MainMenuWindow").GetComponent<MainMenu>();
        CamScript = GameObject.Find("Main Camera").GetComponent<Cam>();
        charaOperation = GameObject.Find("Reizi").GetComponent<CharaOperation>();
        fishingManager = GameObject.Find("Reizi").GetComponent<FishingManager>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && CanMainMenuOpen)
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
        if (Input.GetKeyDown(KeyCode.Tab) && fishingManager.phase == FishingManager.Phase.StartFishing)
        {
            // �������j���[�ɂ���
            if (!mainMenu.MainMenuWindowMove)
            {
                mainMenu.MainMenuWindowMove = true;
                fishingManager.FishingMenu = true;
            }
            else
            {
                mainMenu.MainMenuWindowMove = false;
                fishingManager.FishingMenu = false;
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
        // �t���O
        MenuNow = true;
    }
    public void MainMenuEnd()
    {
        // �J������߂�
        CamScript.CamReset();
        // �L�����𑀍�ł��Ȃ�����
        charaOperation.CanRun = true;
        // �t���O
        MenuNow = false;
    }
}
