using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuSelect : Button
{
    // �I����ԂɂȂ����Ƃ��Ɏ��s����
    public override void OnSelect(BaseEventData eventData)
    {
        transform.parent.gameObject.transform.parent.gameObject.GetComponent<MenuManager>().Select(gameObject);
        Debug.Log(gameObject.name);
    }
    // �I�������ɂȂ����Ƃ��Ɏ��s����
    public override void OnDeselect(BaseEventData eventData)
    {
        
    }
}
