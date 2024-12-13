using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditorInternal;

public class FishImage : MonoBehaviour
{
    [SerializeField] public FishData fishData;

    [SerializeField] float plusScale;
    [SerializeField] float ChangeScaleSpeed;
    bool isSelect;
    Vector3 OriginalScale;
    GameObject FishBookUI;

    private void Start()
    {
        FishBoo
        OriginalScale = transform.localScale;
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
        if (isSelect && Input.GetKeyDown(KeyCode.Return))
        {

        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "FishBookCurSor")
        {
            isSelect = true;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "FishBookCurSor")
        {
            isSelect = false;
        }
    }
}
