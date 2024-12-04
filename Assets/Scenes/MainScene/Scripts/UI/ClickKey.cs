using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class ClickKey : MonoBehaviour
{
    [SerializeField] Sprite OnClickImage;
    [SerializeField] Sprite NoneClickImage;
    [SerializeField] KeyCode keyCode;
    
    void Update()
    {
        if (Input.GetKeyDown(keyCode))
        {
            GetComponent<Image>().sprite = OnClickImage;
        }
        else if (Input.GetKeyUp(keyCode))
        {
            GetComponent<Image>().sprite = NoneClickImage;
        }
    }
}
