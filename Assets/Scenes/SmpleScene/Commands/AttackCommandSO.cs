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
        Debug.Log($"{user.name}��{CommandName}�I{target.name}��{AttackPoint}�_���[�W�B�c��HP{target.hp}");
    }
}
