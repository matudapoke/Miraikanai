using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishBookManager : MonoBehaviour
{
    // フラグ
    bool isOpenFishBook;
    // オブジェクト
    GameObject Reizi_Obj;
    private void Start()
    {
        Reizi_Obj = GameObject.FindWithTag("Player");
    }
    private void Update()
    {
        if (Reizi_Obj.GetComponent<ReiziValue>().ValueChack() && Input.GetKeyDown(KeyCode.Q))
        {
            if (!isOpenFishBook)// 魚図鑑を開ける
            {

            }
            else// 魚図鑑を閉める
            {

            }
        }
    }
}
