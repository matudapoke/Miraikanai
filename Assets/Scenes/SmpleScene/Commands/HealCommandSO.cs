using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class HealCommandSO : CommandSO
{
    [SerializeField] int HealPoint;

    // CommandSOのExecuteを実行せずに上書きして実行する
    public override void Execute(Battler user, Battler target)
    {
        target.hp += HealPoint;
        Debug.Log($"{user.name}の{CommandName}！{target.name}を{HealPoint}回復。残りHP{target.hp}");
    }
}
