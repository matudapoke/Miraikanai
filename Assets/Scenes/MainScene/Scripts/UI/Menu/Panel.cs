using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel : MonoBehaviour
{
    [SerializeField] float MoveSpeed;
    [SerializeField] Vector3 MovePosition;
    Vector3 OrignalPosition;

    [HideInInspector] public bool isShow;
    void Start()
    {
        OrignalPosition = transform.position;
    }

    void Update()
    {
        if (isShow)
        {
            transform.position = Vector3.Lerp(transform.position, MovePosition, MoveSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, OrignalPosition, MoveSpeed * Time.deltaTime);
        }

    }

    public void Show()
    {
        isShow = true;
    }
    public void Hide()
    {
        isShow = false;
    }
    public void ChangeValue()
    {
        if (isShow)
            Hide();
        else
            Show();
    }
}
