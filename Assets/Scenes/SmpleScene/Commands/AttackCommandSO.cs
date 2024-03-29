using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class AttackCommandSO : CommandSO
{
    [SerializeField] int AttackPoint;

    public override void Execute(Battler user, Battler target)
    {
        target.hp -= AttackPoint;
        Debug.Log($"{user.name}の{CommandName}！{target.name}に{AttackPoint}ダメージ。残りHP{target.hp}");
    }
}
