using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishBook : MonoBehaviour
{
    [SerializeField] GameObject Cursor;

    bool isOpenFishBook;

    GameObject Canvas_Obj;
    GameObject Camera_Obj;
    GameObject Reizi_Obj;
    void Start()
    {
        Canvas_Obj = GameObject.Find("CanvasUI");
        Camera_Obj = GameObject.Find("Main Camera");
        Reizi_Obj = GameObject.Find("Reizi");
    }
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Q) && !isOpenFishBook)
        {
            StartCoroutine(StartFishBook());
        }
        else if (Input.GetKeyUp(KeyCode.Q) && isOpenFishBook)
        {
            StartCoroutine (EndFishBook());
        }
    }
    IEnumerator StartFishBook()
    {
        isOpenFishBook = true;
        Reizi_Obj.GetComponent<Animator>().SetBool("IdolA", true);
        Reizi_Obj.GetComponent<CharaOperation>().CanRun = false;
        Canvas_Obj.SetActive(false);
        Camera_Obj.transform.Find("CameraFade").GetComponent<Fade>().FadeStart(0.1f);
        yield return new WaitForSeconds(0.5f);
        Camera_Obj.GetComponent<Cam>().ChangeTarget(Cursor.transform);
        yield return new WaitForSeconds(0.5f);
        Camera_Obj.transform.Find("CameraFade").GetComponent<Fade>().FadeEnd(0.5f);
        yield return new WaitForSeconds(0.3f);
        Reizi_Obj.GetComponent<Animator>().SetBool("IdolA", false);
    }
    IEnumerator EndFishBook()
    {
        isOpenFishBook = false;
        Camera_Obj.transform.Find("CameraFade").GetComponent<Fade>().FadeStart(0.5f);
        Reizi_Obj.GetComponent<Animator>().SetBool("IdolA", true);
        yield return new WaitForSeconds(0.5f);
        Camera_Obj.GetComponent<Cam>().ChangeTarget(Reizi_Obj.transform);
        yield return new WaitForSeconds(0.5f);
        Camera_Obj.transform.Find("CameraFade").GetComponent<Fade>().FadeEnd(0.1f);
        Canvas_Obj.SetActive(true);
        Reizi_Obj.GetComponent<Animator>().SetBool("IdolA", false);
        Reizi_Obj.GetComponent<CharaOperation>().CanRun = true;
    }
}
