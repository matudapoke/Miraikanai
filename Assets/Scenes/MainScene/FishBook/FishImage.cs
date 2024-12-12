using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishImage : MonoBehaviour
{
    [SerializeField] float plusScale;
    [SerializeField] float ChangeScaleSpeed;
    bool isSelect;
    Vector3 OriginalScale;

    private void Start()
    {
        OriginalScale = transform.localScale;
    }

    private void Update()
    {
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
