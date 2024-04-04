using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupController : MonoBehaviour
{
    [SerializeField, Tooltip("ポップアップのプレハブ")]
    GameObject PopupPrefab;

    [Tooltip("ポップアップが現在何個あるか")]
    int PopupCut = 0;
    List<GameObject> PopupList = new List<GameObject> { };
    [Tooltip("生成したポップアップオブジェクト")]
    GameObject Popup;
    [Tooltip("ポップアップのスクリプト")]
    Popup PopupScript;
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void SubmitPopup(string Name, Sprite Image)
    {
        if (PopupList.Count > PopupCut)//ポップアップがまだあったら
        {
            PopupCut += 1;
            for (int i =0; i > PopupList.Count; i++)
            {
                GameObject Popup = PopupList[i];
                Popup.gameObject.transform.position += new Vector3(30,0,0);
                Debug.Log("ｓ");
            }//もしもポップアップが出てきたら今出ているポップアップを上に移動させる
        }
        Popup = Instantiate(PopupPrefab, new Vector3(0, 0, 0), Quaternion.identity, transform);//ポップアップのクローンを生成する
        PopupScript = Popup.GetComponent<Popup>();
        PopupList.Add(Popup.gameObject);
        StartCoroutine(PopupScript.MovePopupRun(Name , Image));
        audioSource.Play();
    }
}
