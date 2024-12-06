using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGroup : MonoBehaviour
{
    Vector3 OriginalPosition;
    Vector3 NextPosition;
    [SerializeField] float MoveSpeed;

    void Start()
    {
        OriginalPosition = transform.position;
        NextPosition = transform.position;
    }

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, NextPosition, MoveSpeed * Time.deltaTime);
    }

    public void MoveUI(Vector3 MovePlusPosition)
    {
        NextPosition = OriginalPosition + MovePlusPosition;
    }
}
