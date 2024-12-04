using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour
{
    [SerializeField] float MoveSpeed;
    bool RightMove;
    float SpwanPosition;
    void Start()
    {
        int RandomInt = Random.Range(0, 2);
        if (RandomInt == 0)
        {
            RightMove = true;
            transform.position = GameObject.FindWithTag("Player").transform.position + new Vector3(20, Random.Range(-7,7), 0);
        }
        else
        {
            RightMove = false;
            transform.Rotate(0, 180, 0);
            transform.position = GameObject.FindWithTag("Player").transform.position + new Vector3(-20, Random.Range(-7, 7), 0);
        }
        SpwanPosition = transform.position.x;
    }

    void Update()
    {
        if (RightMove)
        {
            transform.position -= new Vector3(MoveSpeed, 0, 0) * Time.deltaTime;
            if (transform.position.x <= SpwanPosition - 40)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            transform.position += new Vector3(MoveSpeed, 0, 0) * Time.deltaTime;
            if (transform.position.x >= SpwanPosition + 40)
            {
                Destroy(gameObject);
            }
        }
    }
}
