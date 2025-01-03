using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[SerializeField]
public class CassetteData : ScriptableObject
{
    [Tooltip("曲名")]
    public string Name;
    [Tooltip("作曲者など")]
    public string SubName;
    [Tooltip("画像")]
    public Sprite Image;
    [Tooltip("画像サイズはいくつか")]
    public int ImageSize;
    
    public AudioClip Music;
}