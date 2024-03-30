using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectableText : Button
{
    // �I����ԂɂȂ����Ƃ��Ɏ��s����
    public override void OnSelect(BaseEventData eventData)
    {
        GameObject.Find("ArrowText").transform.SetParent(transform
            );
        Debug.Log($"{gameObject.name}���I�����ꂽ");
    }
    // �I�������ɂȂ����Ƃ��Ɏ��s����
    public override void OnDeselect(BaseEventData eventData)
    {
        Debug.Log($"{gameObject.name}�̑I�����O�ꂽ");
    }
}
