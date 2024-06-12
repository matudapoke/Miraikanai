using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionEfect : MonoBehaviour
{
    bool ObjCollited;
    Reaction reaction;
    enum EfectType
    {
        Suprise,
        Action,
    }
    bool flag;
    [SerializeField] EfectType efectType;
    [SerializeField] float Time;
    int ID;


    void Start()
    {
        reaction = GameObject.Find("EventManager").GetComponent<Reaction>();
    }

    void DisplayEfect()
    {
        if (!flag)
        {
            flag = true;
            if (efectType == EfectType.Suprise)
            {
                reaction.Suprise(GameObject.FindWithTag("Player").transform.position, Time);
            }
            else if (efectType == EfectType.Action)
            {
                reaction.Action_Create(GameObject.FindWithTag("Player").transform.position);
            }
        }
    }
    void DestoryEfect()
    {
        flag = false;
        if (efectType == EfectType.Action)
        {
            reaction.Action_FadeOut(0.5f);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            DisplayEfect();
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            DestoryEfect();
        }
    }
}