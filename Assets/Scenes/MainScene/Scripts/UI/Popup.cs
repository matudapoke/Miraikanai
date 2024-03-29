using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Popup : MonoBehaviour
{
    [Tooltip("�|�b�v�A�b�v��RectTransfrom")]
    RectTransform rectTransform;
    [SerializeField, Tooltip("���O�̃e�L�X�g")]
    TextMeshProUGUI NameTMP;
    [SerializeField, Tooltip("�\������摜�̃Q�[���I�u�W�F�N�g")]
    Image Icon; 

    [SerializeField, Tooltip("�|�b�v�A�b�v���\������鎞��")]
    float SubmitPopupTime;
    [SerializeField, Tooltip("�|�b�v�A�b�v���߂���W")]
    Vector2 ReturnPostion;
    [SerializeField, Tooltip("�|�b�v�A�b�v���ړ�������W")]
    Vector2 MovePopupPosition;
    [SerializeField, Tooltip("�|�b�v�A�b�v�̃X�s�[�h")]
    float MoveSpeed;
    [Tooltip("MovePopup�̃t���O(0=���s���Ă��Ȃ� 1=�|�b�v�A�b�v���o�Ă���� 2=�|�b�v�A�b�v���߂�)")]
    int MovePopupRunning = 0;
    [Tooltip("�|�b�v�A�b�v���Q�ȏ�\�����ꂽ�Ƃ��Ɏ��s�����SweepPopup�̃t���O")]
    int SweepPopupRunning = 0;

    void Start()
    {
        rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = ReturnPostion;//�ŏ��̍��W�Ɉړ�
    }

    void Update()
    {
        if (MovePopupRunning == 1 || MovePopupRunning == 2)
        {
            MovePopup();
        }
    }

    void MovePopup()
    {
        if (MovePopupRunning == 1) rectTransform.anchoredPosition = Vector2.Lerp(rectTransform.anchoredPosition, MovePopupPosition, MoveSpeed * Time.deltaTime);//�|�b�v�A�b�v��\��
        else if (MovePopupRunning == 2) rectTransform.anchoredPosition = Vector2.Lerp(rectTransform.anchoredPosition, ReturnPostion, MoveSpeed * Time.deltaTime);//�|�b�v�A�b�v��߂�
        
        if (SweepPopupRunning == 1 && MovePopupRunning == 1)
        {
            Vector2 SweepPopupPosition = MovePopupPosition + new Vector2(0, 30);
            rectTransform.anchoredPosition = Vector2.Lerp(rectTransform.anchoredPosition, SweepPopupPosition, MoveSpeed * Time.deltaTime);
        }
    }

    public IEnumerator MovePopupRun(string Name, Sprite Image)
    {
        NameTMP.text = Name;
        Icon.sprite = Image;

        MovePopupRunning = 1;
        yield return new WaitForSeconds(SubmitPopupTime);
        MovePopupRunning = 2;
        yield return new WaitForSeconds(SubmitPopupTime);
        Destroy(gameObject);
    }

    public void SweepPopup(int PopupCut)
    {
        Debug.Log("xx"); ;
        SweepPopupRunning = 1;
        ReturnPostion += new Vector2(0, 30*PopupCut);
    }
}