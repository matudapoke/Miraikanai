using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battler : MonoBehaviour
{
    public new string name;
    public int hp;

    // 実行するコマンド
    public CommandSO SelectCommand;
    public Battler target;

    // 持ってる技
    public CommandSO[] commands;
}
