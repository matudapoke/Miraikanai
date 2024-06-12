using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Reaction : MonoBehaviour
{
    [SerializeField] float FadeOutTime;
    void Update()
    {
        if (Action_Display && Action_DestroyTime <= Time.realtimeSinceStartup && Action_Obj != null)
        {
            ActionFadeOut_Run(2);
        }
    }

    // -------------Suprise(ビックリマーク)----------------
    [SerializeField] GameObject Suprise_Prefab;
    GameObject Suprise_Obj;
    public void Suprise(Vector3 Position, float Time)
    {
        Suprise_Obj = Instantiate(Suprise_Prefab, Position, Quaternion.identity);
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
    bool Action_Display;
    [SerializeField] GameObject Action_Prefab;
    GameObject Action_Obj;
    float Action_DestroyTime;
    SpriteRenderer ActionSpriteRenderer;
    Coroutine action_FadeOut_Run;
    public void Action_Create(Vector3 Position)
    {
        if (Action_Obj != null)
        {
            Destroy(Action_Obj);
            StopCoroutine(action_FadeOut_Run);
        }
        Action_Obj = Instantiate(Action_Prefab, Position, Quaternion.identity);
        Action_DestroyTime = Time.realtimeSinceStartup + 5;
        Action_Display = true;
    }
    
    public void Action_FadeOut(float fadeOutTime)
    {
        action_FadeOut_Run=StartCoroutine(ActionFadeOut_Run(fadeOutTime));
    }

    IEnumerator ActionFadeOut_Run(float fadeOutTime)
    {
        if (Action_Obj != null)
        {
            ActionSpriteRenderer = Action_Obj.GetComponent<SpriteRenderer>();
            ActionSpriteRenderer.color = new Color(1, 1, 1, 1);
            for (float i = 1; i > 0; i -= 0.05f)
            {
                if (Action_Obj != null)
                {
                    ActionSpriteRenderer.color = new Color(1, 1, 1, i);
                    yield return new WaitForSeconds(fadeOutTime / 255);
                }
                else
                {

                    Action_Display = false;
                    break;
                }
            }
            Destroy(Action_Obj);
            Action_Display = false;
        }
    }
}
