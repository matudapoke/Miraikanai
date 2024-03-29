using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[SerializeField]
public class FishAbilitie : ScriptableObject
{
    [Tooltip("���̔\�̖͂��O")]
    public string AbilitieName;

    [Tooltip("�̗�UP��")]
    public float HpUpRate;
    [Tooltip("�̗�UP��")]
    public int HpUpInt;
    [Tooltip("�U����UP��")]
    public float AttckPowerUpRate;
    [Tooltip("�U����UP��")]
    public int AttckPowerUpInt;
    [Tooltip("�h���UP��")]
    public float DefensPowerUpRate;
    [Tooltip("�h���UP��")]
    public int DefensPowerUpInt;
    [Tooltip("�f����UP��")]
    public float SpeedPowerUpRate;
    [Tooltip("�f����UP��")]
    public int SpeedPowerUpInt;
    [Tooltip("���UP��")]
    public float AvoidanceRate;
    [Tooltip("��S��UP��")]
    public float CriticalHitRate;
    [Tooltip("��S��UP��UP��")]
    public float CritocalPowerUpRate;
}