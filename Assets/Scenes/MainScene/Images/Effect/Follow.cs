using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReiziFollow : MonoBehaviour
{
    [SerializeField, Tooltip("���Ă����I�u�W�F�N�g")] GameObject Follow_Obj;
    [SerializeField, Tooltip("���W�����炷")] Vector3 ShiftPosition;

    void Start()
    {
        Follow_Obj = GameObject.Find("Reizi");
    }
    void Update()
    {
        transform.position = Follow_Obj.transform.position + ShiftPosition;
    }
}
