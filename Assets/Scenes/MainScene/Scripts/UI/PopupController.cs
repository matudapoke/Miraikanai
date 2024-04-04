using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupController : MonoBehaviour
{
    [SerializeField, Tooltip("�|�b�v�A�b�v�̃v���n�u")]
    GameObject PopupPrefab;

    [Tooltip("�|�b�v�A�b�v�����݉����邩")]
    int PopupCut = 0;
    List<GameObject> PopupList = new List<GameObject> { };
    [Tooltip("���������|�b�v�A�b�v�I�u�W�F�N�g")]
    GameObject Popup;
    [Tooltip("�|�b�v�A�b�v�̃X�N���v�g")]
    Popup PopupScript;
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void SubmitPopup(string Name, Sprite Image)
    {
        if (PopupList.Count > PopupCut)//�|�b�v�A�b�v���܂���������
        {
            PopupCut += 1;
            for (int i =0; i > PopupList.Count; i++)
            {
                GameObject Popup = PopupList[i];
                Popup.gameObject.transform.position += new Vector3(30,0,0);
                Debug.Log("��");
            }//�������|�b�v�A�b�v���o�Ă����獡�o�Ă���|�b�v�A�b�v����Ɉړ�������
        }
        Popup = Instantiate(PopupPrefab, new Vector3(0, 0, 0), Quaternion.identity, transform);//�|�b�v�A�b�v�̃N���[���𐶐�����
        PopupScript = Popup.GetComponent<Popup>();
        PopupList.Add(Popup.gameObject);
        StartCoroutine(PopupScript.MovePopupRun(Name , Image));
        audioSource.Play();
    }
}
