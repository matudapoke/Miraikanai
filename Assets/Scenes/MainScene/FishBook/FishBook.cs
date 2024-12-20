using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishBook : MonoBehaviour
{
    [SerializeField] GameObject Cursor;

    [HideInInspector] public bool isOpenFishBook;
    bool isChangeValue;

    GameObject Canvas_Obj;
    GameObject Camera_Obj;
    GameObject Reizi_Obj;
    Vector3 CameraChift_tmp;
    void Start()
    {
        Canvas_Obj = GameObject.Find("CanvasUI");
        Camera_Obj = GameObject.Find("Main Camera");
        Reizi_Obj = GameObject.Find("Reizi");
        Cursor.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Q) && !isOpenFishBook && Reizi_Obj.GetComponent<ReiziValue>().ValueChack() && !isChangeValue)
        {
            StartCoroutine(StartFishBook());
        }
        else if (Input.GetKeyUp(KeyCode.Q) && isOpenFishBook && !isChangeValue)
        {
            StartCoroutine (EndFishBook());
        }
    }
    IEnumerator StartFishBook()
    {
        isOpenFishBook = true;
        isChangeValue = true;
        Cursor.transform.position = transform.position;
        Cursor.SetActive(true);
        Reizi_Obj.GetComponent<ReiziValue>().isFishBook = true;
        Reizi_Obj.GetComponent<Animator>().SetBool("IdolA", true);
        Reizi_Obj.GetComponent<CharaOperation>().CanRun = false;
        Canvas_Obj.SetActive(false);
        Camera_Obj.transform.Find("CameraFade").GetComponent<Fade>().FadeStart(0.1f);
        yield return new WaitForSeconds(0.5f);
        CameraChift_tmp = Camera_Obj.GetComponent<Cam>().ShiftPos;
        Camera_Obj.GetComponent<Cam>().ShiftPos = new Vector3(0, 0, 0);
        Camera_Obj.GetComponent<Cam>().ChangeTarget(Cursor.transform);
        yield return new WaitForSeconds(0.5f);
        Camera_Obj.transform.Find("FishBookFilter").GetComponent<Fade>().FadeStart(0.5f);
        Camera_Obj.transform.Find("CameraFade").GetComponent<Fade>().FadeEnd(0.5f);
        yield return new WaitForSeconds(0.3f);
        Reizi_Obj.GetComponent<Animator>().SetBool("IdolA", false);
        transform.Find("FishBookCursor").GetComponent<CharaOperation>().CanRun = true;
        isChangeValue = false;
    }
    IEnumerator EndFishBook()
    {
        isOpenFishBook = false;
        isChangeValue = true;
        Cursor.SetActive(false);
        Reizi_Obj.GetComponent<ReiziValue>().isFishBook = false;
        Camera_Obj.transform.Find("CameraFade").GetComponent<Fade>().FadeStart(0.5f);
        Reizi_Obj.GetComponent<Animator>().SetBool("IdolA", true);
        yield return new WaitForSeconds(0.5f);
        Camera_Obj.GetComponent<Cam>().ShiftPos =CameraChift_tmp;
        Camera_Obj.GetComponent<Cam>().ChangeTarget(Reizi_Obj.transform);
        yield return new WaitForSeconds(0.5f);
        Camera_Obj.transform.Find("FishBookFilter").GetComponent<Fade>().FadeEnd(0.5f);
        Camera_Obj.transform.Find("CameraFade").GetComponent<Fade>().FadeEnd(0.1f);
        Canvas_Obj.SetActive(true);
        Reizi_Obj.GetComponent<Animator>().SetBool("IdolA", false);
        Reizi_Obj.GetComponent<CharaOperation>().CanRun = true;
        transform.Find("FishBookCursor").GetComponent<CharaOperation>().CanRun = true;
        isChangeValue = false;
    }
}
