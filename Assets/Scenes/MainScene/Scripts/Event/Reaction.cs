using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reaction : MonoBehaviour
{
    [SerializeField]
    GameObject Suprise_Prefab;
    GameObject Suprise_Obj;
    public void Suprise(Vector3 Position, float Time)
    {
        Suprise_Obj = Instantiate(Suprise_Prefab, Position, Quaternion.identity, transform);
        Invoke("Suprise_Destroy", Time);
    }
    public void Suprise_Destroy()
    {
        if (Suprise_Obj != null)
        {
            Destroy(Suprise_Obj);
        }
    }
}
