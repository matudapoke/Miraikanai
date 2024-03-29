using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[SerializeField]
public class FishAbilitie : ScriptableObject
{
    [Tooltip("‹›‚Ì”\—Í‚Ì–¼‘O")]
    public string AbilitieName;

    [Tooltip("‘Ì—ÍUP“")]
    public float HpUpRate;
    [Tooltip("‘Ì—ÍUP—Ê")]
    public int HpUpInt;
    [Tooltip("UŒ‚—ÍUP“")]
    public float AttckPowerUpRate;
    [Tooltip("UŒ‚—ÍUP—Ê")]
    public int AttckPowerUpInt;
    [Tooltip("–hŒä—ÍUP“")]
    public float DefensPowerUpRate;
    [Tooltip("–hŒä—ÍUP—Ê")]
    public int DefensPowerUpInt;
    [Tooltip("‘f‘‚³UP“")]
    public float SpeedPowerUpRate;
    [Tooltip("‘f‘‚³UP—Ê")]
    public int SpeedPowerUpInt;
    [Tooltip("‰ñ”ğ—¦UP“")]
    public float AvoidanceRate;
    [Tooltip("‰ïS—¦UP“")]
    public float CriticalHitRate;
    [Tooltip("‰ïSUP—¦UP“")]
    public float CritocalPowerUpRate;
}