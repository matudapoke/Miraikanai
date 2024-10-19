using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[SerializeField]
public class FishData : ScriptableObject
{
    [Tooltip("魚を管理するID"), Header("図鑑・基本情報")]
    public int ID;
    [Tooltip("魚の名前")]
    public string FishName;
    [Tooltip("魚の画像")]
    public Sprite FishImage;
    [Tooltip("画像サイズはいくつか")]
    public int FishImageSize;
    [Tooltip("魚の説明")]
    public string FishInfo;
    [Tooltip("魚が食い付くまでの時間(最短で何秒？)"), Header("釣り時")]
    public float FishBiteTime;
    [Tooltip("魚がHITしてから終わるまでの時間")]
    public float FishingHitWaitTime;
    [Tooltip("魚がHITしてから出るメーターの留める許容範囲の最小値(メーターの大きさ：0～1)"), Range(0, 1)]
    public float FishingMeterOKLevelMin;
    [Tooltip("魚がHITしてから出るメーターの留める許容範囲の最大値(メーターの大きさ：0～1)"), Range(0, 1)]
    public float FishingMeterOKLevelMax;
    [Tooltip("魚のレアリティ(1～5)"), Range(1, 5)]
    public int Rarity;
    [Tooltip("魚の抵抗力(Returnを押したら進む距離＝釣り竿のパワー÷魚の抵抗力)")]
    public float FishPower;
    [Tooltip("魚の難易度(バーを留める範囲＝釣り竿の安定性えあ割る÷魚の難易度​)")]
    public float FishLevel;
    
    [Tooltip("魚の金額"), Header("その他")]
    public int FishMoney;

    [Tooltip("図鑑に登録済みかどうか")]
    public bool NewFish;
}