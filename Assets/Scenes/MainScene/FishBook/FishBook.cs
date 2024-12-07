using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishBook : MonoBehaviour
{
    [SerializeField] GameObject Cursor;
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Q))
        {
            GameObject CameraObj = GameObject.Find("Main Camera");
            CameraObj.transform.Find("CameraFade").GetComponent<FadeOut>().FadeOutStart(0.5f);
            CameraObj.transform.position = new Vector3(-150, 0, 0);
            CameraObj.transform.Find("CameraFade").GetComponent<FadeOut>().FadeOutEnd(0.5f);
            CameraObj.GetComponent<Cam>().ChangeTarget(Cursor.transform);
        }
    }
}
