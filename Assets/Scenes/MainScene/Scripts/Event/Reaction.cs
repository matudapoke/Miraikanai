using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reaction : MonoBehaviour
{
    // -------------Suprise(ビックリマーク)----------------
    [SerializeField] GameObject Suprise_Prefab;
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
    GameObject Action_Obj;
    SpriteRenderer ActionSpriteRenderer;
    Coroutine ActionCoroutine;
    public void Action_Create(Vector3 Position, float Time)
    {
        if (ActionCoroutine != null)
        {
            StopCoroutine(ActionCoroutine);
        }
        Action_Destroy();
        Action_Obj = Instantiate(Action_Prefab, Position, Quaternion.identity, transform);
        Action_FadeOut_Run(Time);
    }
    public void Action_Destroy()
    {
        if (Action_Obj != null)
        {
            Destroy(Action_Obj);
        }
    }
    public void Action_FadeOut_Run(float time)
    {
        if (ActionCoroutine != null)
        {
            StopCoroutine(ActionCoroutine);
        }
        ActionCoroutine = StartCoroutine(ActionFadeOut(time));
    }
    IEnumerator ActionFadeOut(float Time)
    {
        yield return new WaitForSeconds(Time);
        if (Action_Obj != null)
        {
            ActionSpriteRenderer = Action_Obj.GetComponent<SpriteRenderer>();
            ActionSpriteRenderer.color = new Color(1,1,1,1);
            for (float i = 1; i > 0; i -= 0.05f)
            {
                ActionSpriteRenderer.color = new Color(1,1,1,i);
                yield return null;
            }
            Action_Destroy();
        }
    }
}
