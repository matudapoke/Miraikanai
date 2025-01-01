using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditorInternal;
using UnityEngine.UI;
using Unity.VisualScripting;

public class FishImage : MonoBehaviour
{
    [SerializeField] public FishData fishData;

    [SerializeField, Tooltip("カーソルを合わせた時にどれだけ大きくなるか")] float plusScale;
    [SerializeField, Tooltip("大きくなるスピード")] float ChangeScaleSpeed;
    bool isSelect;
    Vector3 OriginalScale;

    FishBookManager fishBookManager;

    private void Start()
    {
        OriginalScale = transform.localScale;
        fishBookManager = GameObject.Find("FishBook").GetComponent<FishBookManager>();
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
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "FishBookCurSor")
        {
            isSelect = true;
            fishBookManager.SelectedFishImage_Obj = gameObject;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "FishBookCurSor")
        {
            isSelect = false;
            fishBookManager.SelectedFishImage_Obj = null;
        }
    }
}
