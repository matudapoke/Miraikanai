using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenuContoller : MonoBehaviour
{
    // �l
    [SerializeField] Vector3 WindowMovePosition;
    // �Q�[���I�u�W�F�N�g
    [SerializeField] GameObject Selected_Prefab;
    [HideInInspector] public GameObject Selected_Obj;
    // �t���O
    bool WindowOpen;
    [HideInInspector] public bool CanMainMenu;
    // �R���|�[�l���g
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
        // �t���O
        CanMainMenu = true;
        // �Q�[���I�u�W�F�N�g
        MainMenuWindow_Obj = transform.Find("MainMenuWindow").gameObject;
        EquipmentMenuWindow_Obj = transform.Find("EquipmentMenuWindow").gameObject;
        FishingMenuWindow_Obj = transform.Find("FishingMenuWindow").gameObject;
        // �R���|�[�l���g
        charaOperation = GameObject.Find("Reizi").GetComponent<CharaOperation>();
        CamScript = GameObject.Find("Main Camera").GetComponent<Cam>();
        fishingManager = GameObject.Find("Reizi").GetComponent<FishingManager>();
        // �ŏ��͑I��s�ɂ���i�R���|�[�l���g���I�t�ɂ���j
        MenuSelectSet(MainMenuWindow_Obj, false);
        MenuSelectSet(EquipmentMenuWindow_Obj, false);
        MenuSelectSet(FishingMenuWindow_Obj, false);
    }

    void Update()
    {
        // MainMenuWindow���o��
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
        // Window����߂�
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
        // MenuWindow�𓮂���
        if (MainMenuWindow_Move)
        {
            MainMenuWindow_Obj.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(MainMenuWindow_Obj.GetComponent<RectTransform>().anchoredPosition, MainMenuWindow_MovePosition, 7.5f * Time.deltaTime);
        }
        // EquipmentWindow�𓮂���
        if (EquipmentMenuWindow_Move)
        {
            EquipmentMenuWindow_Obj.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(EquipmentMenuWindow_Obj.GetComponent<RectTransform>().anchoredPosition, EquipmentMenuWindow_MovePosition, 7.5f * Time.deltaTime);
        }
        // FishingWindow�𓮂���
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
        // MainMenuWindow���o��
        MainMenuWindow_Move = true;
        MainMenuWindow_MovePosition = WindowMovePosition;
        // �R���|�[�l���g���I���ɂ���
        MenuSelectSet(MainMenuWindow_Obj, true);
        // �ŏ��̎q��I������
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
            // EquipmentMenuWindow���o��
            EquipmentMenuWindow_Open = true;
            EquipmentMenuWindow_Move = true;
            EquipmentMenuWindow_MovePosition = WindowMovePosition;
            // �R���|�[�l���g���I���ɂ���
            MenuSelectSet(EquipmentMenuWindow_Obj, true);
            // �ŏ��̎q��I������
            EventSystem.current.SetSelectedGameObject(EquipmentMenuWindow_Obj.transform.GetChild(0).gameObject);
            Selected_Obj.transform.SetParent(EquipmentMenuWindow_Obj.transform.GetChild(0));
            // MainMenuWindow���ړ�
            MainMenuWindow_MovePosition -= new Vector3(100, 0, 0);
            MenuSelectSet(MainMenuWindow_Obj, false);
        }
    }
    void EquipmentMenuWindowClose()
    {
        // Window��߂�
        EquipmentMenuWindow_Open = false;
        EquipmentMenuWindow_MovePosition = new Vector3(1500, 0, 0);
        MenuSelectSet(EquipmentMenuWindow_Obj, false);
        // MainMenuWindow���o��
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
            // FishingMenuWindow���o��
            FishingMenuWindow_Open = true;
            FishingMenuWindow_Move = true;
            FishingMenuWindow_MovePosition = WindowMovePosition;
            // �R���|�[�l���g���I���ɂ���
            MenuSelectSet(FishingMenuWindow_Obj, true);
            // �ŏ��̎q��I������
            EventSystem.current.SetSelectedGameObject(FishingMenuWindow_Obj.transform.GetChild(0).gameObject);
            Selected_Obj.transform.SetParent(FishingMenuWindow_Obj.transform.GetChild(0));
            // MainMenuWindow���ړ�
            MainMenuWindow_MovePosition -= new Vector3(100, 0, 0);
            MenuSelectSet(MainMenuWindow_Obj, false);
        }
        if (!CanMainMenu)
        {
            if (!WindowOpen)
            {
                WindowOpenStart();
            }
            // FishingMenuWindow���o��
            FishingMenuWindow_Open = true;
            FishingMenuWindow_Move = true;
            FishingMenuWindow_MovePosition = WindowMovePosition;
            // �R���|�[�l���g���I���ɂ���
            MenuSelectSet(FishingMenuWindow_Obj, true);
            // �ŏ��̎q��I������
            EventSystem.current.SetSelectedGameObject(FishingMenuWindow_Obj.transform.GetChild(0).gameObject);
            Selected_Obj.transform.SetParent(FishingMenuWindow_Obj.transform.GetChild(0));
        }
    }
    public void FishingMenuWindowClose()
    {
        // Window��߂�
        FishingMenuWindow_Open = false;
        FishingMenuWindow_MovePosition = new Vector3(1500, 0, 0);
        MenuSelectSet(FishingMenuWindow_Obj, false);
        if (CanMainMenu)
        {
            // MainMenuWindow���o��
            MainMenuWindow_Obj.SetActive(true);
            MenuSelectSet(MainMenuWindow_Obj, true);
            MainMenuWindowOpen();
        }
    }
    void WindowOpenStart()
    {
        if (fishingManager.phase != FishingManager.Phase.StartFishing)
        {
            // �J�����𓮂���
            CamScript.CamMove(10, new Vector3(1.1f, 0, 0));
            CamScript.CamZoom(10, 2.5f);
            // �L�����𑀍�ł��Ȃ�����
            charaOperation.CanRun = false;
        } 
        // Selected���o��
        Selected_Obj = Instantiate(Selected_Prefab, transform.position, Quaternion.identity);
        // �t���O
        WindowOpen = true;
        fishingManager.CanFishing = false;
    }
    void WindowClose()
    {
        // Window��߂�
        EquipmentMenuWindowClose();
        FishingMenuWindowClose();
        MainMenuWindow_MovePosition = new Vector3(1500, 0, 0);
        MenuSelectSet(MainMenuWindow_Obj, false);
        // �J������߂�
        CamScript.CamReset();
        // �L�����𑀍�ł���悤�ɂ���
        charaOperation.CanRun = true;
        // Selected������
        Destroy(Selected_Obj);
        // �t���O��߂�
        WindowOpen = false;
        fishingManager.CanFishing = true;
    }
    void MenuSelectSet(GameObject obj, bool value)
    {
        Transform children = obj.GetComponentInChildren<Transform>();
        //�q�v�f�����Ȃ���ΏI��
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
