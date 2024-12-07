using System;
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
            StartCoroutine(StartFishBook());
        }
    }
    IEnumerator StartFishBook()
    {
        GameObject CameraObj = GameObject.Find("Main Camera");
        CameraObj.transform.Find("CameraFade").GetComponent<Fade>().FadeStart(0.5f);
        yield return new WaitForSeconds(0.5f);
        CameraObj.transform.position = new Vector3(-150, 0, 0);
        CameraObj.transform.Find("CameraFade").GetComponent<Fade>().FadeEnd(0.5f);
        CameraObj.GetComponent<Cam>().ChangeTarget(Cursor.transform);
        yield return new WaitForSeconds(0.5f);
    }
}
