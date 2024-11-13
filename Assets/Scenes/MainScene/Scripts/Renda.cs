using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.UI;
using Unity.VisualScripting;

public class Renda : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(IntervalMovePosition());
    }
    IEnumerator IntervalMovePosition()
    {
        transform.position = new Vector3(Random.Range(-0.5f,1f), Random.Range(-0.5f,1f), 0) + GetComponentInParent<Transform>().position;        
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(IntervalMovePosition());
    }
}
