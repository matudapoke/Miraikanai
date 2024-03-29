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
    [Tooltip("魚の説明")]
    public string FishInfo;

    [Tooltip("魚影のプレハブ"), Header("釣りパラメータ")]
    public GameObject FishShadow;
    [Tooltip("魚影の動きのスピード")]
    public float FishShadowSpeed;
    [Tooltip("魚影の回転するスピード")]
    public float FishTurnSmoothness;
    [Tooltip("魚影が回転する角度")]
    public float FishTurnAngle;
    [Tooltip("魚が食い付くまでの時間(最短で何秒？)")]
    public float FishBiteTime;
    [Tooltip("魚がHITしてから終わるまでの時間")]
    public float FishingHitWitTime;
    [Tooltip("魚がHITしてから出るメーターの留める許容範囲の最小値(メーターの大きさ：0～5.7)"), Range(0, 5.7f)]
    public float FishingMeterOKLevelMin;
    [Tooltip("魚がHITしてから出るメーターの留める許容範囲の最大値(メーターの大きさ：0～5.7)"), Range(0, 5.7f)]
    public float FishingMeterOKLevelMax;
    [Tooltip("魚のレアリティ(1～5)"), Range(1, 5)]
    public int Rarity;
    [Tooltip("魚の抵抗力(Returnを押したら進む距離＝釣り竿のパワー÷魚の抵抗力)")]
    public float FishPower;
    [Tooltip("魚の難易度(バーを留める範囲＝釣り竿の安定性えあ割る÷魚の難易度​)")]
    public float FishLevel;
    
    [Tooltip("魚のタイプ"), Header("バトルパラメータ")]
    public string FishType;//TypeDateBaseから抽出
    [Tooltip("魚の能力の名前")]
    public string FishAbilitie;
    [Tooltip("魚の能力に関する変する")]
    public float FishAblitiesVar;

    [Tooltip("図鑑に登録済みかどうか")]
    public bool NewFish;
}