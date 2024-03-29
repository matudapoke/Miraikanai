using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corsor : MonoBehaviour
{
    [Tooltip("魚がヒットしたかどうか")]
    public bool Hit;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Fish"))
        {
            Hit = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Fish"))
        {
            Hit = false;
        }
    }
}

//魚がCorsorに当たる
//操作ができなくなる
//FishCatchBarが出る
//エンターを押すとバーが進む