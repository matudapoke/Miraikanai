using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuManager : MonoBehaviour
{
    // 値
    [SerializeField] Vector3 WindowMovePosition;
    Vector3 MenuWindow_MovePosition;
    Vector3 ItemWindow_MovePosition;
    // ゲームオブジェクト
    [SerializeField] GameObject Arrow_Obj;
    [SerializeField] GameObject MenuWindow_Obj;
    [SerializeField] GameObject ItemWindow_Obj;
    // フラグ
    bool MenuWindow_Move;
    bool ItemWindow_Move;
    // コンポーネント
    RectTransform MenuWindow_RectTransform;
    RectTransform ItemWindow_RectTransform;
    CharaOperation charaOperation;
    FishingManager fishingManager;
    Cam CamScript;

    enum Phase
    {
        StartMenu,
        StartItem,
        End,
    }
    Phase phase = Phase.End;

    void Start()
    {
        // コンポーネント
        MenuWindow_RectTransform = MenuWindow_Obj.GetComponent<RectTransform>();
        ItemWindow_RectTransform = ItemWindow_Obj.GetComponent<RectTransform>();
        CamScript = GameObject.Find("Main Camera").GetComponent<Cam>();
        charaOperation = GameObject.Find("Reizi").GetComponent<CharaOperation>();
        fishingManager = GameObject.Find("Reizi").GetComponent<FishingManager>();
    }
    void Update()
    {
        // メニューを開く
        if (Input.GetKeyDown(KeyCode.Tab) && fishingManager.phase == FishingManager.Phase.End)
        {
            StartCoroutine(Menu());
        }
        // MenuWindowを動かす
        if (MenuWindow_Move)
        {
            MenuWindow_RectTransform.anchoredPosition = Vector2.Lerp(MenuWindow_RectTransform.anchoredPosition, MenuWindow_MovePosition, 5 * Time.deltaTime);
        }
        if (ItemWindow_Move)
        {
            ItemWindow_RectTransform.anchoredPosition = Vector2.Lerp(ItemWindow_RectTransform.anchoredPosition, ItemWindow_MovePosition, 5 * Time.deltaTime);
        }
    }
    
    IEnumerator Menu()
    {
        phase = Phase.StartMenu;
        while(phase != Phase.End)
        {
            yield return null;
            switch (phase)
            {
                case Phase.StartMenu:
                    // カメラを動かす
                    CamScript.CamMove(3, new Vector3(3, 0, 0));
                    CamScript.CamZoom(1.5f, 5);
                    // キャラを操作できなくする
                    charaOperation.CanRun = false;
                    // MenuWindowを出す
                    MenuWindow_Obj.SetActive(true);
                    MenuWindow_Move = true;
                    MenuWindow_MovePosition = WindowMovePosition;
                    Debug.Log(phase);
                    // 最初の子を選択する
                    EventSystem.current.SetSelectedGameObject(MenuWindow_Obj.transform.GetChild(0).gameObject);
                    Select(MenuWindow_Obj.transform.GetChild(0).gameObject);
                    yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.X));
                    if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.X))
                    {
                        // MenuWindowを戻す
                        MenuWindow_MovePosition = new Vector3(1500, 0, 0);
                    }
                    else
                    {
                        // MenuWindowをずらす
                        MenuWindow_MovePosition -= new Vector3(50, 0, 0);
                    }
                    break;
                case Phase.StartItem:
                    // MenuWindowを無効にする
                    MenuSelectOff(MenuWindow_Obj);
                    // ItemWindowを出す
                    ItemWindow_Obj.SetActive(true);
                    ItemWindow_Move = true;
                    ItemWindow_MovePosition = WindowMovePosition;
                    // 最初の子を選択する
                    EventSystem.current.SetSelectedGameObject(ItemWindow_Obj.transform.GetChild(0).gameObject);
                    Select(ItemWindow_Obj.transform.GetChild(0).gameObject);
                    yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.X));
                    break;
            }
        }
    }

    public void Select(GameObject obj)
    {
        Arrow_Obj.transform.SetParent(obj.transform);
    }
    void MenuSelectOff(GameObject obj)
    {
        Transform children = obj.GetComponentInChildren<Transform>();
        Debug.Log(children);
        //子要素がいなければ終了
        if (children.childCount == 0)
        {
            return;
        }
        foreach (Transform ob in children)
        {
            if (ob.GetComponent<MenuSelect>() != null)
            {
                ob.GetComponent<MenuSelect>().enabled = false;
            }
        }
    }
    public void StartItem()
    {
        phase = Phase.StartItem;
        Debug.Log("StartItemになりました");
    }
    public void Test()
    {
        Debug.Log("aaa");
    }
}
