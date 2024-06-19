using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReiziFollow : MonoBehaviour
{
    [SerializeField, Tooltip("ついていくオブジェクト")] GameObject Follow_Obj;
    [SerializeField, Tooltip("座標をずらす")] Vector3 ShiftPosition;

    void Start()
    {

    }
    void Update()
    {
        transform.position = Follow_Obj.transform.position + ShiftPosition;
    }
}
