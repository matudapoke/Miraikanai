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
        // Debug.Log($"{user}の攻撃！{target.name}に{3}のダメージ。残りHP{target.hp}");
    }
}
