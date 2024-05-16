using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Reaction : MonoBehaviour
{
    // -------------Suprise(ビックリマーク)----------------
    [SerializeField] GameObject Suprise_Prefab;
    [SerializeField] float FadeOutTime;
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

    // -------------Action(アクションマーク)----------------
    [SerializeField] GameObject Action_Prefab;
    List<GameObject> Action_Objs = new List<GameObject>();
    SpriteRenderer ActionSpriteRenderer;
    Coroutine ActionCoroutine;
    int ActionID = 0;
    public int Action_Create(Vector3 Position, float Time)
    {
        StartCoroutine(Action_Create_Run(Position, Time));
        return ActionID-1;
    }
    IEnumerator Action_Create_Run(Vector3 Position, float Time)
    {
        if (ActionCoroutine != null)
        {
            StopCoroutine(ActionCoroutine);
        }
        Action_Destroy();
        Action_Objs.Add(Instantiate(Action_Prefab, Position, Quaternion.identity, transform));
        ActionID++;
        yield return new WaitForSeconds(Time);
        Action_FadeOut(ActionID-1);
    }
    public void Action_Destroy()
    {
        foreach (GameObject Action_Obj in Action_Objs)
        {
            Destroy(Action_Obj);
        }
    }
    public void Action_FadeOut(int ID)
    {
        if (Action_Objs[ID] != null)
        {
            if (ActionCoroutine != null)
            {
                StopCoroutine(ActionCoroutine);
            }
            ActionCoroutine = StartCoroutine(ActionFadeOut_Run(ID));
        }
    }
    IEnumerator ActionFadeOut_Run(int ID)
    {
        if (Action_Objs[ID] != null)
        {
            ActionSpriteRenderer = Action_Objs[ID].GetComponent<SpriteRenderer>();
            ActionSpriteRenderer.color = new Color(1,1,1,1);
            for (float i = 1; i > 0; i -= 0.05f)
            {
                if (Action_Objs[ID] != null)
                {
                    ActionSpriteRenderer.color = new Color(1, 1, 1, i);
                    yield return new WaitForSeconds(FadeOutTime / 255);
                }
            }
            Destroy(Action_Objs[ID]);
        }
    }
}
