using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectableText : Button
{
    // 選択状態になったときに実行する
    public override void OnSelect(BaseEventData eventData)
    {
        GameObject.Find("ArrowText").transform.SetParent(transform
            );
        Debug.Log($"{gameObject.name}が選択された");
    }
    // 選択解除になったときに実行する
    public override void OnDeselect(BaseEventData eventData)
    {
        Debug.Log($"{gameObject.name}の選択が外れた");
    }
}
