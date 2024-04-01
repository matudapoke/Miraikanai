using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuSelect : Button
{
    // 選択状態になったときに実行する
    public override void OnSelect(BaseEventData eventData)
    {
        transform.parent.gameObject.transform.parent.gameObject.GetComponent<MenuManager>().Select(gameObject);
        Debug.Log(gameObject.name);
    }
    // 選択解除になったときに実行する
    public override void OnDeselect(BaseEventData eventData)
    {
        
    }
}
