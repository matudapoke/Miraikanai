using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [SerializeField] Battler player = default;
    [SerializeField] Battler enemy = default;

    enum Phase
    {
        StartPhase,
        ChooseComandPhase, // コマンド選択
        ExecutePhase,       // 実行
        Result,            // リザルト
        End,
    }

    Phase phase;

    void Start()
    {
        phase = Phase.StartPhase;
        StartCoroutine(Battle());
    }

    IEnumerator Battle()
    {
        while (phase != Phase.End)
        {
            yield return null;
            Debug.Log(phase);
            switch (phase)
            {
                case Phase.StartPhase:
                    phase = Phase.ChooseComandPhase;
                    break;
                case Phase.ChooseComandPhase:
                    //new WaitUntil(() => ここがtrueになるまで待機する)
                    yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
                    // コマンドを選択
                    player.SelectCommand = player.commands[1];
                    player.target = player;
                    enemy.SelectCommand = enemy.commands[0];
                    enemy.target = player;

                    phase = Phase.ExecutePhase;
                    break;
                case Phase.ExecutePhase:
                    player.SelectCommand.Execute(player, player.target);
                    enemy.SelectCommand.Execute(enemy, enemy.target);
                    // どちらかが死亡したら
                    if (player.hp <= 0 || enemy.hp <= 0)
                    {
                        phase = Phase.Result;
                    }
                    else
                    {
                        phase = Phase.ChooseComandPhase;
                    }
                    break;
                case Phase.Result:
                    phase = Phase.End;
                    break;
                case Phase.End:
                    break;
            }
        }
    }
}
