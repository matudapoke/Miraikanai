using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    CharaOperation charaOperation;
    enum Direction
    {
        Up,
        Down,
        Right,
        Left,
    }
    [SerializeField] Direction direction;
    void Start()
    {
        charaOperation = transform.parent.GetComponent<CharaOperation>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            if (direction == Direction.Up)
            {
                charaOperation.CanUpMove = false;
            }
            else if (direction == Direction.Down)
            {
                charaOperation.CanDownMove = false;
            }
            else if (direction == Direction.Right)
            {
                charaOperation.CanRightMove = false;
            }
            else if (direction == Direction.Left)
            {
                charaOperation.CanLeftMove = false;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            if (direction == Direction.Up)
            {
                charaOperation.CanUpMove = true;
            }
            else if (direction == Direction.Down)
            {
                charaOperation.CanDownMove = true;
            }
            else if (direction == Direction.Right)
            {
                charaOperation.CanRightMove = true;
            }
            else if (direction == Direction.Left)
            {
                charaOperation.CanLeftMove = true;
            }
        }
    }
}
