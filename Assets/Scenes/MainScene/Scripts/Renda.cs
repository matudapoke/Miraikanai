using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

public class Renda : MonoBehaviour
{
    [SerializeField] float DownPosition;
    bool isDown;
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Z)) && !isDown)
        {
            transform.position -= new Vector3(0, DownPosition, 0);
            isDown = true;
        }
        else if ((Input.GetKeyUp(KeyCode.Return) || Input.GetKeyUp(KeyCode.Z)) && isDown)
        {
            transform.position += new Vector3(0, DownPosition, 0);
            isDown = false;
        }
    }
}
