using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour
{
    public IEnumerator FadeStart(float FadeOutTime)
    {
        SpriteRenderer ActionSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        ActionSpriteRenderer.color = new Color(1, 1, 1, 1);
        for (float i = 1; i > 0; i -= 0.05f)
        {
            ActionSpriteRenderer.color = new Color(1, 1, 1, i);
            yield return new WaitForSeconds(FadeOutTime / 255);
        }
    }

    public IEnumerator FadeEnd(float FadeOutTime)<----
    {
        SpriteRenderer ActionSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        ActionSpriteRenderer.color = new Color(1, 1, 1, 1);
        for (float i = 0; i < 1; i += 0.05f)
        {
            ActionSpriteRenderer.color = new Color(1, 1, 1, i);
            yield return new WaitForSeconds(FadeOutTime / 255);
        }
    }
}
