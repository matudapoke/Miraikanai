using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingPlace : MonoBehaviour
{
    [Header("出現する魚のリスト")]
    public List<FishData> FishBornList = new List<FishData>();
    public enum Direction
    {
        Up,
        Down,
        Right,
        Left,
        UpRight,
        UpLeft,
        DownRight,
        DownLeft,
    }
    [Header("釣り場の方向")]
    public Direction direction;


    //魚影生成 

    //[Tooltip("生成する範囲A"),SerializeField]
    //Transform RangeA;
    //[Tooltip("生成する範囲B"),SerializeField]
    //Transform RangeB;
    //[Tooltip("生成する魚の数"),SerializeField]
    //int FishBornInt;


    //void Start()
    //{
    //魚生成
    //  for (int i = 0; i <= FishBornInt; i++)
    //{
    //  FishData BornFishData = ChooseFishBasedOnRarity();//FishBornListから魚を抽選
    //GameObject BornFIshShadow = BornFishData.FishShadow;

    //float x = Random.Range(RangeA.position.x, RangeB.position.x);//rangeAとrangeBのx座標の範囲内でランダムな数値を作成
    //float y = Random.Range(RangeA.position.y, RangeB.position.y);//rangeAとrangeBのy座標の範囲内でランダムな数値を作成
    //Quaternion rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
    //Instantiate(BornFIshShadow, new Vector2(x, y), rotation);//魚生成

    //FishShadow BornFIshShadowScript = BornFIshShadow.GetComponent<FishShadow>();
    //BornFIshShadowScript.FishData = BornFishData;
    //}
    //}
}
