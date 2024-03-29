using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class HealCommandSO : CommandSO
{
    [SerializeField] int HealPoint;

    // CommandSO��Execute�����s�����ɏ㏑�����Ď��s����
    public override void Execute(Battler user, Battler target)
    {
        target.hp += HealPoint;
        Debug.Log($"{user.name}��{CommandName}�I{target.name}��{HealPoint}�񕜁B�c��HP{target.hp}");
    }
}
