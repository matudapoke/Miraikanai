using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour
{
    public void FadeStart(float FadeTime)
    {
        StartCoroutine(FadeStartCoroutine(FadeTime));
    }
    public void FadeEnd(float FadeTime)
    {
        StartCoroutine(FadeEndCoroutine(FadeTime));
    }

    IEnumerator FadeStartCoroutine(float FadeTime)
    {
        SpriteRenderer ActionSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        ActionSpriteRenderer.color = new Color(ActionSpriteRenderer.color.r, ActionSpriteRenderer.color.g, ActionSpriteRenderer.color.b, 0);
        for (float i = 0; i < 1; i += 0.05f)
        {
            ActionSpriteRenderer.color = new Color(ActionSpriteRenderer.color.r, ActionSpriteRenderer.color.g, ActionSpriteRenderer.color.b, i);
            yield return new WaitForSeconds(FadeTime / 255.0f);
        }
        ActionSpriteRenderer.color = new Color(ActionSpriteRenderer.color.r, ActionSpriteRenderer.color.g, ActionSpriteRenderer.color.b, 1);
    }

    IEnumerator FadeEndCoroutine(float FadeTime)
    {
        SpriteRenderer ActionSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        ActionSpriteRenderer.color = new Color(ActionSpriteRenderer.color.r, ActionSpriteRenderer.color.g, ActionSpriteRenderer.color.b, 1);
        for (float i = 1; i > 0; i -= 0.05f)
        {
            ActionSpriteRenderer.color = new Color(ActionSpriteRenderer.color.r, ActionSpriteRenderer.color.g, ActionSpriteRenderer.color.b, i);
            yield return new WaitForSeconds(FadeTime / 255.0f);
        }
        ActionSpriteRenderer.color = new Color(ActionSpriteRenderer.color.r, ActionSpriteRenderer.color.g, ActionSpriteRenderer.color.b, 0);
    }
}
