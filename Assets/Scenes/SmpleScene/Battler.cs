using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battler : MonoBehaviour
{
    public new string name;
    public int hp;

    // ���s����R�}���h
    public CommandSO SelectCommand;
    public Battler target;

    // �����Ă�Z
    public CommandSO[] commands;
}
