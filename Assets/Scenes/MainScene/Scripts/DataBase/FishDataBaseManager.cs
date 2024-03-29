using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishDataBeseManager : MonoBehaviour
{
    //魚のデータベース-----------------------------------------------------------------------------
    
    [SerializeField]
    List<FishData> FishDataBase = new List<FishData>();

    public FishData FishRundom()//全体の魚の中からランダムで抽選する
    {
        return FishDataBase[Random.Range(0, FishDataBase.Count)];
    }



    //持っている魚のデータベース-------------------------------------------------------------------

    [SerializeField,Tooltip("持っている魚のデータベースを入れる")]
    PocketFishDataBase PocketFishDataBase;

    void Start() //ゲーム開始時にリストの中をカラにする
    { 
        for (int i = 0; i <  PocketFishDataBase.PocketFishDataBaseList.Count; i++)
        {
            PocketFishDataBase.PocketFishDataBaseList[i].NewFish = true;
        }
        PocketFishDataBase.PocketFishDataBaseList.Clear();
    }

    public void AddFishData(FishData fishData)//魚を追加する
    {
        PocketFishDataBase.PocketFishDataBaseList.Add(fishData);
    }

    //持っている魚をDebug.Logする----------

    //void Update()
    //{
    //int count = PocketFishDataBase.PocketFishDataBaseList.Count; // PocketFishDataBaseListの要素数

    //for (int i = 0; i < count; i++)
    //{
    //    FishData FishData = PocketFishDataBase.PocketFishDataBaseList[i];
    //    Debug.Log(FishData.FishName + " " + FishData.ID);
    //}

    //}

    //-------------------------------------
}