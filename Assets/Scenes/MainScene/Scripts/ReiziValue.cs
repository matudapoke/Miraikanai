using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReiziValue : MonoBehaviour
{
    [HideInInspector] public bool isFishing;
    [HideInInspector] public bool isFishBook;
    [HideInInspector] public bool isSpeak;

    public bool ValueChack()
    {
        if (isFishing || isFishBook || isSpeak)
        {
            return false;
        }
        return true;
    }

}
