using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    CharaOperation charaOperation;
    FishingManager fishingManager;
    Cam CamScript;
    void Start()
    {
        CamScript = GameObject.Find("Main Camera").GetComponent<Cam>();
        charaOperation = GameObject.Find("Reizi").GetComponent<CharaOperation>();
        fishingManager = GameObject.Find("Reizi").GetComponent<FishingManager>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && fishingManager.phase == FishingManager.Phase.End)
        {
            // ���j���[���J��
            if (charaOperation.CanRun)
            {
                CamScript.CamMove(3, new Vector3(3, 0, 0));
                CamScript.CamZoom(1.5f, 5);
                charaOperation.CanRun = false;
            }
            // ���j���[��߂�
            else
            {
                CamScript.CamReset();
                charaOperation .CanRun = true;
            }
        }
    }
}
