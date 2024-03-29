using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[SerializeField]
public class FishAbilitie : ScriptableObject
{
    [Tooltip("Ì\ÍÌ¼O")]
    public string AbilitieName;

    [Tooltip("ÌÍUP")]
    public float HpUpRate;
    [Tooltip("ÌÍUPÊ")]
    public int HpUpInt;
    [Tooltip("UÍUP")]
    public float AttckPowerUpRate;
    [Tooltip("UÍUPÊ")]
    public int AttckPowerUpInt;
    [Tooltip("häÍUP")]
    public float DefensPowerUpRate;
    [Tooltip("häÍUPÊ")]
    public int DefensPowerUpInt;
    [Tooltip("f³UP")]
    public float SpeedPowerUpRate;
    [Tooltip("f³UPÊ")]
    public int SpeedPowerUpInt;
    [Tooltip("ñð¦UP")]
    public float AvoidanceRate;
    [Tooltip("ïS¦UP")]
    public float CriticalHitRate;
    [Tooltip("ïSUP¦UP")]
    public float CritocalPowerUpRate;
}