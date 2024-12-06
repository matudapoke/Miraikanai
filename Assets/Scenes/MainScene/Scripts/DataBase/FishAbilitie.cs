using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[SerializeField]
public class FishAbilitie : ScriptableObject
{
    [Tooltip("魚の能力の名前")]
    public string AbilitieName;

    [Tooltip("体力UP％")]
    public float HpUpRate;
    [Tooltip("体力UP量")]
    public int HpUpInt;
    [Tooltip("攻撃力UP％")]
    public float AttckPowerUpRate;
    [Tooltip("攻撃力UP量")]
    public int AttckPowerUpInt;
    [Tooltip("防御力UP％")]
    public float DefensPowerUpRate;
    [Tooltip("防御力UP量")]
    public int DefensPowerUpInt;
    [Tooltip("素早さUP％")]
    public float SpeedPowerUpRate;
    [Tooltip("素早さUP量")]
    public int SpeedPowerUpInt;
    [Tooltip("回避率UP％")]
    public float AvoidanceRate;
    [Tooltip("会心率UP％")]
    public float CriticalHitRate;
    [Tooltip("会心時UP率UP％")]
    public float CritocalPowerUpRate;
}