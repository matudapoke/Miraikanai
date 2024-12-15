using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditorInternal;
using UnityEngine.UI;

public class FishImage : MonoBehaviour
{
    [SerializeField] public FishData fishData;

    [SerializeField] float plusScale;
    [SerializeField] float ChangeScaleSpeed;
    bool isSelect;
    bool isSelectMenu;
    Vector3 OriginalScale;
    GameObject FishBookUI;
    GameObject CurSorObj;
    Cam cam;

    private void Start()
    {
        FishBookUI = GameObject.Find("FishBookUI");
        OriginalScale = transform.localScale;
        cam = GameObject.Find("Main Camera").GetComponent<Cam>();
    }

    private void Update()
    {
        // 画像拡大
        if (isSelect && transform.localScale.x < OriginalScale.x + plusScale)
        {
            transform.localScale += new Vector3(ChangeScaleSpeed * Time.deltaTime, ChangeScaleSpeed * Time.deltaTime, ChangeScaleSpeed * Time.deltaTime);
        }
        else if (isSelect)
        {
            transform.localScale = new Vector3(OriginalScale.x + plusScale, OriginalScale.y + plusScale, 0);
        }
        if (!isSelect && transform.localScale.x > OriginalScale.x)
        {
            transform.localScale -= new Vector3(ChangeScaleSpeed * Time.deltaTime, ChangeScaleSpeed * Time.deltaTime, ChangeScaleSpeed * Time.deltaTime);
        }
        else if (!isSelect)
        {
            transform.localScale = OriginalScale;
        }
        // メニュー表示
        if (isSelect && !isSelectMenu && Input.GetKeyDown(KeyCode.Return) && GetComponent<SpriteRenderer>().color == Color.white)
        {
            FishBookUI.transform.Find("FishName").GetComponent<Text>().text = fishData.FishName;
            isSelectMenu = true;
            CurSorObj.GetComponent<CharaOperation>().CanRun = false;
            cam.ChangeTarget(gameObject.transform);
            cam.CamZoom(5, 1.2f);
            cam.CamMove(5, new Vector3(-1, 0, 0));
        }
        else if (isSelect && isSelectMenu && (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Z)))
        {
            FishBookUI.transform.Find("FishName").GetComponent<Text>().text = "";
            isSelectMenu = false;
            CurSorObj.GetComponent<CharaOperation>().CanRun = true;
            cam.CamReset();
            cam.ChangeTarget(CurSorObj.transform);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "FishBookCurSor")
        {
            isSelect = true;
            CurSorObj = collision.gameObject;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "FishBookCurSor")
        {
            isSelect = false;
            CurSorObj = null;
        }
    }
}
