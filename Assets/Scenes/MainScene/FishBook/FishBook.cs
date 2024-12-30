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
    GameObject CameraParent_Obj;
    GameObject Reizi_Obj;
    Vector3 CameraChift_tmp;
    void Start()
    {
        Canvas_Obj = GameObject.Find("CanvasUI");
        CameraParent_Obj = GameObject.Find("Camera");
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
        CameraParent_Obj.transform.Find("CameraFade").GetComponent<Fade>().FadeStart(0.1f);
        yield return new WaitForSeconds(0.5f);
        CameraChift_tmp = CameraParent_Obj.transform.Find("Main Camera").GetComponent<Cam>().ShiftPos;
        CameraParent_Obj.transform.Find("Main Camera").GetComponent<Cam>().ShiftPos = new Vector3(0, 0, 0);
        CameraParent_Obj.transform.Find("Main Camera").GetComponent<Cam>().ChangeTarget(Cursor.transform);
        yield return new WaitForSeconds(0.5f);
        CameraParent_Obj.transform.Find("FishBookFilter").GetComponent<Fade>().FadeStart(0.5f);
        CameraParent_Obj.transform.Find("CameraFade").GetComponent<Fade>().FadeEnd(0.5f);
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
        CameraParent_Obj.transform.Find("CameraFade").GetComponent<Fade>().FadeStart(0.5f);
        Reizi_Obj.GetComponent<Animator>().SetBool("IdolA", true);
        yield return new WaitForSeconds(0.5f);
        CameraParent_Obj.transform.Find("Main Camera").GetComponent<Cam>().ShiftPos =CameraChift_tmp;
        CameraParent_Obj.transform.Find("Main Camera").GetComponent<Cam>().ChangeTarget(Reizi_Obj.transform);
        yield return new WaitForSeconds(0.5f);
        CameraParent_Obj.transform.Find("FishBookFilter").GetComponent<Fade>().FadeEnd(0.5f);
        CameraParent_Obj.transform.Find("CameraFade").GetComponent<Fade>().FadeEnd(0.1f);
        Canvas_Obj.SetActive(true);
        Reizi_Obj.GetComponent<Animator>().SetBool("IdolA", false);
        Reizi_Obj.GetComponent<CharaOperation>().CanRun = true;
        transform.Find("FishBookCursor").GetComponent<CharaOperation>().CanRun = true;
        isChangeValue = false;
    }
}
