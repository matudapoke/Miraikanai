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
    [SerializeField] EfectType efectType;
    [SerializeField] float Time;


    void Start()
    {
        reaction = GameObject.Find("EventManager").GetComponent<Reaction>();
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ObjCollited = true;
            if (efectType == EfectType.Suprise)
            {
                reaction.Suprise(collision.transform.position, Time);
            }
            else if (efectType == EfectType.Action)
            {
                reaction.Action_Create(collision.transform.position, Time);
            }
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (efectType == EfectType.Action)
            {
                reaction.Action_FadeOut_Run(0);
            }
        }
    }
}
