using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CommandSO : ScriptableObject
{
    public string CommandName;

    public virtual void Execute(Battler user, Battler target)
    {
        // target.hp -= 3;
        // Debug.Log($"{user}�̍U���I{target.name}��{3}�̃_���[�W�B�c��HP{target.hp}");
    }
}
