using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReiziValue : MonoBehaviour
{
    public bool isFishing;
    public bool isFishBook;
    public bool isSpeak;
    public bool isWorkBoy;

    public bool ValueChack()
    {
        if (isFishing || isFishBook || isSpeak || isWorkBoy)
        {
            return false;
        }
        return true;
    }

}
