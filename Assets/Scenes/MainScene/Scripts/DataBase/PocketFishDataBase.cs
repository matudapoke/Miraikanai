using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[SerializeField]
public class PocketFishDataBase : ScriptableObject
{
    [Tooltip("�v���C���[�������Ă��鋛�̃��X�g")]
    public List<FishData> PocketFishDataBaseList = new List<FishData>();
}