using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[SerializeField]
public class PocketFishDataBase : ScriptableObject
{
    [Tooltip("プレイヤーが持っている魚のリスト")]
    public List<FishData> PocketFishDataBaseList = new List<FishData>();
}